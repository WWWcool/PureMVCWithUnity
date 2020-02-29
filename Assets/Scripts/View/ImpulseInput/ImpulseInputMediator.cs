
using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;

namespace Game
{
    public class ImpulseInputMediator : Mediator
    {
        private GameProxy gameProxy = null;
        public new const string NAME = "ImpulseInputMediator";

        private bool _inputBlocked = false;

        public ImpulseInputView inputView
        {
            get {return (ImpulseInputView)ViewComponent;}
        }

        public ImpulseInputMediator(ImpulseInputView view):base(NAME, view)
        {
            view.onDrag += OnDrag;
            view.onPointerClick += OnPointerClick;
        }

        public override void OnRegister()
        {
            base.OnRegister();
            gameProxy = Facade.RetrieveProxy(GameProxy.NAME) as GameProxy;
            if (null == gameProxy)
                throw new Exception("[ImpulseInputMediator][OnRegister] " + GameProxy.NAME + "is null!");
        }

        public void OnPointerClick()
        {
            if(!gameProxy.GetData().isBallRunning)
            {
                SendNotification(GameEvent.RUN_BALL);
            }
        }

        public void OnDrag(Vector2 delta)
        {
            if (_inputBlocked)
                return;

            SendNotification(GameEvent.MOVE_RACKET, delta);
        }
    }
}
