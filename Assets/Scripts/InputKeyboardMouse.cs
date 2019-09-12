#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Valve.VR;

/// <summary>
/// <author>Simon</author>
/// </summary>
public class InputKeyboardMouse : Singleton<InputKeyboardMouse>
{
    #region Public variables
    [Tooltip("The object to be interacted with using the input.")]
    public GameObject playerObject;
    [Tooltip("If the player pressed W and S or A and D, it stops.")]
    public bool conflictingKeysStopPlayer = true;
    [Tooltip("The hand object to be interacted with using the input.")]
    public GameObject hand;
    [Tooltip("Allows the player to fly.")]
    public bool allowFlying = false;
    [Tooltip("The speed at which the player moves around.")]
    public float moveSpeed = 5;

    public GameObject rotX;
    public GameObject rotY;
    #endregion

    #region Private variables
    private FixedJoint fixedJoint;
    [SerializeField] private Interactable currentInteractable;
    private bool wasSnappable = false;

    private Camera mainCamera;

    private Vector3 oldMousePosition = Vector3.zero;

    Quaternion originalRotation;
    float rotationY;
    float rotationX;
    #endregion

    private void Start() {
        SetInstance(this);
    }

    private void Awake() 
    {
        fixedJoint = GetComponentInChildren<FixedJoint>();

        originalRotation = playerObject.transform.rotation;
    }

    /// <summary>
    /// Handles keyboard movement, mouse picking and player mouse rotation
    /// </summary>
    private void Update() 
    {

        #region Keyboard movement
        Vector3 movement = Vector3.zero;

        //if conflictingKeysStopPlayer is false, then ignore the top row
        if (conflictingKeysStopPlayer && (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))) movement.z = 0;
        else if (Input.GetKey(KeyCode.W)) movement.z = Time.deltaTime * moveSpeed;
        else if (Input.GetKey(KeyCode.S)) movement.z = -Time.deltaTime * moveSpeed;

        //if conflictingKeysStopPlayer is false, then ignore the top row
        if (conflictingKeysStopPlayer && (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))) movement.x = 0;
        else if (Input.GetKey(KeyCode.A)) movement.x = -Time.deltaTime * moveSpeed;
        else if (Input.GetKey(KeyCode.D)) movement.x = Time.deltaTime * moveSpeed;

        //movement.Normalize();

        playerObject.transform.Translate(movement, Space.Self);
        if (!allowFlying) playerObject.transform.position = new Vector3(playerObject.transform.position.x, 1.83f, playerObject.transform.position.z);
        #endregion

        #region Mouse rotation
        //Vector3 currentMousePosition = Input.mousePosition;
        //Vector3 deltaMousePosition = currentMousePosition - oldMousePosition;

        //deltaMousePosition.x *= Time.deltaTime * 140;
        //deltaMousePosition.y *= Time.deltaTime * 140;
        //deltaMousePosition.z = 0;

        rotationY += Input.GetAxis("Mouse Y") * 7;
        rotationX += Input.GetAxis("Mouse X") * 7;
        float rotAverageY = Mathf.Clamp(rotationY, -90, 90);
        //float rotAverageX = Mathf.Clamp(rotationX, -30, 30);
        float rotAverageX = rotationX;

        Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);

        playerObject.transform.localRotation = originalRotation * xQuaternion * yQuaternion;

        //rotX.transform.Rotate(-deltaMousePosition.y, 0, 0, Space.Self);
        //rotY.transform.Rotate(0, deltaMousePosition.x, 0, Space.Self);
        //playerObject.transform.rotation = Quaternion.Euler(playerObject.transform.rotation.x, playerObject.transform.rotation.y, 0);
        //playerObject.transform.RotateAroundLocal(new Vector3(0, 1, 0), deltaMousePosition.y);

        //oldMousePosition = currentMousePosition;
        #endregion

        //#region Mouse picking
        //if (Input.GetMouseButtonDown(0)) 
        //{
        //    if (mainCamera == null) 
        //    {
        //        mainCamera = Camera.main;
        //    }

        //    Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        //    RaycastHit[] raycastHits = Physics.RaycastAll(ray);

        //    if (raycastHits.Length > 0) 
        //    {
        //        var nearest = raycastHits
        //            .Where(x => x.transform.CompareTag("Interactable"))
        //            .OrderBy(x => (x.transform.position - playerObject.transform.position).sqrMagnitude)
        //            .FirstOrDefault();

        //        if (nearest.transform != null) //Check if an object with the Interactable tag was hit
        //        {
        //            var interactable = nearest.transform.GetComponent<Interactable>();

        //            Pickup(interactable);
        //        }
        //    }
        //}
        //else if (Input.GetMouseButtonUp(0)) 
        //{
        //    Drop();
        //}
        //#endregion
    }

    public void Pickup(Interactable interactable) 
    {
        //if (currentInteractable == interactable) return;

        //currentInteractable = interactable;

        //if (!currentInteractable) return;

        //if (currentInteractable.ActiveHand) Drop();

        //currentInteractable.transform.position = hand.transform.position;

        //Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
        //fixedJoint.connectedBody = targetBody;

        currentInteractable = interactable;

        if (!currentInteractable) return;
        currentInteractable = currentInteractable.Interact();

        //Debug.Log("Pickup");

        Pickup pickup = currentInteractable?.GetComponent<Pickup>();
        if (currentInteractable != null && pickup != null) {
            if (pickup.SnapOnPickup) {
                currentInteractable.transform.position = hand.transform.position;
                currentInteractable.transform.rotation = Quaternion.Euler(hand.transform.eulerAngles);
            }

            if (pickup is Ornament) {
                wasSnappable = ((Ornament)pickup).Snappable;
                ((Ornament)pickup).Snappable = false;
            }

            Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
            fixedJoint.connectedBody = targetBody;
        }
    }

    private void Drop() 
    {
        if (!currentInteractable) return;

        Pickup pickup = currentInteractable?.GetComponent<Pickup>();
        if (pickup is Ornament) {
            ((Ornament)pickup).Snappable = wasSnappable;
            wasSnappable = false;
        }

        Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
        targetBody.velocity = Vector3.zero;
        targetBody.angularVelocity = Vector3.zero;

        fixedJoint.connectedBody = null;

        currentInteractable.ActiveHand = null;
        currentInteractable = null;
    }
}

#endif