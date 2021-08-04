using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class InputController : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        #region Singleton

        public static InputController Instance;

        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            
            Instance = this;
        }

        #endregion

        public Vector2 SwipeDirection;

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.delta.magnitude < 5)
                return;
            
            SwipeDirection = eventData.delta.normalized;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            SwipeDirection = Vector2.zero;
        }
    }
}
