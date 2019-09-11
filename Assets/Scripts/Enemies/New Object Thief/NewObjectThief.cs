using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewObjectThief : MonoBehaviour
{
    //States & Commands
    NewObjectThiefState currentState;
    NewObjectThiefState returnedState;

    public ObjectThiefJump enemyJump;
    public ObjectThiefPickupHand pickupHand;

    [SerializeField] private Rigidbody rigidBodyMarionette;
    [SerializeField] private Rigidbody rigidBodyArm;

    [SerializeField] private float speed = 50;
    [SerializeField] private float armSpeed = 50;



    [HideInInspector] public GameObject currentTargetObject;
    [HideInInspector] public GameObject objectInHand;



    private void Start()
    {
        //Change this? Should be the play-area in the beginning
        currentTargetObject = GameObject.FindGameObjectWithTag("DistanceCheckForObjectThief");

        currentState = new NewObjectThiefMoveToTargetState();
        currentState.Enter(this);

        
    }

    private void Update()
    {
        //Fullfix för om target-objekt försvinner
        if(currentTargetObject == null)
        {
            currentTargetObject = GameObject.FindGameObjectWithTag("DistanceCheckForObjectThief");
        }

        returnedState = currentState.Update(this, Time.deltaTime);
        if (returnedState != null)
        {
            StateSwap();
        }
    }

    private void FixedUpdate()
    {
        returnedState = currentState.FixedUpdate(this, Time.deltaTime);
    }

    private void StateSwap()
    {
        currentState.Exit(this);
        currentState = returnedState;
        currentState.Enter(this);
    }

    public void Move(Vector3 moveAgainst)
    {
        MovePart(rigidBodyMarionette, moveAgainst, speed);
        //moveAgainst = moveAgainst.normalized;
        //Vector3 movement = new Vector3(moveAgainst.x, 0, moveAgainst.z);
        //rigidBodyMarionette.AddForce(movement * speed);
    }

    public Vector3 GetDirectionToTarget(Vector3 targetPosition, Vector3 bodyPartToMovePosition)
    {
        Vector3 directionToTarget = targetPosition - bodyPartToMovePosition;
        return directionToTarget;
    }

    public Vector3 GetTargetPosition()
    {
        return currentTargetObject.transform.position;
    }

    public Vector3 GetStringPosition()
    {
        return rigidBodyMarionette.transform.position;
    }

    public Vector3 GetArmPosition()
    {
        return rigidBodyArm.transform.position;
    }

    public void MoveArm(Vector3 moveAgainst)
    {
        MovePart(rigidBodyArm, moveAgainst, armSpeed);
    }

    private void MovePart(Rigidbody rigidBodyToMove, Vector3 moveAgainst, float forceToMoveWith)
    {
        moveAgainst = moveAgainst.normalized;
        Vector3 movement = new Vector3(moveAgainst.x, 0, moveAgainst.z);
        rigidBodyToMove.AddForce(movement * forceToMoveWith);
    }

    public ObjectThiefObjectSearcher objectSearcher;

    public void FindNewCurrentGameObjectWithTag(string tag)
    {
        currentTargetObject = GameObject.FindGameObjectWithTag(tag);
    }

    public void Despawn()
    {
        if(objectInHand != null)
        {
            Destroy(objectInHand);
        }
        
        Destroy(gameObject);
    }
    
}
