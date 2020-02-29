
using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;

namespace Game
{
    public class LeaderBoardMediator : Mediator
    {
        private LeaderBoardProxy leaderBoardProxy = null;
        public new const string NAME = "LeaderBoardMediator";
        public LeaderBoardView leaderBoardView
        {
            get {return (LeaderBoardView)ViewComponent;}
        }

        public LeaderBoardMediator(LeaderBoardView view):base(NAME, view){}

        public override void OnRegister()
        {
            base.OnRegister();
            leaderBoardProxy = Facade.RetrieveProxy(LeaderBoardProxy.NAME) as LeaderBoardProxy;
            if (null == leaderBoardProxy)
                throw new Exception("[LeaderBoardMediator][OnRegister] " + LeaderBoardProxy.NAME + "is null!");
            UpdateUI();
        }

        public override string[] ListNotificationInterests()
        {
            var notifications = new List<string>();
            notifications.Add(GameEvent.LD_UPDATE);
            notifications.Add(GameEvent.LD_SHOW);
            return notifications.ToArray();
        }

        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case GameEvent.LD_UPDATE:
                    UpdateUI();
                    break;
                case GameEvent.LD_SHOW:
                    leaderBoardView.gameObject.SetActive(true);
                    break;
            }
        }

        private void UpdateUI()
        {
            leaderBoardView.UpdateUI((List<LeaderBoardItem>)leaderBoardProxy.leaderBoardItems);
        }
    }
}