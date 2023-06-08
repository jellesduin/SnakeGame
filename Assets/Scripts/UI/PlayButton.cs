using System;
using Manager;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    private GameManager _gameManager;
    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }
    
    public void Play()
    {
        _gameManager.Play();
    }
}
