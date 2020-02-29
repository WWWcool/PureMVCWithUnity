using UnityEngine;

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
        public Transform ball;
        public Transform racket;
        private void Start()
        {
            (GameFacade.Instance as GameFacade).StartUp(this);
        }
    }
}
