using System.Collections;
using UnityEngine;

/* Script Made By Daniel */
public enum ButtonAction { Play, PreviousTrack, NextTrack }

public class MusicButton : MonoBehaviour
{
    [SerializeField] private MusicPlayer musicPlayer;
    [SerializeField] private ButtonAction buttonAction;

    private Vector3 position;
    private bool canTrigger = true;
    private bool hasCollided = false;

    private void FixedUpdate()
    {
        if (!hasCollided) position = transform.localPosition;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (canTrigger && other.gameObject.CompareTag("Button Trigger"))
        {
            ButtonPush();
        }
        if (other.gameObject.CompareTag("Button Limiter"))
        {
            hasCollided = true;
            transform.localPosition = position;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Button Limiter")) hasCollided = false;
    }

    protected virtual void ButtonPush()
    {
        switch (buttonAction)
        {
            case ButtonAction.Play:
                musicPlayer.PlayAndPause();
                //Debug.Log("Play");
                break;
            case ButtonAction.PreviousTrack:
                musicPlayer.ChangeSong(ChangeTrack.PreviousTrack);
                //Debug.Log("Previous Track");
                break;
            case ButtonAction.NextTrack:
                musicPlayer.ChangeSong(ChangeTrack.NextTrack);
                //Debug.Log("Next Track");
                break;
            default:
                Debug.LogError("Something Went Wrong");
                break;
        }
        StartCoroutine(ButtonTriggerDelay());
    }

    private IEnumerator ButtonTriggerDelay()
    {
        canTrigger = false;
        yield return new WaitForSecondsRealtime(0.5f);
        canTrigger = true;
    }
}
