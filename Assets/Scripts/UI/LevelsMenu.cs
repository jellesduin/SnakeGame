using System.Collections.Generic;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsMenu : MonoBehaviour
{
    [SerializeField] private RectTransform container;
    [SerializeField] private Button levelsButton;
    private GameManager _gameManager;
    void Start()
    {
        var cheatMode = PlayerPrefs.GetInt("Cheats") == 1;
        _gameManager = FindObjectOfType<GameManager>();
        for (var i = 1; i <= _gameManager.levelCount; i++)
        {
            var button = Instantiate(levelsButton, container);
            var index = i;

            button.GetComponentInChildren<TextMeshProUGUI>().text = i + "";
            
            var completed = PlayerPrefs.GetInt("Level " + (index - 1)) == 1;
            if(index > 1 && !completed && !cheatMode)
            {
                button.interactable = false;
                button.GetComponent<Image>().color = Color.gray;
            }
            else
            {
                button.interactable = true;
            }
            
            
            button.onClick.AddListener(() =>
            {
                _gameManager.LoadLevel(index);
            });
            
            if (PlayerPrefs.GetInt("Level " + i) == 1)
            {
                button.GetComponent<Image>().color = Color.green;
            }
            
        }
    }
}