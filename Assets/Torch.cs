using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    private FireHolder fireHolder;

    private void Start()
    {
        fireHolder = GetComponentInChildren<FireHolder>();
        fireHolder.LightUp();
    }
}
