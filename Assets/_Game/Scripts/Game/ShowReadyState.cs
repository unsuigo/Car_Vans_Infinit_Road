using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowReadyState : MonoBehaviour
{
    [SerializeField] private Renderer _centerRenderer;

    public void SetReady()
    {
        _centerRenderer.material.color = Color.green;
    }

    public void SetNotReady()
    {
        _centerRenderer.material.color = Color.red;

    }
}
