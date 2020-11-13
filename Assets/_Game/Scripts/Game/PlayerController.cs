using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private float _roadWidth;
   [SerializeField] private float _sideSpeed;

   private float _leftX;
   private float _rightX;
   private float _centerX;

   private PlayerPositionState _positionState;
   private void Start()
   {
      Debug.Log("PlayerController start");

      _rightX = _roadWidth * 0.25f;
      _leftX = _rightX * -1;
      _centerX = 0;

      _positionState = PlayerPositionState.Left;

   }

   private void OnEnable()
   {
      ControllButton.onTap += OnTapScreen;

   }

   private void OnDisable()
   {
      ControllButton.onTap -= OnTapScreen;

   }

   public void OnTapScreen()
   {
      Debug.Log("Tap");
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