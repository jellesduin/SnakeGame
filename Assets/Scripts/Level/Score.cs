using System;
using Level.Controllers;
using Level.Models;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    public class Score : MonoBehaviour
    {
        private LevelController _levelController;
        public TextMeshProUGUI scoreText;

        private void Start()
        {
            _levelController = FindObjectOfType<LevelManager>().ActiveLevelController;
        }

        private void Update()
        {
            scoreText.text = "Score: " + _levelController.Score;
        }
    }
}