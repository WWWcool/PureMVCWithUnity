using System;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;

namespace Game
{
    public class LevelCompleteCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            var gameProxy = Facade.RetrieveProxy(GameProxy.NAME) as GameProxy;
            if (null == gameProxy)
                throw new Exception("[LevelCompleteCommand][Execute] " + GameProxy.NAME + "is null!");

            var data = gameProxy.GetData();
            var levels = data.levels;
            var current = ++data.currentLevel;

            SendNotification(GameEvent.NEW_LIFE);
            if (current < levels.Length)
            {
                SendNotification(GameEvent.LOAD_LEVEL);
            }
            else
            {
                SendNotification(GameEvent.GAME_OVER);
            }
        }
    }
}