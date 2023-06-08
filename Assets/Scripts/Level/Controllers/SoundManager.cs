using System;
using UnityEngine;

namespace Level.Controllers
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;
        [SerializeReference] private AudioClip deathSound;
        [SerializeReference] private AudioClip wonSound;
        [SerializeReference] private AudioClip eatSound;
        
        
        [SerializeReference] private AudioSource sfxSource;
        [SerializeReference] private AudioSource backgroundMusic;

        public float SfxVolume
        {
            get => PlayerPrefs.GetFloat("SFXVolume", 1f);
            private set => PlayerPrefs.SetFloat("SFXVolume", value);
        }

        public float MusicVolume
        {
            get => PlayerPrefs.GetFloat("MusicVolume", 1f);
            private set => PlayerPrefs.SetFloat("MusicVolume", value);
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
            
            sfxSource.volume = SfxVolume;
            backgroundMusic.volume = MusicVolume;
        }


        private void Play(AudioClip clip)
        {
            sfxSource.PlayOneShot(clip);
        }
        
        public void PlayLostSound()
        {
            Play(deathSound);
        }
        
        public void PlayWonSound()
        {
            Play(wonSound);
        }
        
        public void PlayEatSound()
        {
            Play(eatSound);
        }

        public void ChangeFXVolume(float volume)
        {
            sfxSource.volume = volume;
            SfxVolume = volume;
        }
        
        public void ChangeMusicVolume(float volume)
        {
            backgroundMusic.volume = volume;
            MusicVolume = volume;
        }
    }
}