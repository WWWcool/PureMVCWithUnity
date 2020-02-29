using System;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;

namespace Game
{
    public class StartupCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            Game game = notification.Body as Game;
            if(null == game)
                throw new Exception("[StartupCommand][Execute] Wrong notification body");

            GameData gameData = new GameData() {
                lifes = 1,
                currentLevel = 0,
                score = 0,
                levels = game.levels,
                ball = game.ball,
                racket = game.racket,
                ballRacketDelta = game.racket.position - game.ball.position
            };
            Facade.RegisterProxy(new GameProxy(gameData));
            Facade.RegisterProxy(new LevelProxy());
            Facade.RegisterProxy(new LeaderBoardProxy());

            Facade.RegisterMediator(new GameUIMediator(game.gameUIView));
            Facade.RegisterMediator(new ImpulseInputMediator(game.impulseInputView));
            Facade.RegisterMediator(new BottomMediator(game.bottomView));
            Facade.RegisterMediator(new HighScorePopupMediator(game.highScorePopupView));
            Facade.RegisterMediator(new LeaderBoardMediator(game.leaderBoardView));
        }
    }
}