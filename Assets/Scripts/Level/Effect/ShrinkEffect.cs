using Level.Controllers;

namespace Level.Effect
{
    public class ShrinkEffect : BaseEffect
    {
        public ShrinkEffect() : base(Effects.Shrink, "Shrink")
        {
        }

        public override void ApplyEffect(SnakeController snakeController)
        {
            snakeController.Shrink();
        }

        public override void ShowEffectText(UIController uiController)
        {
            uiController.ShowEffectText(Effect);
        }

        public override void AddScore(ScoreController scoreController)
        {
            scoreController.AddScore(50);
        }
    }
}