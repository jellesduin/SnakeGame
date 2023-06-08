using System.Collections.Generic;
using Manager;
using UnityEngine;

namespace Level.Effect
{
    public class EffectController : MonoBehaviour
    {
        private readonly Dictionary<Effects, IEffect> _effects = new();
        private LevelManager _levelManager;
        private void Start()
        {
            _levelManager = FindObjectOfType<LevelManager>();
            
            
            var speedEffect = new SpeedEffect(15, 50, 2, 5);
            var shrinkEffect = new ShrinkEffect();
            var invertEffect = new InversedEffect(2);
            
            _effects.Add(invertEffect.Effect, invertEffect);
            _effects.Add(shrinkEffect.Effect, shrinkEffect);
            _effects.Add(speedEffect.Effect, speedEffect);
        }

        /**
         * Apply specific effect
         * @param effect
         */
        public void ApplyEffect(Effects effect)
        {
            var eff = _effects[effect];
            
            eff.ApplyEffect(_levelManager.ActiveLevelController.SnakeController);
            eff.ShowEffectText(_levelManager.ActiveUIController);
            eff.AddScore(_levelManager.ActiveLevelController.ScoreController);
        }
    }
}
