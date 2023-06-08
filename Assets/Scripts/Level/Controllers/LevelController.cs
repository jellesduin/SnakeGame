using System;
using System.Collections;
using Level.Effect;
using Level.Models;
using Manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Level.Controllers
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] GameObject[] walls = {};
        private bool _isGameOver = false;
        private bool _completed = false;
        private bool _started = false;
        public SnakeController SnakeController { get; private set; }
        public EffectController EffectController { get; private set; }
        public ScoreController ScoreController { get; private set; }
        private SoundManager _soundManager;

        private UIController _uiController;
        public UIController UIController
        {
            set
            {
                _uiController = value;
                _uiController.ShowText("Level " + _levelIndex + "\n press any key to start", 0.1f);
            }
            private get { return _uiController; }
            
        }

        private GameManager _gameManager;
        private LevelManager _levelManager;
        private LevelBuilder _levelBuilder;
        private int _levelIndex;
        private int _width;
        private int _height;
        
        [Header("Level Settings")]
        public int levelValue = 15;
        public LevelType levelType = LevelType.Score;
        public bool speedModifier = false;

        public int Score => ScoreController.Score;

        void Start()
        {
            _soundManager = FindObjectOfType<SoundManager>();
            EffectController = FindObjectOfType<EffectController>();
            SnakeController = FindObjectOfType<SnakeController>();
            _levelBuilder = GetComponent<LevelBuilder>();
            _width = _levelBuilder.width;
            _height = _levelBuilder.height;
            ScoreController = GetComponent<ScoreController>();
            SceneManager.LoadScene("LevelUI", LoadSceneMode.Additive);

            _levelManager =  FindObjectOfType<LevelManager>();
            _gameManager = FindObjectOfType<GameManager>();
            _levelManager.ActiveLevelController = this;
            _levelIndex = Convert.ToInt32(SceneManager.GetActiveScene().name.Split(' ')[1]);
            Time.timeScale = 0;
        }

        private void Update()
        {
            if (Won() && !_completed)
            {
                _soundManager.PlayWonSound();
                _completed = true;
                _levelManager.CompleteLevel();
            }
            
            if (_isGameOver && Input.anyKey)
            {
                _isGameOver = false;
                _levelManager.RestartLevel();
            }
            
            if (Input.anyKey && !_started)
            {
                _started = true;
                Time.timeScale = 1;

                _uiController.ShowStartGoal(levelType, levelValue);
            }
            if (Input.GetKeyDown(KeyCode.Escape) && !_completed && !_isGameOver)
            {
                Debug.Log("Pause");
                _gameManager.PauseResume();
            }
        }
        
        private bool Won()
        {
            switch (levelType)
            {
                case LevelType.Score:
                    if (Score >= levelValue)
                        return true;
                    break;
                case LevelType.Size:
                    if(SnakeController.GetSnake().Length >= levelValue)
                        return true;
                    break;
                case LevelType.Food:
                    if(SnakeController.FoodEaten >= levelValue)
                        return true;
                    break;
            }

            return false;
        }

        public void GameOver()
        {
            _soundManager.PlayLostSound();
            _isGameOver = true;
            _uiController.ShowText("Game Over\n press any key to restart", 2);
        }

        public void GetLevelSize(out int w, out int h)
        {
            w = _width;
            h = _height;
        }

        public GameObject[] GetWalls()
        {
            return walls;
        }
    }
}