
using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;

namespace Game
{
    public class HighScorePopupMediator : Mediator
    {
        private GameProxy gameProxy = null;
        private LeaderBoardProxy leaderBoardProxy = null;
        public new const string NAME = "HighScorePopupMediator";
        public HighScorePopupView highScorePopupView
        {
            get {return (HighScorePopupView)ViewComponent;}
        }

        public HighScorePopupMediator(HighScorePopupView view):base(NAME, view)
        {
            view.onClick += OnClick;
        }

        public override void OnRegister()
        {
            base.OnRegister();
            leaderBoardProxy = Facade.RetrieveProxy(LeaderBoardProxy.NAME) as LeaderBoardProxy;
            if (null == leaderBoardProxy)
                throw new Exception("[HighScorePopupMediator][OnRegister] " + LeaderBoardProxy.NAME + "is null!");
            gameProxy = Facade.RetrieveProxy(GameProxy.NAME) as GameProxy;
            if (null == gameProxy)
                throw new Exception("[HighScorePopupMediator][OnRegister] " + GameProxy.NAME + "is null!");
        }

        public override string[] ListNotificationInterests()
        {
            var notifications = new List<string>();
            notifications.Add(GameEvent.GAME_OVER);
            return notifications.ToArray();
        }

        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case GameEvent.GAME_OVER:
                    var score = gameProxy.GetData().score;
                    if(leaderBoardProxy.IsScoreHigh(score))
                    {
                        highScorePopupView.Show(score);
                    }
                    break;
            }
        }

        public void OnClick(string name)
        {
            leaderBoardProxy.AddNewScore(name, gameProxy.GetData().score);
            SendNotification(GameEvent.LD_SHOW);
        }
    }
}