using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class BlockView : MonoBehaviour
    {
        [SerializeField] private Color[] _colors;

        public int id = -1;
        public Action<BlockView> onCollision;

        public void SetColor(int index)
        {
            if(index < _colors.Length)
            {
                var sprite = GetComponent<SpriteRenderer>();
                sprite.color = _colors[index];
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            onCollision.Invoke(this);
        }
    }
}