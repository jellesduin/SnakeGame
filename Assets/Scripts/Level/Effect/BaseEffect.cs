using Level.Controllers;

namespace Level.Effect
{
    public abstract class BaseEffect : IEffect
    {
        protected readonly string Name;
        public readonly Effects Effect;

        protected BaseEffect(Effects effect, string name)
        {
            Name = name;
            Effect = effect;
        }

        public abstract void ApplyEffect(SnakeController snakeController);
        public abstract void ShowEffectText(UIController uiController);
        public abstract void AddScore(ScoreController scoreController);
    }
}