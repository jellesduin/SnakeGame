using System;
using Level.Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public int levelCount;
        private bool _isPaused;
        private bool _inGame;
        private LevelManager _levelManager;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            LoadCompletedLevels();
            SceneManager.LoadScene("MainMenu");
            _levelManager = FindObjectOfType<LevelManager>();
            _levelManager.LevelCount = levelCount;
        }

        private void LoadCompletedLevels()
        {
            for (var i = 1; i <= levelCount; i++)
            {
                if (!PlayerPrefs.HasKey("Level " + i))
                {
                    PlayerPrefs.SetInt("Level " + i, 0);
                }
            }
        }
        

        public void Play()
        {
            // TODO: load the next uncompleted level
            for (var i = 1; i <= levelCount; i++)
            {
                if (PlayerPrefs.GetInt("Level " + i) == 0)
                {
                    LoadLevel(i);
                    return;
                }
                
                if (i == levelCount)
                {
                    LoadLevel(1);
                    return;
                }
            }
            
            _inGame = true;
            _isPaused = false; 
        }

        public void LoadLevel(int index)
        {
            if (index < 1) return;
            
            _inGame = true;
            _isPaused = false;
            _levelManager.LoadLevel(index);
        }

        public void LoadMenu(string menuName)
        {
            _inGame = false;
            _isPaused = false;
            if (menuName is not ("MainMenu" or "LevelsMenu" or "SettingsMenu")) return;
            
            SceneManager.LoadScene(menuName);
        }

        public void PauseResume()
        {
            Debug.Log("In game: " + _inGame + " is paused: " + _isPaused + "");
            if(!_inGame) return; 
            _isPaused = !_isPaused;
            Time.timeScale = _isPaused ? 0 : 1;
            
            if (_isPaused) SceneManager.LoadScene("Paused", LoadSceneMode.Additive);
            else SceneManager.UnloadSceneAsync("Paused");
        }
    }
}