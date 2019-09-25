using System.Collections;
using UnityEngine;

/* Script Made By Daniel */
public class PhysicsButton : MonoBehaviour
{
    private Vector3 position;
    private bool canTrigger = true;
    private bool hasCollided = false;
    private float executeTime = 2;
    private Coroutine coroutine;

    private void FixedUpdate()
    {
        if (!hasCollided) position = transform.position;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (canTrigger && other.gameObject.CompareTag("Button Trigger"))
        {
            if (coroutine == null)
            {
                //Debug.Log(other.gameObject.name);
                //StartCoroutine(ExecuteButtonPush());
                ButtonPush();
            }
        }
        if (other.gameObject.CompareTag("Button Limiter"))
        {
            hasCollided = true;
            transform.position = position;
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
        Debug.Log("ButtonPush");
        yield return new WaitForSecondsRealtime(executeTime);
        Debug.Log("Push Finished");
        ButtonPush();
    }

    protected virtual void ButtonPush()
    {
        //Debug.Log("NU");
        StartCoroutine(ButtonTriggerDelay());
    }

    private IEnumerator ButtonTriggerDelay()
    {
        canTrigger = false;
        yield return new WaitForSecondsRealtime(1);
        canTrigger = true;
    }
}