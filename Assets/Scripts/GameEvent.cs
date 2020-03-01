namespace Game
{
    public class GameEvent
    {
        // System
        public const string STARTUP = "Startup";
        public const string LOAD_LEVEL = "LoadLevel";
        public const string START_GAME = "StartGame";
        // Global
        public const string UI_UPDATE = "UIUpdate";
        public const string LD_UPDATE = "LDUpdate";
        public const string LD_SHOW = "LDShow";
        public const string GAME_OVER = "GameOver";
        public const string LEVEL_COMPLETE = "LevelComplete";
        // Game
        public const string NEW_LIFE = "NewLife";
        public const string RUN_BALL = "RunBall";
        public const string MOVE_RACKET = "MoveRacket";
        public const string ENABLE_RACKET_AND_BALL = "EnableRacketAndBall";
        public const string DISABLE_RACKET_AND_BALL = "DisableRacketAndBall";
        public const string UPDATE_BLOCK = "UpdateBlock";
        public const string REMOVE_BLOCK = "RemoveBlock";
    }
}