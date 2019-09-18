using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThiefObjectSearcher : MonoBehaviour
{
    [SerializeField] PickupType targetType;

    [HideInInspector] private Body bodyTarget;

    private Pickup possibleTarget;
    [HideInInspector] public Pickup realTarget;


    private void OnTriggerEnter(Collider other)
    {
        //Behöver ändras helt och hållet(?)
        if (other.tag == "Interactable")
        {
            possibleTarget = other.GetComponent<Pickup>();

            if(possibleTarget != null)
            {
                if (targetType == PickupType.Body)
                {
                    CheckIfBodyAndIfInGrave(other);
                }
                else
                {
                    SetTargetIfSameTypeAndNotSameAsLastTarget();
                }
            }
        }
    }

    private void CheckIfBodyAndIfInGrave(Collider other)
    {
        bodyTarget = other.GetComponent<Body>();

        if (bodyTarget != null)
        {
            if(!bodyTarget.IsInGrave)
            {
                SetTargetIfSameTypeAndNotSameAsLastTarget();
            }
        }
    }

    private void SetTargetIfSameTypeAndNotSameAsLastTarget()
    {
        if (possibleTarget.GetPickupType() == targetType && realTarget != possibleTarget)
        {
            realTarget = possibleTarget;
        }
    }


    public PickupType GetTargetType()
    {
        return targetType;
    }
}
