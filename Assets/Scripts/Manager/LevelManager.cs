using System;
using Level.Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class LevelManager : MonoBehaviour
    {
        public LevelController ActiveLevelController { get; set; }

        private UIController _activeUIController;
        public UIController ActiveUIController
        {
            get => _activeUIController;
            set
            {
                _activeUIController = value;
                ActiveLevelController.UIController = value;
            }
        }

        public int CurrentLevel { get; private set; }
        public int LevelCount { get; set; }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
        
        public void LoadLevel(int level)
        {
            CurrentLevel = level;
            SceneManager.LoadScene("Level " + level);
        }
        
        public void NextLevel()
        {
            if (CurrentLevel >= LevelCount) return;
            LoadLevel(CurrentLevel + 1);
        }
        
        public void CompleteLevel()
        {
            Time.timeScale = 0;

            PlayerPrefs.SetInt("Level " + CurrentLevel, 1);
            SceneManager.UnloadSceneAsync("LevelUI");
            SceneManager.LoadScene("LevelCompleted", LoadSceneMode.Additive);
        }
        
        public void RestartLevel()
        {
            SceneManager.UnloadSceneAsync("LevelUI");
            SceneManager.LoadScene("Level " + CurrentLevel);
        }
    }
}