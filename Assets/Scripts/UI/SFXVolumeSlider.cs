using Level.Controllers;
using UnityEngine;
using UnityEngine.UI;

public class SFXVolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        slider.value = SoundManager.Instance.SfxVolume;
        slider.onValueChanged.AddListener(value => SoundManager.Instance.ChangeFXVolume(value));
    }
}