using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class AnimationTrigger : MonoBehaviour
{
    //[SerializeField] string _tag;
    [SerializeField] string _trigger;
    private void OnTriggerEnter(Collider other)
    {
        //if (_tag == string.Empty || other.CompareTag(_tag))
        //{
            Animator animator = other.transform.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.SetTrigger(_trigger);
            }
        //}
    }

    public void SetTrigger(string trigger)
    {
        _trigger = trigger;
    }
}
