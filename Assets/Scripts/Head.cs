using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : Pickup
{
    public enum MyColor { Blue, Green, Red };

    private void Awake()
    {
        MyColor myColor = (MyColor)Random.Range(0, 3);

        MeshRenderer[] meshRenderer = GetComponentsInChildren<MeshRenderer>();

        if (myColor == MyColor.Blue)
        {
            for (int i = 0; i < meshRenderer.Length; i++)
            {
                meshRenderer[i].material.color = Color.blue;
            }
        }
        else if (myColor == MyColor.Green)
        {
            for (int i = 0; i < meshRenderer.Length; i++)
            {
                meshRenderer[i].material.color = Color.green;
            }
        }
        else if (myColor == MyColor.Red)
        {
            for (int i = 0; i < meshRenderer.Length; i++)
            {
                meshRenderer[i].material.color = Color.red;
            }
        }
    }
}
