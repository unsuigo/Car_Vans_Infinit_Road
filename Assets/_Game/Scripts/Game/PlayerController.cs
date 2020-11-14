using System;
using DG.Tweening;
using Game;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   public  static Action onBoost;

   [SerializeField] private float _roadWidth;
   [SerializeField] private float _sideSpeed;

   private float _leftX;
   private float _rightX;
   private float _centerX;
   private bool _isSafeToConnectFuel;

   private PlayerPositionState _positionState;
   
   private void Start()
   {
      _rightX = _roadWidth * 0.25f;
      _leftX = _rightX * -1;
      _centerX = 0;
      _positionState = PlayerPositionState.Left;
   }

   private void OnEnable()
   {
      ControllButton.onTap += OnTapScreen;
      Car.onSafeToConnectFuel += OnSafeToConnectFuel;
      Car.onUnsafeToConnectFuel += OnUnsafeToConnectFuel;
   }

   private void OnDisable()
   {
      ControllButton.onTap -= OnTapScreen;
      Car.onSafeToConnectFuel -= OnSafeToConnectFuel;
      Car.onUnsafeToConnectFuel -= OnUnsafeToConnectFuel;
   }

   private void OnSafeToConnectFuel()
   {
      _isSafeToConnectFuel = true;
   }
   
   private void OnUnsafeToConnectFuel()
   {
      _isSafeToConnectFuel = false;
   }

   private void Connection()
   {
      MoveCenter();
      DOVirtual.DelayedCall(0.5f, MoveRight);
      DOVirtual.DelayedCall(0.9f, Boost);
   }

   public void OnTapScreen()
   {
      if (_isSafeToConnectFuel)
      {
         Connection();
      }
      else
      {
         if (_positionState == PlayerPositionState.Left)
         {
            MoveCenter();
         }
         else if (_positionState == PlayerPositionState.Center)
         {
            MoveRight();
         }
         else
         {
            MoveLeft();
         }
      }
   }

   private void Boost()
   {
      onBoost?.Invoke();
   }
   private void MoveHorizontal(float dist)
   {
      transform.DOLocalMoveX(dist, _sideSpeed);
   }

   private void MoveLeft()
   {
      _positionState = PlayerPositionState.Left;
      MoveHorizontal(_leftX);
   }
   private void MoveRight()
   {
      _positionState = PlayerPositionState.Right;
      MoveHorizontal(_rightX);
   }
   
   private void MoveCenter()
   {
      _positionState = PlayerPositionState.Center;
      MoveHorizontal(_centerX);
   }
   
}

enum PlayerPositionState
{
   Left,
   Right,
   Center
}