﻿using UnityEngine;

public enum HeadType { Red, Green, Blue, [System.ObsoleteAttribute] NumberOfTypes, [System.ObsoleteAttribute] None };

/* Script Created By Petter */
public class Head : BodyPart
{
    private HeadType headType;

    private void Awake()
    {
        SetColor();
    }

    private void SetColor()
    {
        MeshRenderer[] meshRenderer = GetComponentsInChildren<MeshRenderer>();

        headType = (HeadType)Random.Range(0, 3);
        for (int i = 0; i < meshRenderer.Length; i++)
        {
            if (headType == HeadType.Blue)
            {
                meshRenderer[i].material.color = Color.blue;
            }
            else if (headType == HeadType.Green)
            {
                meshRenderer[i].material.color = Color.green;
            }
            else if (headType == HeadType.Red)
            {
                meshRenderer[i].material.color = Color.red;
            }
        }
    }

    public HeadType GetHeadType() { return headType; }
}