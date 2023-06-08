using Manager;
using UnityEngine;

public class NextLevelButton : MonoBehaviour
{
    private LevelManager _levelManager;
    private void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        
        if(_levelManager.CurrentLevel >= _levelManager.LevelCount)
            gameObject.SetActive(false);
    }
    
    public void NextLevel()
    {
        _levelManager.NextLevel();
    }
}