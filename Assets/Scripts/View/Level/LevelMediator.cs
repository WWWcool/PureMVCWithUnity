
using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;

namespace Game
{
    public class LevelMediator : Mediator
    {
        private LevelProxy levelProxy = null;
        public new const string NAME = "LevelMediator";
        public LevelView levelView
        {
            get {return (LevelView)ViewComponent;}
        }

        public LevelMediator(LevelView view):base(NAME, view)
        {
            view.onCollision += OnCollision;
        }

        public override void OnRegister()
        {
            base.OnRegister();
            levelProxy = Facade.RetrieveProxy(LevelProxy.NAME) as LevelProxy;
            if (null == levelProxy)
                throw new Exception("[LevelMediator][OnRegister] " + LevelProxy.NAME + "is null!");
        }

        public override string[] ListNotificationInterests()
        {
            var notifications = new List<string>();
            notifications.Add(GameEvent.UPDATE_BLOCK);
            notifications.Add(GameEvent.REMOVE_BLOCK);
            return notifications.ToArray();
        }

        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case GameEvent.UPDATE_BLOCK:
                    {
                        Block block = notification.Body as Block;
                        if (null == block)
                            throw new Exception("[LevelMediator][HandleNotification] Wrong notification body - " + notification.Name);
                        levelView.UpdateBlock(block);
                        break;
                    }
                case GameEvent.REMOVE_BLOCK:
                    {
                        Block block = notification.Body as Block;
                        if (null == block)
                            throw new Exception("[LevelMediator][HandleNotification] Wrong notification body - " + notification.Name);
                        levelView.RemoveBlock(block);
                        break;
                    }
            }
        }

        public void InitBlocks(LevelData data)
        {
            levelView.Clear();
            for (var i = 0; i < data.blocks.Length; i++)
            {
                levelView.AddBlock(i, data.blocks[i].toughness, data.blocks[i].position);
            }
        }

        private void OnCollision(BlockView view)
        {
            levelProxy.HitBlock(view.id);
        }
    }
}
