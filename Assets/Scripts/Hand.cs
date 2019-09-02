using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour
{
    public SteamVR_Action_Boolean grabAction = null;
        
    private SteamVR_Behaviour_Pose pose = null;
    private FixedJoint fixedJoint;
    private Interactable currentInteractable;
    private List<Interactable> contactInteractable = new List<Interactable>();

    private void Awake()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        fixedJoint = GetComponent<FixedJoint>();
    }

    private void Update()
    {
        if (grabAction.GetStateDown(pose.inputSource))
        {
            Debug.Log(pose.inputSource + " " + "Trigger Down");
            Pickup();
        }

        if (grabAction.GetStateUp(pose.inputSource))
        {
            Debug.Log(pose.inputSource + " " + "Trigger Up");
            Drop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            contactInteractable.Add(other.gameObject.GetComponent<Interactable>());
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            contactInteractable.Remove(other.gameObject.GetComponent<Interactable>());
        }
    }

    private void Pickup()
    {
        currentInteractable = GetNearestInteractable();

        if (!currentInteractable) return;

        if (currentInteractable.ActiveHand)
            currentInteractable.ActiveHand.Drop();

        currentInteractable.transform.position = currentInteractable.GetPickupPosition();
        Debug.Log(currentInteractable.transform.position);

        Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
        fixedJoint.connectedBody = targetBody;

        currentInteractable.ActiveHand = this;
    }

    private void Drop()
    {
        if (!currentInteractable) return;

        Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
        targetBody.velocity = pose.GetVelocity();
        targetBody.angularVelocity = pose.GetAngularVelocity();

        fixedJoint.connectedBody = null;

        currentInteractable.ActiveHand = null;
        currentInteractable = null;
    }

    private Interactable GetNearestInteractable()
    {
        Interactable nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0;

        foreach (Interactable interactable in contactInteractable)
        {
            distance = (interactable.transform.position - transform.position).sqrMagnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = interactable;
            }
        }
        return nearest;
    }
}