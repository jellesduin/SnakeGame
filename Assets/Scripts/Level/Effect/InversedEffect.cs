using Level.Controllers;

namespace Level.Effect
{
    public class InversedEffect : BaseEffect
    {
        private readonly int _duration;
        public InversedEffect(int duration) : base(Effects.Inversed, "Inversed")
        {
            _duration = duration;
        }

        public override void ApplyEffect(SnakeController snakeController)
        {
            snakeController.Invert(_duration);
        }

        public override void ShowEffectText(UIController uiController)
        {
            uiController.ShowEffectText(Effect);
        }

        public override void AddScore(ScoreController scoreController)
        {
            scoreController.AddScore(250);
        }
    }
}