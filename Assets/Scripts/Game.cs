using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class Game : MonoBehaviour
    {
        public LevelData[] levels;
        public ImpulseInputView impulseInputView;
        public GameUIView gameUIView;
        public BottomView bottomView;
        public LeaderBoardView leaderBoardView;
        public HighScorePopupView highScorePopupView;
        public LevelView levelView;
        public Transform ball;
        public Transform racket;
        public int lifes = 2;
        private void Start()
        {
            (GameFacade.Instance as GameFacade).StartUp(this);
        }
    }
}
