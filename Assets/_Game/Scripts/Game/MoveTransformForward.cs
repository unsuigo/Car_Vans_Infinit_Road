using DG.Tweening;
using UniRx;
using UnityEngine;

public class MoveTransformForward : MonoBehaviour
{
   [SerializeField] private float _speed;
   [SerializeField] private float _boostTime;
   [SerializeField] private float _boost;
   private float _moveSpeed;
   private void Start()
   {
      NormalSpeed();
      //_____Update_____
      Observable.EveryUpdate()
         .Subscribe(x => MoveForward())
         .AddTo(this);
   }

   private void  MoveForward()
   {
      transform.Translate(Vector3.forward * Time.deltaTime * _moveSpeed);
   }

   public void SetSpeed(float speed)
   {
      _moveSpeed = speed;
   }

   public void NormalSpeed()
   {
      SetSpeed(_speed);
   }

   public void FastSpeed()
   {
      SetSpeed(_speed * _boost);
   }

   public void SlowSpeed()
   {
      SetSpeed(_speed * 0.8f);
   }

   public void Boost()
   {
      FastSpeed();
      Tween tween = DOVirtual.DelayedCall(_boostTime, NormalSpeed);
   }
   
   
   
}
