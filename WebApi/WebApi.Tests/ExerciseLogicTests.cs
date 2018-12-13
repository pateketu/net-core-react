using Moq;
using System;
using WebApi.Logic;
using WebApi.Model;
using WebApi.Repo;
using Xunit;

namespace WebApi.Tests
{
    
    public class ExerciseLogicTests
    {
        private readonly int _timeFrameSecs = 30;
        readonly ExerciseLogic _sut;
        readonly Mock<IExerciseRepo> _mockRepo;

        public ExerciseLogicTests()
        {
            _mockRepo = new Mock<IExerciseRepo>();
            var mockRandomGenerator = new Mock<IRandomGenerator>();
            var random = new Random();
            mockRandomGenerator.Setup(r => r.Next()).Returns(random.Next());

            _sut = new ExerciseLogic(_mockRepo.Object, mockRandomGenerator.Object);
        }

        [Fact]
        public void Start_CreatesABeginnerLevelExercise()
        {
            var ex = _sut.Start();

            Assert.True(ex.Level == ExerciseLevel.Beginner);
            Assert.True(ex.TimeFrameSeconds == 20);
        }

        [Fact]
        public void GetNext_WhenExerciseInExpertLevel_ReturnsResultWithAllLevelsFinished()
        {
            _mockRepo.Setup(r => r.Get(It.IsAny<Guid>())).Returns(new Exercise() { Level = ExerciseLevel.Expert });

            var result = _sut.GetNext(Guid.NewGuid(), 12);

            Assert.True(result.AllLevelsFinished);

        }

        [Fact]
        public void GetNext_WhenExerciseHasExpired_ReturnsResultWithTimeFrameExpired()
        {
            _mockRepo.Setup(r => r.Get(It.IsAny<Guid>())).Returns(new Exercise()
            {
                Level = ExerciseLevel.Advanced,
                TimeFrameSeconds = _timeFrameSecs,
                StartedAt = DateTime.Now.Subtract(new TimeSpan(0, 0, 40))
            });

            var result = _sut.GetNext(Guid.NewGuid(), 12);

            Assert.True(result.TimeFrameExpired);
        }

        [Fact]
        public void GetNext_ExerciseWithWrongAnswer_ReturnsResultWithInCorrectAnswer()
        {
            _mockRepo.Setup(r => r.Get(It.IsAny<Guid>())).Returns(new Exercise()
            {
                Level = ExerciseLevel.Advanced,
                A = 1,
                B = 2,
                TimeFrameSeconds = _timeFrameSecs,
                StartedAt = DateTime.Now.Subtract(new TimeSpan(0, 0, 20))
            });

            var result = _sut.GetNext(Guid.NewGuid(), 12);

            Assert.True(result.InCorrectAnswer);
        }

        [Fact]
        public void GetNext_CorrectAnswerAndUnExpired_ReturnsResultWithNextExercise()
        {
            _mockRepo.Setup(r => r.Get(It.IsAny<Guid>())).Returns(new Exercise()
            {
                Level = ExerciseLevel.Beginner,
                A = 1,
                B = 2,
                TimeFrameSeconds = _timeFrameSecs,
                StartedAt = DateTime.Now.Subtract(new TimeSpan(0, 0, 20))
            });

            var result = _sut.GetNext(Guid.NewGuid(), 3);

            Assert.NotNull(result.Exercise);
            Assert.True(result.Exercise.Level == ExerciseLevel.Beginner);
            Assert.True(result.Exercise.TimeFrameSeconds == _timeFrameSecs);
        }

        [Theory]        
        [InlineData(ExerciseLevel.Beginner, ExerciseLevel.Talented, 30)]
        [InlineData(ExerciseLevel.Talented, ExerciseLevel.Intermediate, 29)]
        [InlineData(ExerciseLevel.Intermediate, ExerciseLevel.Advanced, 28)]
        [InlineData(ExerciseLevel.Advanced, ExerciseLevel.Expert, 27)]
        public void GetNext_MovesLevelOfExerciseAsExpected(ExerciseLevel currentLevel, ExerciseLevel nextLevel, int currentLevelTimeFrame)
        {

            var exercise = new Exercise()
            {
                Level = currentLevel,
                A = 1,
                B = 2,
                TimeFrameSeconds = currentLevelTimeFrame,
                StartedAt = DateTime.Now.Subtract(new TimeSpan(0, 0, 20))
            };
            _mockRepo.Setup(r => r.Get(It.IsAny<Guid>())).Returns(exercise);

            ExerciseResult result = null;
            Guid id = Guid.NewGuid();
            for (int i = 0; i < 3; i++)
            {
                result = _sut.GetNext(id, (exercise.A + exercise.B));
                exercise = result.Exercise;
            }

            Assert.True(result != null && result.Exercise.Level == nextLevel);
            Assert.True(result.Exercise.TimeFrameSeconds == currentLevelTimeFrame - 1);
        }
    }
}
