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

    public KeyCode grabKey;

    [Header("Debug")]
    [SerializeField] private Interactable currentInteractable;
    [SerializeField] private List<Interactable> contactInteractable = new List<Interactable>();

    private SteamVR_Behaviour_Pose pose = null;
    private FixedJoint fixedJoint;
    private MeshRenderer[] controllerMeshes;
    #endregion

    private void Awake()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        fixedJoint = GetComponent<FixedJoint>();
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
        }
    }

    public void Drop()
    {
        if (!currentInteractable) return;
        Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();

        if(FindObjectOfType<Player>().GetPlayMode() == Playmode.VR) {
            targetBody.velocity = pose.GetVelocity();
            targetBody.angularVelocity = pose.GetAngularVelocity();
        }

        fixedJoint.connectedBody = null;

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
            if (interactable == null)
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
            interactable.SetToOutlineMaterial(MaterialType.Standard);
        }
        if (!GetNearestInteractable()?.ActiveHand) GetNearestInteractable()?.SetToOutlineMaterial(MaterialType.Outline);
    }
}