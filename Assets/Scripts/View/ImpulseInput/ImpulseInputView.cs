using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class ImpulseInputView : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerClickHandler
    {
        private Vector2 _prevPoint = Vector2.zero;
        private Camera _camera;

        public Action<Vector2> onDrag;
        public Action onPointerClick;

        private void Start()
        {
            _camera = Camera.main;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //Debug.Log("[ImpulseInputView][OnPointerClick]");
            onPointerClick.Invoke();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //Debug.Log("[ImpulseInputView][OnBeginDrag]");
            _prevPoint = GetEventWorldPosition(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            //Debug.Log("[ImpulseInputView][OnDrag]");
            var currentPoint = GetEventWorldPosition(eventData);
            onDrag.Invoke(_prevPoint - currentPoint);
            _prevPoint = currentPoint;
        }

        private Vector2 GetEventWorldPosition(PointerEventData eventData)
        {
            return _camera.ScreenToWorldPoint(eventData.position);
        }
    }
}
