using UnityEngine;

public class CheatsButton : MonoBehaviour
{
    public void Cheats()
    {
        PlayerPrefs.SetInt("Cheats", 1);
    }
}