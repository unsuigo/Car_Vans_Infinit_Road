using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Game.Utils;
using UnityEngine;

namespace  Game
{
    
    public class VCameraManager : SingletonT<VCameraManager>
    {
        [SerializeField] private CinemachineVirtualCamera vCameraPlayer;

       public void ConnectToPlayer(Transform player)
        {
            vCameraPlayer.Follow = player;
            vCameraPlayer.LookAt = player;
        }
    }
    
}