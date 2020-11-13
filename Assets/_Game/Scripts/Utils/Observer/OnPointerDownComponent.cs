using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Game.Utils
{
    
    public class OnPointerDownComponent : MonoBehaviour, IPointerDownHandler
    {
        public UnityEvent OnPointerDownEvent;

        public void OnPointerDown(PointerEventData eventData)
        {
            OnPointerDownEvent?.Invoke();
        }

    }

}