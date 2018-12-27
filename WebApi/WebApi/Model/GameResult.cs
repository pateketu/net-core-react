namespace WebApi.Model
{
    public class GameResult
    {
        public bool InCorrectAnswer { get; set; }
        public Game Game { get; set; }
        public bool AllLevelsFinished {get;set;}
        public bool TimeFrameExpired { get; set; }
    }
}
