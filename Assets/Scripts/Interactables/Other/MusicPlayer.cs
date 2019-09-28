using UnityEngine;
using System;

public enum ChangeTrack { PreviousTrack, NextTrack }

/* Script Made By Daniel */
public class MusicPlayer : MonoBehaviour
{
    #region Variables
    [SerializeField] private Sound[] tracks;
    [SerializeField] private AudioSource[] speakers;

    private ParticleSystem[] particles;
    private int currentTrackNumber;
    private bool isPlaying;
    #endregion

    private void Awake()
    {
        particles = GetComponentsInChildren<ParticleSystem>();
        PlayButton.PlayEvent += PlayOnStart;
        if (tracks.Length > 0)
        {
            foreach (AudioSource audioSource in speakers)
            {
                audioSource.clip = tracks[0].GetAudioClip();
                audioSource.volume = tracks[0].GetVolume();
            }
        }
    }
    private void Update()
    {
        if (!speakers[0].isPlaying && isPlaying) NextTrack();
    }

    public void PlayAndPause()
    {
        if (speakers.Length > 0 && tracks.Length > 0)
        {
            if (!isPlaying)
            {
                foreach (AudioSource audioSource in speakers) audioSource.Play();
                foreach (ParticleSystem particle in particles) particle.Play();
            }
            else
            {
                foreach (AudioSource audioSource in speakers) audioSource.Pause();
                foreach (ParticleSystem particle in particles) particle.Stop();
            }
            isPlaying = speakers[0].isPlaying;
        }
    }

    public void ChangeSong(ChangeTrack changeTrack)
    {
        if (speakers.Length > 0 && tracks.Length > 0)
        {
            if (changeTrack == ChangeTrack.PreviousTrack)
            {
                if (currentTrackNumber > 0) currentTrackNumber--;
                else currentTrackNumber = tracks.Length - 1;
            }
            else if (changeTrack == ChangeTrack.NextTrack)
            {
                if (currentTrackNumber < tracks.Length - 1) currentTrackNumber++;
                else currentTrackNumber = 0;
            }
            foreach (AudioSource audioSource in speakers)
            {
                audioSource.clip = tracks[currentTrackNumber].GetAudioClip();
                audioSource.volume = tracks[currentTrackNumber].GetVolume();
            }
            foreach (AudioSource audioSource in speakers) audioSource.Play();
            foreach (ParticleSystem particle in particles) particle.Play();
        }
    }

    private void PlayOnStart()
    {
        if (!isPlaying) PlayAndPause();
    }

    private void NextTrack()
    {
        if (speakers.Length > 0 && tracks.Length > 0)
        {
            ChangeSong(ChangeTrack.NextTrack);
            isPlaying = true;
        }
    }

    [Serializable]
    public struct Sound
    {
        [SerializeField] private AudioClip audioClip;
        [SerializeField, Range(0, 1)] private float volume;

        public AudioClip GetAudioClip() { return audioClip; }

        public float GetVolume() { return volume; }
    }
}