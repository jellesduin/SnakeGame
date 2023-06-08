using System;
using Level.Controllers;
using Level.Models;
using Manager;
using TMPro;
using UnityEngine;

namespace Level
{
    public class Goal : MonoBehaviour
    {
        private LevelController _levelController;
        public TextMeshProUGUI goalText;
        private void Start()
        {
            _levelController = FindObjectOfType<LevelManager>().ActiveLevelController;
            goalText.text = "Reach " + _levelController.levelValue +  " " + Enum.GetName(typeof(LevelType), _levelController.levelType) + " to win!";
        }
    }
}