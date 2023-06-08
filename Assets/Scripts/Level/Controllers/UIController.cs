using Level.Effect;
using Level.Models;
using Manager;
using TMPro;
using UnityEngine;

namespace Level.Controllers
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject textPrefab;

        private Canvas _canvas;
        private LevelManager _levelManager;
        private void Start()
        {
            _canvas = GetComponent<Canvas>();
            _levelManager = FindObjectOfType<LevelManager>();
            _levelManager.ActiveUIController = this;
        }

        public void ShowText(string text, float duration = 3, int fontSize = 110, Color color = default, float alpha = 1)
        {
            if (color == default) color = Color.white;

            var textObject = Instantiate(textPrefab, _canvas.transform);
            var textMeshPro = textObject.GetComponent<TextMeshProUGUI>();
            textMeshPro.text = text;
            textMeshPro.fontSize = fontSize;
            textMeshPro.color = color;
            textMeshPro.alpha = alpha;

            Destroy(textObject, duration);
        }

        public void ShowStartGoal(LevelType levelType, int levelValue)
        {
            switch (levelType)
            {
                case LevelType.Score:
                    ShowText("Reach a score of " + levelValue + "!", 1, 110, default, .5f);
                    break;
                case LevelType.Size:
                    ShowText("Reach snake size of " + levelValue + "!", 1, 110, default, .5f);
                    break;
                case LevelType.Food:
                    ShowText("Eat " + levelValue + " apples!", 1, 110, default, .5f);
                    break;
            }
        }

        public void ShowEffectText(Effects effect)
        {
            switch (effect)
            {
                case Effects.SpeedBoost:
                    ShowText("Speed Boost!", 1, 110, Color.green, .5f);
                    break;
                case Effects.Shrink:
                    ShowText("Shrink!", 1, 110, Color.red, .5f);
                    break;
                case Effects.Inversed:
                    ShowText("Inversed!", 1, 110, Color.blue, .5f);
                    break;
            }
        }
    }
}