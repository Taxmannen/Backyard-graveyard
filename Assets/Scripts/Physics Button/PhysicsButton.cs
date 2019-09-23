using System.Collections;
using UnityEngine;

/* Script Made By Daniel */
public class PhysicsButton : MonoBehaviour
{
    private Vector3 position;
    private bool canTrigger = true;
    private bool hasCollided = false;

    private void FixedUpdate()
    {
        if (!hasCollided) position = transform.position;
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
            transform.position = position;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Button Limiter")) hasCollided = false;
    }

    protected virtual void ButtonPush()
    {
        StartCoroutine(ButtonTriggerDelay());
    }

    private IEnumerator ButtonTriggerDelay()
    {
        canTrigger = false;
        yield return new WaitForSecondsRealtime(0.5f);
        canTrigger = true;
    }
}