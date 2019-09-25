using UnityEngine;
using System;

/* Script Made By Tåqvist. Base taken from MusicPlayer by Daniel */
[RequireComponent(typeof(AudioSource))]
public class SoundFXPlayer : MonoBehaviour
{
    [SerializeField] private Sound[] soundFxs;
    private AudioSource audioSource;

    private int currentSoundFxNumber = 0;

    //Used for testing
    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.Z))
    //    {
    //        PlaySound();
    //    }
    //}

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (soundFxs.Length > 0)
        {
            audioSource.clip = soundFxs[0].GetAudioClip();
            if(audioSource.playOnAwake == true)
            {
                audioSource.Play();
            }
        }
    }

    public void PlaySound()
    {
        currentSoundFxNumber = UnityEngine.Random.Range(0, soundFxs.Length);
        audioSource.clip = soundFxs[currentSoundFxNumber].GetAudioClip();
        audioSource.Play();
    }

    [Serializable]
    public struct Sound
    {
        [SerializeField] private AudioClip audioClip;
        public AudioClip GetAudioClip() { return audioClip; }
    }
}
