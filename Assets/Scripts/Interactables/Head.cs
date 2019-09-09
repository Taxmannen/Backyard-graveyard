using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeadType { Red, Green, Blue, [System.ObsoleteAttribute] NumberOfTypes, [System.ObsoleteAttribute] None };

/* Script Created By Petter */
public class Head : Pickup
{
    private HeadType headType;

    //public enum headType { Blue, Green, Red };

    private void Awake()
    {
        SetColor();
    }

    private void SetColor()
    {
        headType = (HeadType)Random.Range(0, 3);

        MeshRenderer[] meshRenderer = GetComponentsInChildren<MeshRenderer>();

        if (headType == HeadType.Blue)
        {
            for (int i = 0; i < meshRenderer.Length; i++)
            {
                meshRenderer[i].material.color = Color.blue;
            }
        }
        else if (headType == HeadType.Green)
        {
            for (int i = 0; i < meshRenderer.Length; i++)
            {
                meshRenderer[i].material.color = Color.green;
            }
        }
        else if (headType == HeadType.Red)
        {
            for (int i = 0; i < meshRenderer.Length; i++)
            {
                meshRenderer[i].material.color = Color.red;
            }
        }
    }

    public HeadType GetHeadType() { return headType; }
}
