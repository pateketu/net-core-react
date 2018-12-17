namespace WebApi.Model
{
    public class ExerciseResult
    {
        public bool InCorrectAnswer { get; set; }
        public Exercise Exercise { get; set; }
        public bool AllLevelsFinished {get;set;}
        public bool TimeFrameExpired { get; set; }
    }
}
