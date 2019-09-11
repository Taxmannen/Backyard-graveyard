using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThiefObjectSearcher : MonoBehaviour
{
    private Pickup possibleTarget;
    [SerializeField] PickupType targetType;
    [HideInInspector] public Pickup realTarget;

    private void OnTriggerEnter(Collider other)
    {
        //Behöver ändras helt och hållet
        if (other.tag == "Interactable")
        {
            possibleTarget = other.GetComponent<Pickup>();

            if(possibleTarget != null)
            {
                if (possibleTarget.GetPickupType() == targetType)
                {
                    realTarget = possibleTarget;
                }
            }

            
        }

        
    }

    
}
