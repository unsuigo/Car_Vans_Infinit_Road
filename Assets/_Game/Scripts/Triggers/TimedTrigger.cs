using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System.Linq;
public class TimedTrigger : MonoBehaviour
{
    [SerializeField] string[] _tags;
    [SerializeField] float _time;
    public UnityEvent EventTrigger;
    public UnityEvent EventTimePass;
    public UnityEvent EventInterapted;
    private Tween _tween;
    private void OnTriggerEnter(Collider other)
    {
        if (_tags.Contains(other.tag))
        {
            EventTrigger?.Invoke();
            _tween = DOVirtual.DelayedCall(_time, EventPausePast);
        }
    }

   
    private void EventPausePast()
    {
        EventTimePass?.Invoke();
    }

    private void EventInterupted()
    {
        EventInterapted?.Invoke();
    }
}
