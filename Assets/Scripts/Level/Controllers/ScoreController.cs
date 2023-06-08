using UnityEngine;

namespace Level.Controllers
{
    public class ScoreController : MonoBehaviour
    {
        public int Score { get; private set; } = 0;

        public void AddScore(int score, float speed)
        {
            var scoreToAdd = score * (speed / 10);
            Score += (int) scoreToAdd;
        }
        
        public void AddScore(int score)
        {
            Score += score;
        }
    }
}