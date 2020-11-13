using UnityEngine;
using UnityEngine.Events;
/*
[System.Serializable]
public class UnityGameObjectEvent : UnityEvent<GameObject> { }
*/
public class EventListener : MonoBehaviour
{
    public GameEvent gEvent;
    public UnityEvent response = new UnityEvent();

    private void OnEnable()
    {
        gEvent.Register(this);
    }

    private void OnDisable()
    {
        gEvent.Unregister(this);
    }

    public void OnEventOccurs()
    {
        response.Invoke();
    }
}
