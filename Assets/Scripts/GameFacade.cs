using PureMVC.Interfaces;
using PureMVC.Patterns.Facade;
using UnityEngine;

namespace Game
{
    public class GameFacade : Facade
    {
        public static IFacade Instance
        {
            get
            {
                if (null == instance)
                {
                    Debug.Log("[GameFacade][Instance] new Facade");
                    instance = new GameFacade();
                }
                return instance;
            }
        }

        public void StartUp(Game game)
        {
            Debug.Log("[GameFacade][StartUp]");
            SendNotification(GameEvent.STARTUP , game);
        }

        protected override void InitializeFacade()
        {
            base.InitializeFacade();
        }

        protected override void InitializeView()
        {
            base.InitializeView();
        }

        protected override void InitializeController()
        {
            base.InitializeController();
            RegisterCommand(GameEvent.STARTUP, () => { /*Debug.Log("[GameFacade][StartupCommand]");*/ return new StartupCommand(); });
            RegisterCommand(GameEvent.NEW_LIFE, () => { /*Debug.Log("[GameFacade][NewLifeCommand]");*/ return new NewLifeCommand(); });
            RegisterCommand(GameEvent.RUN_BALL, () => { /*Debug.Log("[GameFacade][RunBallCommand]");*/ return new RunBallCommand(); });
            RegisterCommand(GameEvent.MOVE_RACKET, () => { /*Debug.Log("[GameFacade][MoveRacketCommand]");*/ return new MoveRacketCommand(); });
        }

        protected override void InitializeModel()
        {
            base.InitializeModel();
        }
    }
}