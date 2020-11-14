using System;
using System.Collections;
using UnityEngine;


namespace Game 
{
   
public class Car : MonoBehaviour
{
   public delegate void OnScoreReceived(int currentScore);
   public static event OnScoreReceived onScore;
   
   public  static Action<float> onDamageReceived;
   public  static Action<float> onFuelChange;
   public  static Action onFuelFinished;
   public  static Action onSafeToConnectFuel;
   public  static Action onUnsafeToConnectFuel;
   
   [SerializeField] private ShowReadyState _showReadyState;
   [SerializeField] private float _useFuelSpeed;

   private bool _isMoving;
   private bool _canMoveUnderVan;
   private float _fuel = 0;

   private void OnEnable()
   {
      VCameraManager.Instance.ConnectToPlayer(transform);
   }

   private void Start()
   {
      _showReadyState.gameObject.SetActive(false);
      RefillFuel();
      StartCoroutine(UseFuel());
   }

   void RefillFuel()
   {
      _fuel = 1;
      onFuelChange?.Invoke(_fuel);
   }

   IEnumerator UseFuel()
   {
      while (_fuel > 0)
      {
         _fuel -= _useFuelSpeed;
         onFuelChange?.Invoke(_fuel);
      yield return new WaitForSeconds(1);
      }

      FuelFinished();
   }

   private void FuelFinished()
   {
      onFuelFinished?.Invoke();
   }
  
   public void SafePlaceUnderVan()
   {
      Debug.Log("SafePlaceUnderVan");
      _showReadyState.SetReady();
      onSafeToConnectFuel?.Invoke();
   }
   
   public void NotSafePlaceUnderVan()
   {
      Debug.Log("NOT SafePlaceUnderVan");
      _showReadyState.SetNotReady();
      onUnsafeToConnectFuel?.Invoke();
   }

   public void ShowReadyState()
   {
      _showReadyState.gameObject.SetActive(true);
   }
   
   public void HideReadyState()
   {
      _showReadyState.gameObject.SetActive(false);
   }

   public void DamageReceived(float damage)
   {
      onDamageReceived?.Invoke(damage);
      Debug.Log("car damage");
   }

   public void FuelReceived(int score)
   {
      RefillFuel();
      onScore?.Invoke(score);
      // onScore(score);
   }
   
   
   
}

}