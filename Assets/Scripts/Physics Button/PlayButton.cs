using System;
using UnityEngine;

/* Script Made By Daniel */
public class PlayButton : PhysicsButton
{
    [SerializeField] private GameObject button;
    public static event Action PlayEvent;
    public static event Action StopEvent;
    public static bool isPlaying;

    protected override void ButtonPush()
    {
        PlayEvent?.Invoke();
        isPlaying = true;
        button.SetActive(false);
    }

    public void StopPlaying()
    {
        if (isPlaying)
        {
            StopEvent?.Invoke();
            isPlaying = false;
            button.SetActive(true);
        }
    }
}