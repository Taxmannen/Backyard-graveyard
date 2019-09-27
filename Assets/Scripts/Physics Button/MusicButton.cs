using UnityEngine;

public enum ButtonAction { Play, PreviousTrack, NextTrack }

/* Script Made By Daniel */
public class MusicButton : PhysicsButton
{
    #region Variables
    [SerializeField] private MusicPlayer musicPlayer;
    [SerializeField] private ButtonAction buttonAction;
    private bool canPush;
    #endregion

    private void Start()
    {
        Invoke("EnablePlayer", 1);
    }

    protected override void ButtonPush()
    {
        if (musicPlayer && canPush)
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
        }
        base.ButtonPush();
    }

    private void EnablePlayer()
    {
        canPush = true;
    }
}
