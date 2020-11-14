using System;
using DG.Tweening;
using TMPro;
using UnityEngine;


namespace  Game
{
    
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private GameObject _startButton;
        [SerializeField] private GameObject _fuelPanel;
        [SerializeField] private GameObject _scorePanel;
        [SerializeField] private GameObject _tapButton;
        private int _scoreTotal = 0;
  
        private void OnEnable()
        {
            StartButton();
            // CarGameManager.OnScoreCountChanged += SetScore;
            CarGameManager.sessionWinAction += WinPanel;
            CarGameManager.sessionFailAction += FailPanel;
            CarGameManager.sessionStartAction += StatePlaying;
        }

        private void OnDisable()
        {
            CarGameManager.sessionWinAction -= WinPanel;
            CarGameManager.sessionStartAction -= StatePlaying;
            CarGameManager.sessionFailAction -= FailPanel;

        }


        // private void SetScore(int score)
        // {
        //     // _scoreTotal += score;
        //     // _score.text = _scoreTotal.ToString();
        // }

        private void FailPanel()
        {
            _gameOverPanel.SetActive(true);
            _winPanel.SetActive(false);
            _startButton.SetActive(false);
            _scorePanel.SetActive(false);
            _fuelPanel.SetActive(false);
            _tapButton.SetActive(false);
            DOVirtual.DelayedCall(3, StartButton);
        }
        
        private void WinPanel()
        {
            _gameOverPanel.SetActive(false);
            _winPanel.SetActive(true);
            _startButton.SetActive(false);
            _scorePanel.SetActive(false);
            _fuelPanel.SetActive(false);
            _tapButton.SetActive(false);

            DOVirtual.DelayedCall(3, StartButton);
            
        }

        private void StartButton()
        {
            _gameOverPanel.SetActive(false);
            _winPanel.SetActive(false);
            _startButton.SetActive(true);
            _scorePanel.SetActive(false);
            _fuelPanel.SetActive(false);
            _tapButton.SetActive(false);

        }

        public void StatePlaying()
        {
            _gameOverPanel.SetActive(false);
            _winPanel.SetActive(false);
            _startButton.SetActive(false);
            _scorePanel.SetActive(true);
            _fuelPanel.SetActive(true);
            _tapButton.SetActive(true);

        }
        
    }
 
}