﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

/* Script Made By Daniel, Edited By Petter */
public class Hand : MonoBehaviour
{
    #region Variables
    public SteamVR_Action_Boolean grabAction = null;
    public SteamVR_Action_Boolean restartAction = null;


    public SteamVR_Action_Vibration vibrationAction;
    public SteamVR_Input_Sources handToVibrate;
    public VibrationValues vibrationValues;
    
    private KeyCode grabKey = KeyCode.E;

    [Header("Debug")]
    [SerializeField] private Interactable currentInteractable;
    [SerializeField] private List<Interactable> contactInteractable = new List<Interactable>();
    [SerializeField] private bool childObjectOnPickup = true;

    private SteamVR_Behaviour_Pose pose = null;
    private FixedJoint fixedJoint;
    private MeshRenderer[] controllerMeshes;
    #endregion

    private void Awake()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        fixedJoint = GetComponent<FixedJoint>();
        //vibrationValues = GameObject.FindGameObjectWithTag("VibrationValues")
    }

    private void Update()
    {
        // Fetch using another method?
        switch (FindObjectOfType<Player>().GetPlayMode()) {
            case Playmode.VR:
                if (grabAction.GetStateDown(pose.inputSource)) Interact();
                if (grabAction.GetStateUp(pose.inputSource)) Drop();

                //For Debug
                if (restartAction.GetLastStateDown(pose.inputSource)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case Playmode.PC:
                break;
        }

        //Keyboard controls
        if (Input.GetKeyDown(grabKey)) Interact();
        if (Input.GetKeyUp(grabKey)) Drop();

        if (contactInteractable.Count > 0) SetMaterialOnClosest(); //Måste optimeras
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            contactInteractable.Add(other.gameObject.GetComponent<Interactable>());
            Vibrate(vibrationValues.touchingStuff);
        }   
        else if(other.gameObject.layer == LayerMask.NameToLayer("Button"))
        {
            Vibrate(vibrationValues.pushingButton);
        }
        else
        {
            Vibrate(vibrationValues.touchingStuff);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            Interactable interactable = other.gameObject.GetComponent<Interactable>();
            contactInteractable.Remove(interactable);
            interactable.SetToOutlineMaterial(MaterialType.Standard);
        }
    }

    private void Interact()
    {
        currentInteractable = GetNearestInteractable();

        if (!currentInteractable) return;

        Vibrate(vibrationValues.pickUpVibration);

        if (currentInteractable.ActiveHand)
            currentInteractable.ActiveHand.Drop();
        currentInteractable = currentInteractable.Interact();

        Pickup pickup = currentInteractable?.GetComponent<Pickup>();
        if (currentInteractable != null && pickup != null)
        {
            if (pickup.SnapOnPickup)
            {
                currentInteractable.transform.position = transform.position;
                currentInteractable.transform.rotation = Quaternion.Euler(transform.eulerAngles);
            }

            Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
            fixedJoint.connectedBody = targetBody;

            currentInteractable.ActiveHand = this;
            SetControllerMeshState(false);

            if (childObjectOnPickup)
                currentInteractable.transform.parent = Player.GetInstance().transform.root;
        }
    }

    public void Drop()
    {
        if (!currentInteractable) return;
        Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();

        //Vibrate(vibrationValues.putDownVibration);
        

        if (FindObjectOfType<Player>().GetPlayMode() == Playmode.VR)
        {
            targetBody.velocity = pose.GetVelocity();
            targetBody.angularVelocity = pose.GetAngularVelocity();
        }

        fixedJoint.connectedBody = null;

        if (childObjectOnPickup && !currentInteractable.IsBeingDestroyed)
        {
            try { currentInteractable.transform.SetParent(null); }
            catch { Exception e; }
        }

        currentInteractable.GetComponent<Pickup>()?.Drop();
        currentInteractable = null;
        SetControllerMeshState(true);
    }

    private Interactable GetNearestInteractable()
    {
        Interactable nearest = null;
        float minDistance = float.MaxValue;
        foreach (Interactable interactable in contactInteractable)
        {
            if (interactable == null || !interactable.gameObject.activeSelf)
            {
                contactInteractable.Remove(interactable);
                break;
            }
            else
            {
                float distance = (interactable.transform.position - transform.position).sqrMagnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = interactable;
                }
            }
        }
        return nearest;
    }

    private void SetControllerMeshState(bool state)
    {
        if (controllerMeshes == null) controllerMeshes = GetComponentsInChildren<MeshRenderer>();
        if (controllerMeshes != null)
        {
            foreach (MeshRenderer renderer in controllerMeshes) renderer.enabled = state;
        }
    }

    //Glöm ej att optimera
    private void SetMaterialOnClosest()
    {
        foreach (Interactable interactable in contactInteractable)
        {
            interactable?.SetToOutlineMaterial(MaterialType.Standard);
        }
        if (!GetNearestInteractable()?.ActiveHand) GetNearestInteractable()?.SetToOutlineMaterial(MaterialType.Outline);
    }


    public void Vibrate(VibrationValues.VibrationSettings vibrationSettings)
    {
        if (Player.GetInstance().GetPlayMode() == Playmode.PC) return;
        vibrationAction.Execute(0, vibrationSettings.duration, vibrationSettings.frequency, vibrationSettings.amplitude, handToVibrate);
    }

    public void Vibrate(VibrationValues.VibrationSettings vibrationSettings, float modifierValue, float modifierMax)
    {
        if (Player.GetInstance().GetPlayMode() == Playmode.PC) return;
        //vibrationAction.RemoveOnActiveBindingChangeListener(vibrationAction.Execute(), handToVibrate);
        float percentageValue = modifierValue / modifierMax;
        if(percentageValue < 0.5f)
        {
            vibrationSettings.duration /= 2f;
        }

        vibrationAction.Execute(0, vibrationSettings.duration, vibrationSettings.frequency, percentageValue, handToVibrate);
    }
}