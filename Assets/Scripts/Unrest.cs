using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unrest : MonoBehaviour
{
    private float timeOnGroundBeforeUnrestIncrease = 5f; //Change to 15 sec
    private Coroutine coroutine;
    private bool onGround;
    private int value = 1;


    private void OnCollisionEnter(Collision other)
    {
        if (coroutine == null && other.gameObject.CompareTag("Ground"))
        {
            coroutine = StartCoroutine(UnrestChange());
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (coroutine != null && other.gameObject.CompareTag("Ground"))
        {
            onGround = false;
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    private IEnumerator UnrestChange()
    {
        onGround = true;
        while (onGround)
        {
            yield return new WaitForSecondsRealtime(timeOnGroundBeforeUnrestIncrease);
            UnrestManager.GetInstance().UpdateUnrest(value);
        }
    }
}
