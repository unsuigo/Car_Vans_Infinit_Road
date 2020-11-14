using System;
using TMPro;
using UnityEngine;


namespace Game
{
    public class CarGameManager : MiniGames
    {
        public static event Action sessionFailAction;
        public static event Action sessionWinAction;
        public static event Action sessionStartAction;
        public static Action<int> OnScoreCountChanged;
        
        [SerializeField] private GameObject _timerPanel;
        [SerializeField] private GameObject _playerCarPrefab;
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private int _scoreToWin;
        private int ScoreCount { get; set; }
        // private int _scoreCollectedQty = 0;
        private Transform _player;
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
          
            ScoreCount = 0;
        }
        
        
        //Override
        public override void Init()
        {
            _player = Instantiate(_playerCarPrefab).transform;
            _player.position = new Vector3(-2.116f,0,10);
            ScoreCount = 0;
            SetScoreText(ScoreCount);
            _timerPanel.SetActive(true);
            CurrentGameState = GameState.Playing;
            WorldTileManager.Instance.ResetWorld();
        }
        
        public override void Fail()
        {
            base.Fail();
            GameSessionFail();
            Debug.Log("FAIL");
        }
        
        public override void Win()
        {
            base.Win();
            Debug.Log("WIN");
            Destroy(_player.gameObject);
            CurrentGameState = GameState.None;
            LocalSettings.StarsGotQty = ScoreCount;
            sessionWinAction?.Invoke();            
        }

        public void StartGameSession()
        {
           Init();
            
            sessionStartAction?.Invoke();
            Debug.Log("StartGameSession?????????????");
        }
        
        public void GameSessionFail()
        {
            // AudioSystem.Instance.PlayOneShot(AudioClips.GameEnd);

            // _timerPanel.SetActive(false);
           
            sessionFailAction?.Invoke();
            Destroy(_player.gameObject);
            CurrentGameState = GameState.None;
            LocalSettings.StarsGotQty = ScoreCount;
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