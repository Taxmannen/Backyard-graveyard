using UnityEngine;
using System;

public enum ChangeTrack { PreviousTrack, NextTrack }

/* Script Made By Daniel */
[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    #region Variables
    [SerializeField] private Sound[] tracks;

    private AudioSource audioSource;
    private int currentTrackNumber;
    private bool isPlaying;
    #endregion

    private void Awake()
    {
        PlayButton.PlayEvent += PlayOnStart;
        audioSource = GetComponent<AudioSource>();
        if (tracks.Length > 0)
        {
            audioSource.clip = tracks[0].GetAudioClip();
            audioSource.volume = tracks[0].GetVolume();
        }
    }
    private void Update()
    {
        if (!audioSource.isPlaying && isPlaying) NextTrack();
    }

    public void PlayAndPause()
    {
        if (tracks.Length > 0)
        {
            if (!audioSource.isPlaying) audioSource.Play();
            else                        audioSource.Pause();
            isPlaying = audioSource.isPlaying;

        }
    }

    public void ChangeSong(ChangeTrack changeTrack)
    {
        if (tracks.Length > 0)
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
            audioSource.clip = tracks[currentTrackNumber].GetAudioClip();
            audioSource.volume = tracks[currentTrackNumber].GetVolume();
            audioSource.Play();
        }
    }

    private void PlayOnStart()
    {
        if (!isPlaying) PlayAndPause();
    }

    private void NextTrack()
    {
        if (tracks.Length > 0)
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