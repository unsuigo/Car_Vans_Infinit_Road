using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class RotationTrigger : MonoBehaviour
{
    [SerializeField] string _tag;
    [SerializeField] Vector3 _localRotation;
    [SerializeField] float _time = 0.5f;
    
    [SerializeField] UnityEvent EventEnded;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tag))
        {
            other.transform.DOLocalRotate(_localRotation, _time).OnComplete(() => {
                EventEnded?.Invoke();
            });
        }
    }
}
