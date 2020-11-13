using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    
    public class TimeProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _progressImage;
        [SerializeField] private TextMeshProUGUI _fuelLeftText;
        [SerializeField] private float _disabledAlpha;
        [SerializeField] private string _secondsPrefix = "s";

        private bool _lockedByLevel = false;
    
        public float Progress { get; private set; }


        private void OnEnable()
        {
            CarGameManager.sessionStartAction += StartSession;
            Car.onFuelChange += SetProgress;
        }

        private void OnDisable()
        {
            CarGameManager.sessionStartAction -= StartSession;
            Car.onFuelChange -= SetProgress;
            
        } 

        public void SetProgress(float progress)
        {
            _progressImage.fillAmount = 
                Progress = progress;

            SetFuelLeft(progress);
        }

        public void SetProgressEnabled(bool isEnabled)
        {
            _fuelLeftText.gameObject.SetActive(isEnabled);
        }
    
        private void SetImageFade(bool isDisabled)
        {
            _progressImage.DOFade(isDisabled
                    ? _disabledAlpha
                    : 1f, 
                0f);
        }
    
        private void SetFuelLeft(float leftFuel)
        {
            string f = leftFuel.ToString("F2");
            _fuelLeftText.text = $"{f}{_secondsPrefix}";
        }
        void StartSession()
        {
            
        }
    }
   
}