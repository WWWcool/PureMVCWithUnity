using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BottomView : MonoBehaviour
    {
        public Action onTriggerEnter;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            onTriggerEnter.Invoke();
        }
    }
}
