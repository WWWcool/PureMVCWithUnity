
using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;

namespace Game
{
    public class GameUIMediator : Mediator
    {
        private GameProxy gameProxy = null;
        public new const string NAME = "GameUIMediator";
        public GameUIView gameUIView
        {
            get {return (GameUIView)ViewComponent;}
        }

        public GameUIMediator(GameUIView view):base(NAME, view){}

        public override void OnRegister()
        {
            base.OnRegister();
            gameProxy = Facade.RetrieveProxy(GameProxy.NAME) as GameProxy;
            if (null == gameProxy)
                throw new Exception("[GameUIMediator][OnRegister] " + GameProxy.NAME + "is null!");
            UpdateUI();
        }

        public override string[] ListNotificationInterests()
        {
            var notifications = new List<string>();
            notifications.Add(GameEvent.UI_UPDATE);
            return notifications.ToArray();
        }

        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case GameEvent.UI_UPDATE:
                    UpdateUI();
                    break;
            }
        }

        private void UpdateUI()
        {
            var data = gameProxy.GetData();
            gameUIView.UpdateUI(data.lifes, data.score);
        }
    }
}