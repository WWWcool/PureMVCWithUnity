using System.Collections.Generic;
using PureMVC.Patterns.Proxy;
using UnityEngine;

namespace Game
{
    public class GameData
    {
        public int lifes;
        public int currentLevel;
        public int score;
        public LevelData[] levels;
        public bool isBallRunning = false;
        public Transform ball;
        public Transform racket;
        public Vector3 ballRacketDelta;
    }

    public class GameProxy : Proxy
    {
        public new const string NAME = "GameProxy";
        public GameData gameData
        {
            get {return (GameData)base.Data;}
        }

        public GameProxy(GameData data) : base(NAME, data){}

        public GameData GetData()
        {
            return gameData;
        }

        public void AddScore(int value)
        {
            gameData.score += value;
            SendNotification(GameEvent.UI_UPDATE);
        }

        public void ReduceLife()
        {
            gameData.lifes -= 1;
            SendNotification(GameEvent.UI_UPDATE);
            if (gameData.lifes <= 0)
            {
                GameFacade.Instance.RemoveCommand(GameEvent.RUN_BALL);
                GameFacade.Instance.RemoveCommand(GameEvent.MOVE_RACKET);
                SendNotification(GameEvent.NEW_LIFE);
                SendNotification(GameEvent.GAME_OVER);
            }
            else
            {
                SendNotification(GameEvent.NEW_LIFE);
            }
        }
    }
}