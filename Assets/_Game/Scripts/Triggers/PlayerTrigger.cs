using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] string _tag = "Player";
    public UnityEvent EventTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == _tag)
        {
            EventTrigger?.Invoke();
        }
    }
}
