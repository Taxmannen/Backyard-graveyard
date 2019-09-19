﻿using UnityEngine;
using System;

/* Script Made By Daniel */
public enum Direction { Left, Right }

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private Sound[] tracks;

    private AudioSource audioSource;
    private int currentTrackNumber;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = tracks[0].GetAudioClip();
        audioSource.volume = tracks[0].GetVolume();
        audioSource.Play();
    }

    public void PlayAndPause()
    {
        if (tracks.Length > 0)
        {
            if (!audioSource.isPlaying) audioSource.Play();
            else                        audioSource.Pause();
        }
    }

    public void ChangeTrack(Direction dir)
    {
        if (tracks.Length > 0)
        {
            if (dir == Direction.Left)
            {
                if (currentTrackNumber > 0) currentTrackNumber--;
                else currentTrackNumber = tracks.Length - 1;
            }
            else if (dir == Direction.Right)
            {
                if (currentTrackNumber < tracks.Length - 1) currentTrackNumber++;
                else currentTrackNumber = 0;
            }
            audioSource.clip = tracks[currentTrackNumber].GetAudioClip();
            audioSource.volume = tracks[currentTrackNumber].GetVolume();
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