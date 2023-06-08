using System;
using Level.Controllers;
using Level.Models;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    public class SnakeInfo : MonoBehaviour
    {
        private Snake _snake;
        public TextMeshProUGUI snakeLivesText;
        public TextMeshProUGUI snakeLengthText;
        public TextMeshProUGUI snakeSpeedText;

        private void Start()
        {
            var mainCamera = Camera.main;
            if (mainCamera != null) transform.position = mainCamera.ViewportToWorldPoint(new Vector3(0.1f, 0.9f, 10));
        }

        private void Update()
        {
            _snake ??= FindObjectOfType<LevelManager>().ActiveLevelController.SnakeController.GetSnake();
            
            snakeLengthText.text = "Length: " + _snake.Length;
            snakeLivesText.text = "Lives: " + _snake.Lives;
            snakeSpeedText.text = "Speed: " + _snake.SnakeSpeed.ToString("F2");
        }
    }
}