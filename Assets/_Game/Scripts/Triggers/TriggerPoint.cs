using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class TriggerPoint : MonoBehaviour
{
    [SerializeField] string[] _tags;
    [SerializeField] bool _isTrigger;
    public UnityEvent EventTriggerEnter;
    public UnityEvent EventTriggerExit;
    public bool IsTriggered()
    {
        return _isTrigger;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_tags.Contains(other.tag))
        {
            _isTrigger = true;
            EventTriggerEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_tags.Contains(other.tag))
        {
            _isTrigger = false;
            EventTriggerExit?.Invoke();
        }
    }
}
