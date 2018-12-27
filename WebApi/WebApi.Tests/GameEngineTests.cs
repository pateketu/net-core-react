using Moq;
using System;
using WebApi.Logic;
using WebApi.Model;
using WebApi.Repo;
using Xunit;

namespace WebApi.Tests
{
    
    public class GameEngineTests
    {
        private const int TimeFrameSecs = 30;
        private readonly GameEngine _sut;
        private readonly Mock<IGameRepo> _mockRepo;

        public GameEngineTests()
        {
            _mockRepo = new Mock<IGameRepo>();
            var mockRandomGenerator = new Mock<IRandomGenerator>();
            var random = new Random();
            mockRandomGenerator.Setup(r => r.Next()).Returns(random.Next());

            _sut = new GameEngine(_mockRepo.Object, mockRandomGenerator.Object);
        }

        [Fact]
        public void Start_CreatesABeginnerLevelExercise()
        {
            var ex = _sut.Start();

            Assert.True(ex.Level == Level.Beginner);
            Assert.True(ex.TimeFrameSeconds == Constants.InitialTimeFrameSeconds);
        }

        [Fact]
        public void GetNext_WhenExerciseInExpertLevel_ReturnsResultWithAllLevelsFinished()
        {
            _mockRepo.Setup(r => r.Get(It.IsAny<Guid>())).Returns(new Game() { Level = Level.Expert });

            var result = _sut.GetNext(Guid.NewGuid(), 12);

            Assert.True(result.AllLevelsFinished);

        }

        [Fact]
        public void GetNext_WhenExerciseHasExpired_ReturnsResultWithTimeFrameExpired()
        {
            _mockRepo.Setup(r => r.Get(It.IsAny<Guid>())).Returns(new Game()
            {
                Level = Level.Advanced,
                TimeFrameSeconds = TimeFrameSecs,
                StartedAt = DateTime.Now.Subtract(new TimeSpan(0, 0, 40))
            });

            var result = _sut.GetNext(Guid.NewGuid(), 12);

            Assert.True(result.TimeFrameExpired);
        }

        [Fact]
        public void GetNext_ExerciseWithWrongAnswer_ReturnsResultWithInCorrectAnswer()
        {
            _mockRepo.Setup(r => r.Get(It.IsAny<Guid>())).Returns(new Game()
            {
                Level = Level.Advanced,
                A = 1,
                B = 2,
                TimeFrameSeconds = TimeFrameSecs,
                StartedAt = DateTime.Now.Subtract(new TimeSpan(0, 0, 20))
            });

            var result = _sut.GetNext(Guid.NewGuid(), 12);

            Assert.True(result.InCorrectAnswer);
        }

        [Fact]
        public void GetNext_CorrectAnswerAndUnExpired_ReturnsResultWithNextExercise()
        {
            _mockRepo.Setup(r => r.Get(It.IsAny<Guid>())).Returns(new Game()
            {
                Level = Level.Beginner,
                A = 1,
                B = 2,
                TimeFrameSeconds = TimeFrameSecs,
                StartedAt = DateTime.Now.Subtract(new TimeSpan(0, 0, 20))
            });

            var result = _sut.GetNext(Guid.NewGuid(), 3);

            Assert.NotNull(result.Game);
            Assert.True(result.Game.Level == Level.Beginner);
            Assert.True(result.Game.TimeFrameSeconds == TimeFrameSecs);
        }

        [Theory]        
        [InlineData(Level.Beginner, Level.Talented, 30)]
        [InlineData(Level.Talented, Level.Intermediate, 29)]
        [InlineData(Level.Intermediate, Level.Advanced, 28)]
        [InlineData(Level.Advanced, Level.Expert, 27)]
        public void GetNext_MovesLevelOfExerciseAsExpected(Level currentLevel, Level nextLevel, int currentLevelTimeFrame)
        {

            var exercise = new Game()
            {
                Level = currentLevel,
                A = 1,
                B = 2,
                TimeFrameSeconds = currentLevelTimeFrame,
                StartedAt = DateTime.Now.Subtract(new TimeSpan(0, 0, 20))
            };
            _mockRepo.Setup(r => r.Get(It.IsAny<Guid>())).Returns(exercise);

            GameResult result = null;
            Guid id = Guid.NewGuid();
            for (int i = 0; i < 3; i++)
            {
                result = _sut.GetNext(id, (exercise.A + exercise.B));
                exercise = result.Game;
            }

            Assert.True(result != null && result.Game.Level == nextLevel);
            Assert.True(result.Game.TimeFrameSeconds == currentLevelTimeFrame - 1);
        }
    }
}
