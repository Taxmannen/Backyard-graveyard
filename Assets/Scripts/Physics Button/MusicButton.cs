using UnityEngine;

public enum ButtonAction { Play, PreviousTrack, NextTrack }

/* Script Made By Daniel */
public class MusicButton : PhysicsButton
{
    [SerializeField] private MusicPlayer musicPlayer;
    [SerializeField] private ButtonAction buttonAction;

    protected override void ButtonPush()
    {
        switch (buttonAction)
        {
            case ButtonAction.Play:
                musicPlayer.PlayAndPause();
                break;
            case ButtonAction.PreviousTrack:
                musicPlayer.ChangeSong(ChangeTrack.PreviousTrack);
                break;
            case ButtonAction.NextTrack:
                musicPlayer.ChangeSong(ChangeTrack.NextTrack);
                break;
            default:
                Debug.LogError("Something Went Wrong");
                break;
        }
        base.ButtonPush();
    }
}