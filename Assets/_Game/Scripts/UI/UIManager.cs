using System;
using TMPro;
using UnityEngine;


namespace  Game
{
    
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _score;
        private int _scoreTotal = 0;
  
        private void OnEnable()
        {
            // Car.onScore += SetScore;
            CarGameManager.OnScoreCountChanged += SetScore;
        }

        private void OnDisable() => CarGameManager.OnScoreCountChanged -= SetScore;
        // private void OnDisable() => Car.onScore -= SetScore;

        private void SetScore(int score)
        {
            // _scoreTotal += score;
            // _score.text = _scoreTotal.ToString();
        }
    }
 
}