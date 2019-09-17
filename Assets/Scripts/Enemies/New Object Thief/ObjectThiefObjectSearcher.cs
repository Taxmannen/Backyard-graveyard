using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThiefObjectSearcher : MonoBehaviour
{
    [SerializeField] PickupType targetType;

    private Pickup possibleTarget;
    [HideInInspector] public Pickup realTarget;

    [HideInInspector] private Body bodyTarget;
    [HideInInspector] private Ornament ornamentTarget;



    private void OnTriggerEnter(Collider other)
    {
        //Behöver ändras helt och hållet
        if (other.tag == "Interactable")
        {
            possibleTarget = other.GetComponent<Pickup>();

            if(targetType == PickupType.Body)
            {
                bodyTarget = other.GetComponent<Body>();

                if (possibleTarget != null && bodyTarget != null)
                {
                    if (possibleTarget.GetPickupType() == targetType && realTarget != possibleTarget && !bodyTarget.IsInGrave)
                    {
                        realTarget = possibleTarget;
                    }
                }
            }

            else if (possibleTarget != null)
            {
                if (possibleTarget.GetPickupType() == targetType && realTarget != possibleTarget)
                {
                    realTarget = possibleTarget;
                }
            }

            //if(targetType == PickupType.Ornament)
            //{

            //}





        }

        
    }

    public PickupType GetTargetType()
    {
        return targetType;
    }

    
}
