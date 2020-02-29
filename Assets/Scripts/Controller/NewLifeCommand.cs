using System;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;

namespace Game
{
    public class NewLifeCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            var gameProxy = Facade.RetrieveProxy(GameProxy.NAME) as GameProxy;
            if (null == gameProxy)
                throw new Exception("[NewLifeCommand][Execute] " + GameProxy.NAME + "is null!");

            var data = gameProxy.GetData();
            data.ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            data.ball.SetParent(data.racket);
            data.ball.localPosition = Vector3.zero - data.ballRacketDelta;
            data.isBallRunning = false;
        }
    }
}