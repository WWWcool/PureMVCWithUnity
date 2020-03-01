using System;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;

namespace Game
{
    public class LoadLevelCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            var gameProxy = Facade.RetrieveProxy(GameProxy.NAME) as GameProxy;
            if (null == gameProxy)
                throw new Exception("[LoadLevelCommand][Execute] " + GameProxy.NAME + "is null!");

            var levelProxy = Facade.RetrieveProxy(LevelProxy.NAME) as LevelProxy;
            if (null == levelProxy)
                throw new Exception("[LoadLevelCommand][Execute] " + LevelProxy.NAME + "is null!");

            var levelMediator = Facade.RetrieveMediator(LevelMediator.NAME) as LevelMediator;
            if (null == levelMediator)
                throw new Exception("[LoadLevelCommand][Execute] " + LevelMediator.NAME + "is null!");

            var data = gameProxy.GetData();
            var levels = data.levels;
            var current = data.currentLevel;

            if(current < levels.Length)
            {
                var level = levels[current];
                levelProxy.InitBlocks(level);
                levelMediator.InitBlocks(level);
                SendNotification(GameEvent.START_GAME);
            }
            else
            {
                throw new Exception("[LoadLevelCommand][Execute] try to load not existing level! " + current.ToString());
            }
        }
    }
}