using Manager;
using UnityEngine;

public class LoadSceneButton : MonoBehaviour
{
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }
        
    public void LoadMenu(string menuName)
    {
        _gameManager.LoadMenu(menuName);
    }
}