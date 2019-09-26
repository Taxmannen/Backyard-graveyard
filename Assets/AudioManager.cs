using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip despawnSound;

    public void PlaySoundAtPosition(AudioClip clip, Transform sourceTransform)
    {
        AudioSource.PlayClipAtPoint(clip, sourceTransform.position);
    }
}
