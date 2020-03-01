using System;
using UnityEngine;

namespace Game
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private GameObject _blockPrefab;

        public Action<BlockView> onCollision;

        public void Clear()
        {
            foreach(Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void AddBlock(int id, int toughness, Vector2 position)
        {
            var block = Instantiate(_blockPrefab, position, Quaternion.identity, transform).GetComponent<BlockView>();
            block.SetColor(toughness);
            block.id = id;
            block.onCollision += onCollision;
        }

        public void UpdateBlock(Block data)
        {
            var view = FindBlock(data.id);
            if(view != null)
            {
                view.SetColor(data.toughness);
            }
        }

        public void RemoveBlock(Block data)
        {
            var view = FindBlock(data.id);
            if (view != null)
            {
                Destroy(view.gameObject);
            }
        }

        private BlockView FindBlock(int id)
        {
            BlockView res = null;
            foreach(Transform child in transform)
            {
                var view = child.GetComponent<BlockView>();
                if(view != null)
                {
                    if(view.id == id)
                    {
                        res = view;
                        break;
                    }
                }
            }
            return res;
        }
    }
}