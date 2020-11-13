using System;
using TMPro;
using UnityEngine;


namespace Game
{
    public class CarGameManager : MiniGames
    {
        public static event Action sessionDoneAction;
        public static event Action sessionStartAction;
        public static Action<int> OnScoreCountChanged;
        
        [SerializeField] private GameObject _timerPanel;
        [SerializeField] private TextMeshProUGUI _score;
        private int ScoreCount { get; set; }
        // private int _scoreCollectedQty = 0;

        public GameState CurrentGameState{ get; set;}
        
        private void OnEnable()
        {
            Car.onScore += AddScore;
            Car.onDamageReceived += OnDamage;
            Car.onFuelFinished += Fail;
        }

        private void OnDisable()
        {
            Car.onScore -= AddScore;
        }

        void Start()
        {
            Application.targetFrameRate = 60;
            CurrentGameState = GameState.None;
            _timerPanel.SetActive(true);
            // AudioSystem.Instance.PlayMusic(AudioClips.MainTrack);
            ScoreCount = 0;
        }
        
        
        public override void Init()
        {
            //Load Level
        }
        
        public override void Fail()
        {
            base.Fail();
            GameSessionDone();
            Debug.Log("FAIL");
        }
        
        public override void Win()
        {
            base.Win();
            Debug.Log("WIN");

        }

        public void StartGameSession()
        {
            ScoreCount = 0;
            SetScoreText(ScoreCount);
            _timerPanel.SetActive(true);
            CurrentGameState = GameState.Playing;
            
            sessionStartAction?.Invoke();
        }
        
        public void GameSessionDone()
        {
            AudioSystem.Instance.PlayOneShot(AudioClips.GameEnd);

            // _timerPanel.SetActive(false);
           
            sessionDoneAction?.Invoke();
            CurrentGameState = GameState.None;
            LocalSettings.StarsGotQty = ScoreCount;
            
            sessionDoneAction?.Invoke();
        }
       
        public void GameSessionPaused()
        {
            CurrentGameState = GameState.Standby;
        }

        public void GameSessionResumed()
        {
            CurrentGameState = GameState.Playing;

        }


        #region Score

        public void ScoreCollected(int score)
        {
            SetScoreText(score);
        }

        void SetScoreText(int score)
        {
            _score.text = $"{score}";
        }
        
        public void AddScore(int count)
        {
            if (count > 0)
            {
                ScoreCount += count;
            }

            if (ScoreCount > 2)
            {
                Win();
                
            }
            SetScoreText(ScoreCount);
            OnScoreCountChanged?.Invoke(ScoreCount);
        }

        #endregion

        private void OnDamage(float damage)
        {
            Fail();
        }
    }

    public enum GameState
    {
        Playing,
        Standby,
        None
    }
}