using Level.Controllers;

namespace Level.Effect
{
    public interface IEffect
    {
        void ApplyEffect(SnakeController snakeController);
        void ShowEffectText(UIController uiController);
        void AddScore(ScoreController scoreController);
    }
}