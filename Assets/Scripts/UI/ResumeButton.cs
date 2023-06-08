using System;
using Manager;
using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    private GameManager _gameManager;
    public void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void Resume()
    {
        _gameManager.PauseResume();        
    }
}