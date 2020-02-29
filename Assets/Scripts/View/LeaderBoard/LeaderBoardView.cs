using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LeaderBoardView : MonoBehaviour
    {
        [SerializeField] private LayoutGroup _layout;
        [SerializeField] private GameObject _itemPrefab;

        public void UpdateUI(List<LeaderBoardItem> items)
        {
            foreach (Transform child in _layout.transform)
            {
                Destroy(child.gameObject);
            }
            items.Sort(new SortByScore<LeaderBoardItem>());
            foreach(var item in items)
            {
                var itemObj = Instantiate(_itemPrefab, _layout.transform);
                itemObj.GetComponentInChildren<Text>().text = item.name + " : " + item.score.ToString();
            }
        }

        class SortByScore<T> : IComparer<T>
            where T : LeaderBoardItem
        {
            public int Compare(T x, T y)
            {
                if (x.score < y.score)
                    return 1;
                if (x.score > y.score)
                    return -1;
                else return 0;
            }
        }
    }
}