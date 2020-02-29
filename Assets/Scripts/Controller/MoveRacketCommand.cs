using System;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;

namespace Game
{
    public class MoveRacketCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            if (null == notification.Body)
                throw new Exception("[MoveRacketCommand][Execute] Wrong notification body");
            var gameProxy = Facade.RetrieveProxy(GameProxy.NAME) as GameProxy;
            if (null == gameProxy)
                throw new Exception("[MoveRacketCommand][Execute] " + GameProxy.NAME + "is null!");

            var data = gameProxy.GetData();
            if(data.isBallRunning)
            {
                Vector2 delta = (Vector2)notification.Body;
                delta.y = 0;
                data.racket.position = Clamp(
                    data.racket.position - (Vector3)delta,
                    new Vector3(-2f, -10f, 0f),
                    new Vector3(2f, 10f, 0f)
                );
            }
        }

        private Vector3 Clamp(Vector3 value, Vector3 min, Vector3 max)
        {
            return new Vector3(
                Mathf.Clamp(value.x, min.x, max.x),
                Mathf.Clamp(value.y, min.y, max.y),
                Mathf.Clamp(value.z, min.z, max.z)
            );
        }
    }
}