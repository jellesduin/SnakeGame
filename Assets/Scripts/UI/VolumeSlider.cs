using System;
using Level.Controllers;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        slider.value = SoundManager.Instance.MusicVolume;
        slider.onValueChanged.AddListener(value => SoundManager.Instance.ChangeMusicVolume(value));
    }
}