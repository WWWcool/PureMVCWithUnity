
using System;
using PureMVC.Patterns.Mediator;

namespace Game
{
    public class BottomMediator : Mediator
    {
        private GameProxy gameProxy = null;
        public new const string NAME = "BottomMediator";

        public BottomView inputView
        {
            get {return (BottomView)ViewComponent;}
        }

        public BottomMediator(BottomView view):base(NAME, view)
        {
            view.onTriggerEnter += OnTriggerEnter;
        }

        public override void OnRegister()
        {
            base.OnRegister();
            gameProxy = Facade.RetrieveProxy(GameProxy.NAME) as GameProxy;
            if (null == gameProxy)
                throw new Exception("[BottomMediator][OnRegister] " + GameProxy.NAME + "is null!");
        }

        public void OnTriggerEnter()
        {
            if(gameProxy.GetData().isBallRunning)
            {
                gameProxy.ReduceLife();
            }
        }
    }
}
