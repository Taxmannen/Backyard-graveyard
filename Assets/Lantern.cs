using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    private FireHolder fireHolder;
    private float secondsThatTheLanternHasBeenLit;

    [SerializeField] private float secondsLitForUnrestDecrease;

    private void Start()
    {
        fireHolder = GetComponentInChildren<FireHolder>();
    }

    private void Update()
    {
        if (fireHolder.isLit)
        {
            secondsThatTheLanternHasBeenLit += Time.deltaTime;
            if (secondsThatTheLanternHasBeenLit >= secondsLitForUnrestDecrease)
            {
                secondsThatTheLanternHasBeenLit = 0f;
                UnrestManager.GetInstance().UpdateUnrest(-1);
            }
        }
        else
        {
            secondsThatTheLanternHasBeenLit = 0f;
        }
    }
}
