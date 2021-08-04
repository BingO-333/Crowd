using System;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        #region Singleton

        public static LevelManager Instance;

        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            Instance = this;

            TimeRemain = _gameDuration;
        }

        #endregion

        public Action OnStartGame;
        public Action OnTimeOut;

        public Vector2 MovableArea = new Vector2(40, 40);
        
        public PlayersManager PlayersManager;
        public UIManager UIManager;
        
        public float TimeRemain { get; private set; }

        [SerializeField] private int _gameDuration = 120;

        private bool _gameIsOver;

        private void Start()
        {
            PlayersManager.Player.Crowd.OnDie += (i1, i2) => _gameIsOver = true;
        }

        private void Update()
        {
            if (!_gameIsOver)
            {
                TimeRemain -= Time.deltaTime;
                if (TimeRemain <= 0) FinishTheGame();
            }
        }

        public void StartGame() => OnStartGame?.Invoke();

        public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        private void FinishTheGame()
        {
            _gameIsOver = true;
            OnTimeOut?.Invoke();
        }
    }
}