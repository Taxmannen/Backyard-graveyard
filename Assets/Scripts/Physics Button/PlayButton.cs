using System;
using UnityEngine;

/* Script Made By Daniel */
public class PlayButton : PhysicsButton
{
    [SerializeField] private GameObject button;
    public static event Action PlayEvent;
    public static bool isPlaying;

    protected override void ButtonPush()
    {
        PlayEvent?.Invoke();
        isPlaying = true;
        button.SetActive(false);
    }

    public void EnableButton()
    {
        button.SetActive(true);
    }
}