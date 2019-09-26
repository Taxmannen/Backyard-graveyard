using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    private FireHolder fireHolder;
    private Pickup pickup;

    private void Start()
    {
        pickup = GetComponent<Pickup>();
        fireHolder = GetComponentInChildren<FireHolder>();
        fireHolder.LightUp();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "FireHolder")
        {
            if(pickup.ActiveHand != null)
            {
                pickup.ActiveHand.Vibrate(pickup.ActiveHand.vibrationValues.burningStuffWithTorch);
            }
        }
    }
}
