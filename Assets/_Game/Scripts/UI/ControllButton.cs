using UnityEngine;
using UnityEngine.EventSystems;

public class ControllButton : MonoBehaviour , IPointerDownHandler
{
    public delegate void ActionTap();
    public static event ActionTap onTap;
    
    
    public void OnPointerDown(PointerEventData eventData)
    {
        onTap?.Invoke();
    }
    
}
