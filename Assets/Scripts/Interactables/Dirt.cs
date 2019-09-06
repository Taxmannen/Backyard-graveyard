﻿using UnityEngine;

/* Script Made By Daniel */
public class Dirt : Pickup
{
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.GetComponent<Grave>() != null)
        {
            Grave grave = other.gameObject.GetComponent<Grave>();
            if (!ActiveHand)
            {
                grave.GetComponent<Grave>().AddDirt();
                Destroy(gameObject);
            } 
        }
    }
}