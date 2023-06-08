using Manager;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }
}