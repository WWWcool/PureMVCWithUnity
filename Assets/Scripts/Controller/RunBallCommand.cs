using System;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;

namespace Game
{
    public class RunBallCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            var gameProxy = Facade.RetrieveProxy(GameProxy.NAME) as GameProxy;
            if (null == gameProxy)
                throw new Exception("[RunBallCommand][Execute] " + GameProxy.NAME + "is null!");

            var data = gameProxy.GetData();
            data.isBallRunning = true;
            data.ball.SetParent(data.racket.parent);
            var body = data.ball.GetComponent<Rigidbody2D>();
            body.AddForce((new Vector2(UnityEngine.Random.Range(-1f, 1f), 1f))*6f, ForceMode2D.Impulse);
        }
    }
}
