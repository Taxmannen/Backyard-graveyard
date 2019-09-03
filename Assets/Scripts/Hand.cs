﻿using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/* Script Made By Daniel, Edited By Petter */
public class Hand : MonoBehaviour
{
    public SteamVR_Action_Boolean grabAction = null;
        
    private SteamVR_Behaviour_Pose pose = null;
    private FixedJoint fixedJoint;
    private Interactable currentInteractable;
    private List<Interactable> contactInteractable = new List<Interactable>();

    private MeshRenderer[] controllerMeshes;

    private void Awake()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        fixedJoint = GetComponent<FixedJoint>();
    }

    private void Update()
    {
        if (grabAction.GetStateDown(pose.inputSource)) Pickup();
        if (grabAction.GetStateUp(pose.inputSource)) Drop();

        if (contactInteractable.Count > 0) SetMaterialOnClosest(); //Måste optimeras
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
            Interactable interactable = other.gameObject.GetComponent<Interactable>();
            contactInteractable.Remove(interactable);
            interactable.SetToOutlineMaterial(false);
        }
    }

    private void Pickup()
    {
        currentInteractable = GetNearestInteractable();

        if (!currentInteractable) return;

        if (currentInteractable.ActiveHand)
            currentInteractable.ActiveHand.Drop();

        currentInteractable = currentInteractable.Interact();

        if (currentInteractable != null)
        {
            if (currentInteractable.SnapOnPickup)
            {
                currentInteractable.transform.position = transform.position;
                currentInteractable.transform.rotation = Quaternion.Euler(transform.eulerAngles);
            }

            Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
            fixedJoint.connectedBody = targetBody;

            currentInteractable.ActiveHand = this;
            SetControllerMeshState(false);
        }
    }

    private void Drop()
    {
        if (!currentInteractable) return;

        Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
        targetBody.velocity = pose.GetVelocity();
        targetBody.angularVelocity = pose.GetAngularVelocity();

        fixedJoint.connectedBody = null;

        currentInteractable.Drop();
        currentInteractable = null;
        SetControllerMeshState(true);
    }

    private Interactable GetNearestInteractable()
    {
        Interactable nearest = null;
        float minDistance = float.MaxValue;
        foreach (Interactable interactable in contactInteractable)
        {
            float distance = (interactable.transform.position - transform.position).sqrMagnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = interactable;
            }
        }
        return nearest;
    }

    private void SetControllerMeshState(bool state)
    {
        if (controllerMeshes == null) controllerMeshes = GetComponentsInChildren<MeshRenderer>();
        if (controllerMeshes != null) foreach (MeshRenderer renderer in controllerMeshes) renderer.enabled = state;
    }

    //Glöm ej att optimera
    private void SetMaterialOnClosest()
    {
        foreach (Interactable interactable in contactInteractable)
        {
            interactable.SetToOutlineMaterial(false);
        }
        GetNearestInteractable().SetToOutlineMaterial(true);
    }
}