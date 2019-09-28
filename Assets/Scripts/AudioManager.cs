using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private void Awake()
    {
        SetInstance(this);
    }

    public void PlaySoundAtPosition(AudioClip clip, Transform sourceTransform)
    {
        AudioSource.PlayClipAtPoint(clip, sourceTransform.position);
    }
}