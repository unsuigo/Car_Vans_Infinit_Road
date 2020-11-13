using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTriggerCommand : MonoBehaviour
{
    [SerializeField] string _trigger;

    public void Execute(GameObject go = null)
    {
        if (go == null) go = this.gameObject;
        Animator animator = go.GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.SetTrigger(_trigger);
        }
    }
    
}
