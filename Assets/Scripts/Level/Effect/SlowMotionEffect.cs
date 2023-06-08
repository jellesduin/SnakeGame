using System.Collections;
using Level.Controllers;
using Level.Models;
using UnityEngine;

namespace Level.Effect
{
    public class SlowMotionEffect : BaseEffect
    {

        private readonly int _minSpeedDecrease, _maxSpeedDecrease;
        private readonly float _minTime, _maxTime;

        public SlowMotionEffect(int minSpeedDecrease, int maxSpeedDecrease, float minTime, float maxTime) : base(Effects.SlowMo, "Slow Motion!")
        {
            _minSpeedDecrease = minSpeedDecrease;
            _maxSpeedDecrease = maxSpeedDecrease;
            _minTime = minTime;
            _maxTime = maxTime;
        }

        public override void ApplyEffect(SnakeController snakeController)
        {
            var snake = snakeController.GetSnake();
            var speedDecrease = Random.Range(_minSpeedDecrease, _maxSpeedDecrease);
            var time = Random.Range(_minTime, _maxTime);
            
            snakeController.StopAllCoroutines();
            snakeController.StartCoroutine(SlowMoEffectTimer(snake, time, speedDecrease));
        }

        public override void ShowEffectText(UIController uiController)
        {
            uiController.ShowEffectText(Effect);
        }

        public override void AddScore(ScoreController scoreController)
        {
            scoreController.AddScore(50);
        }

        private static IEnumerator SlowMoEffectTimer(Snake snake, float time, int speedDecrease)
        {
            snake.SnakeSpeed -= snake.SnakeSpeed * speedDecrease / 100;
            yield return new WaitForSeconds(time);
            snake.SnakeSpeed = snake.BaseSpeed;
        }

    }
}