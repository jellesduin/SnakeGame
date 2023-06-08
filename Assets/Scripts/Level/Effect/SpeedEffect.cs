using System.Collections;
using Level.Controllers;
using Level.Models;
using UnityEngine;

namespace Level.Effect
{
    public class SpeedEffect : BaseEffect
    {

        private readonly int _minSpeedIncrease, _maxSpeedIncrease;
        private readonly float _minTime, _maxTime;

        public SpeedEffect(int minSpeedIncrease, int maxSpeedIncrease, float minTime, float maxTime) : base(Effects.SpeedBoost, "Speed up!")
        {
            _minSpeedIncrease = minSpeedIncrease;
            _maxSpeedIncrease = maxSpeedIncrease;
            _minTime = minTime;
            _maxTime = maxTime;
        }

        public override void ApplyEffect(SnakeController snakeController)
        {
            var snake = snakeController.GetSnake();
            var speedIncrease = Random.Range(_minSpeedIncrease, _maxSpeedIncrease);
            var time = Random.Range(_minTime, _maxTime);
            
            snakeController.StopAllCoroutines();
            snakeController.StartCoroutine(SpeedEffectTimer(snake, time, speedIncrease));
        }

        public override void ShowEffectText(UIController uiController)
        {
            uiController.ShowEffectText(Effect);
        }

        public override void AddScore(ScoreController scoreController) {}

        private static IEnumerator SpeedEffectTimer(Snake snake, float time, int speedIncrease)
        {
            snake.SnakeSpeed += snake.SnakeSpeed * speedIncrease / 100;
            yield return new WaitForSeconds(time);
            snake.SnakeSpeed = snake.BaseSpeed;
        }

    }
}