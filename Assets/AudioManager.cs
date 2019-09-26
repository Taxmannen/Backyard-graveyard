using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioClip despawnSound;

    private void Awake()
    {
        SetInstance(this);
    }

    public void PlaySoundAtPosition(AudioClip clip, Transform sourceTransform)
    {
        AudioSource.PlayClipAtPoint(clip, sourceTransform.position);
    }
}
