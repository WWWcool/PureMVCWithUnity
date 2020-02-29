using System;
using System.Collections.Generic;
using PureMVC.Patterns.Proxy;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class LeaderBoardItem
    {
        public string name;
        public int score;
    }

    [Serializable]
    public class LeaderBoardItems
    {
        public LeaderBoardItem[] items;
    }

    public class LeaderBoardProxy : Proxy
    {
        public new const string NAME = "LeaderBoardProxy";
        public const string LEADER_BOARD_KEY = "LeaderBoardKey";
        public IList<LeaderBoardItem> leaderBoardItems
        {
            get {return (IList<LeaderBoardItem>)base.Data;}
        }

        public LeaderBoardProxy() : base(NAME, new List<LeaderBoardItem>())
        {
            if(PlayerPrefs.HasKey(LEADER_BOARD_KEY))
            {
                var data = PlayerPrefs.GetString(LEADER_BOARD_KEY);
                var items = JsonUtility.FromJson<LeaderBoardItems>(data);
                ((List<LeaderBoardItem>)leaderBoardItems).AddRange(items.items);
            }
        }

        public bool IsScoreHigh(int score)
        {
            var res = leaderBoardItems.Count < 8;
            for (var i = 0; i < leaderBoardItems.Count; i++)
            {
                if (leaderBoardItems[i].score < score)
                {
                    res = true;
                    break;
                }
            }
            return res;
        }

        public void AddNewScore(string name, int score)
        {
            bool needUpdate = true;
            int index = -1;
            for(var i = 0; i < leaderBoardItems.Count; i++)
            {
                if(leaderBoardItems[i].score < score)
                {
                    index = i;
                    break;
                }
            }

            if(index != -1)
            {
                leaderBoardItems.Insert(index, new LeaderBoardItem(){name = name, score = score});
                if(leaderBoardItems.Count > 8)
                {
                    leaderBoardItems.RemoveAt(8);
                }
            }
            else
            {
                if(leaderBoardItems.Count < 8)
                {
                    leaderBoardItems.Add(new LeaderBoardItem(){name = name, score = score});
                }
                else
                {
                    needUpdate = false;
                }
            }

            if(needUpdate)
            {
                SendNotification(GameEvent.LD_UPDATE);
                var data = JsonUtility.ToJson(new LeaderBoardItems() { items = ((List<LeaderBoardItem>)leaderBoardItems).ToArray() });
                //Debug.Log("[LeaderBoardProxy][AddNewScore] save data: " + data);
                PlayerPrefs.SetString(LEADER_BOARD_KEY, data);
            }
        }
    }
}