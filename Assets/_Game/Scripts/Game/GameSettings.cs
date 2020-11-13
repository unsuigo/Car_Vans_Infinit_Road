using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    public float jumpBoost = 2;
    public float speed = 20;
    public float sensitivity;
    public float botSens = 10;

    public float speedFast = 2f;
    public float speedSlow = 0.5f;
}
