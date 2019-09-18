using System.Collections;
using UnityEngine;

/* Script Made By Daniel */
public enum ButtonAction { Play, PreviousTrack, NextTrack }

public class MusicButton : MonoBehaviour
{
    [SerializeField] private ButtonAction buttonAction;
    [SerializeField] private MusicPlayer musicPlayer;

    private Vector3 startPosition;
    private bool canTrigger = true;

    private void Awake()
    {
        startPosition = transform.localPosition;
    }

    private void FixedUpdate()
    {
        //if (transform.localPosition.y > 0.475) transform.localPosition = startPosition;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (canTrigger && other.gameObject.CompareTag("Button Trigger"))
        {
            switch(buttonAction)
            {
                case ButtonAction.Play:
                    musicPlayer.PlayAndPause();
                    break;
                case ButtonAction.PreviousTrack:
                    musicPlayer.ChangeTrack(Direction.Left);
                    break;
                case ButtonAction.NextTrack:
                    musicPlayer.ChangeTrack(Direction.Right);
                    break;
                default:
                    Debug.LogError("Something Went Wrong");
                    break;
            }
            StartCoroutine(ButtonTriggerDelay());
        }
    }

    private IEnumerator ButtonTriggerDelay()
    {
        canTrigger = false;
        yield return new WaitForSecondsRealtime(0.5f);
        canTrigger = true;
    }
}
