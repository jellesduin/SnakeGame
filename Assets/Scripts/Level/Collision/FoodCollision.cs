using System;
using Level.Controllers;
using Level.Effect;
using Level.Spawner;
using Manager;
using UnityEngine;

namespace Level.Collision
{
    public class FoodCollision : MonoBehaviour
    {
        private EffectController _effectController;
        private LevelManager _levelManager;
        private LevelController _levelController;
        private SnakeController _snakeController;
        private ScoreController _scoreController;
        private FoodSpawner _foodSpawner;
        private SoundManager _soundManager;
        
        private void Start()
        {
            _soundManager = FindObjectOfType<SoundManager>();
            _levelManager = FindObjectOfType<LevelManager>();
            _levelController = _levelManager.ActiveLevelController;
            _snakeController = _levelController.SnakeController;
            _scoreController = _levelController.ScoreController;
            _effectController = _levelController.EffectController;
            _foodSpawner = transform.parent.GetComponent<FoodSpawner>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Snake"))
            {
                _soundManager.PlayEatSound();
                _snakeController.Grow();
                _scoreController.AddScore(100, _snakeController.GetSnake().SnakeSpeed);
                _foodSpawner.Spawn();
                
                if(_levelController.speedModifier)
                    _effectController.ApplyEffect(Effects.SpeedBoost);

                Destroy(gameObject);
            }
        }
    }
}