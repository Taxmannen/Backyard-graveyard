using System.Collections;
using UnityEngine;

/* Script Made By Daniel */

[RequireComponent (typeof (AudioSource))]
public class PhysicsButton : MonoBehaviour
{
    #region Variables
    [SerializeField] private float executeTime = 0;

    private bool canTrigger = true;
    private bool hasCollided = false;
    private Vector3 position;
    private Coroutine coroutine;
    public AudioSource audioSource;
    #endregion

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (!hasCollided) position = transform.localPosition;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (canTrigger && other.gameObject.CompareTag("Button Trigger"))
        {
            if (coroutine == null) coroutine = StartCoroutine(ExecuteButtonPush());
        }
        if (other.gameObject.CompareTag("Button Limiter"))
        {
            hasCollided = true;
            transform.localPosition = position;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Button Trigger"))
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }
        if (other.gameObject.CompareTag("Button Limiter"))
        {
            hasCollided = false;
        }
    }

    private IEnumerator ExecuteButtonPush()
    {
        yield return new WaitForSecondsRealtime(executeTime);
        ButtonPush();
    }

    protected virtual void ButtonPush()
    {
        audioSource.Play();
        StartCoroutine(ButtonTriggerDelay());
    }

    private IEnumerator ButtonTriggerDelay()
    {
        canTrigger = false;
        yield return new WaitForSecondsRealtime(1);
        canTrigger = true;
    }
}