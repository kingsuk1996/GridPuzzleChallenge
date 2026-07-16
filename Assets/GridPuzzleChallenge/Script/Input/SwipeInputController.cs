
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GridPulse
{
    public sealed class SwipeInputController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Swipe Settings")]
        [SerializeField, Min(10f)]
        private float minimumSwipePixels = 48f;

        private Vector2 _pointerStart;

        public event Action<Direction> DirectionRequested;

        public void OnPointerDown(PointerEventData eventData)
        {
            _pointerStart = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Vector2 delta = eventData.position - _pointerStart;

            if (TryGetDirection(delta, out Direction direction))
            {
                DirectionRequested?.Invoke(direction);
            }
        }

#if UNITY_EDITOR || UNITY_STANDALONE
        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.UpArrow) || UnityEngine.Input.GetKeyDown(KeyCode.W))
            {
                DirectionRequested?.Invoke(Direction.Up);
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.DownArrow) || UnityEngine.Input.GetKeyDown(KeyCode.S))
            {
                DirectionRequested?.Invoke(Direction.Down);
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow) || UnityEngine.Input.GetKeyDown(KeyCode.A))
            {
                DirectionRequested?.Invoke(Direction.Left);
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.RightArrow) || UnityEngine.Input.GetKeyDown(KeyCode.D))
            {
                DirectionRequested?.Invoke(Direction.Right);
            }
        }
#endif

        private bool TryGetDirection(Vector2 delta, out Direction direction)
        {
            direction = Direction.Up;

            if (delta.magnitude < minimumSwipePixels)
                return false;

            delta.Normalize();

            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                direction = delta.x > 0f
                    ? Direction.Right
                    : Direction.Left;
            }
            else
            {
                direction = delta.y > 0f
                    ? Direction.Up
                    : Direction.Down;
            }

            return true;
        }

        public void RequestDirection(Direction direction)
        {
            DirectionRequested?.Invoke(direction);
        }
    }
}