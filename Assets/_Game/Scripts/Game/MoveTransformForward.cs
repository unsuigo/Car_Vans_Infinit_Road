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


   private void OnEnable()
   {
      PlayerController.onBoost += Boost;
      PlayerController.onSlow += SlowSpeed;
      
   }

   private void OnDisable()
   {
      PlayerController.onBoost -= Boost;
      PlayerController.onSlow -= SlowSpeed;

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

   private void StopMove()
   {
      SetSpeed(0);
   }

   private void StartMove()
   {
      NormalSpeed();
   }

   public void Boost()
   {
      var boostedSpeed = _moveSpeed * _boost;
      Sequence mySequence = DOTween.Sequence();
      
      mySequence.Append(DOTween.To(
         () => _moveSpeed,
         x => _moveSpeed = x,
         boostedSpeed,
         _boostTime));
      mySequence.Append(DOTween.To(
         () => _moveSpeed,
         x => _moveSpeed = x,
         _speed,
         0.9f));
      mySequence.PrependInterval(_boostTime);
      mySequence.Duration();
      
      // Tween tween = DOVirtual.DelayedCall(_boostTime, NormalSpeed);
   }
   
   
   
}
