﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Code by Christopher Tåqvist */

public class NewObjectThief : MonoBehaviour
{
    [Header("Testing stuff")]
    [SerializeField] public bool stateMachineOn = true;
    public bool debugStates = false;

    //Objects to target & Hold
    [Header("Object Check (DO NOT TOUCH!)")]
    public GameObject currentTargetObject;
    public GameObject objectInHand;
    public GameObject randomTargetObject;

    //States & Commands
    NewObjectThiefState currentState;
    NewObjectThiefState returnedState;

    [Header("Components (DO NOT TOUCH)")]
    public ObjectThiefJump enemyJump;
    public ObjectThiefPickupHand pickupHand;
    public ObjectThiefObjectSearcher objectSearcher;
    public NewObjectThiefRandomTargetArea randomTargetArea;
    public EnemyMarionette enemyMarionette;

    [Header("Rigidbodies to Control (DO NOT TOUCH)")]
    [SerializeField] private Rigidbody rigidBodyMarionette;
    [SerializeField] private Rigidbody rigidBodyArm;





    [Header("Transforms relative to object (DO NOT TOUCH)")]
    [SerializeField] private Transform[] relativeTransforms;
    private Vector3[] relativeStartPositions;
    private Quaternion[] relativeStartRotations;

    private void SetRelativeStartPosAndRotations()
    {
        relativeStartPositions = new Vector3[relativeTransforms.Length];
        relativeStartRotations = new Quaternion[relativeTransforms.Length];

        for(int i = 0; i < relativeTransforms.Length; i++)
        {
            relativeStartPositions[i] = relativeTransforms[i].localPosition;
            relativeStartRotations[i] = relativeTransforms[i].localRotation;
        }
    }

    public void RestartAllPositions()
    {
        for (int i = 0; i < relativeTransforms.Length; i++)
        {
            relativeTransforms[i].localPosition = relativeStartPositions[i];
            relativeTransforms[i].localRotation = relativeStartRotations[i];
        }
    }

    private void OnDisable()
    {
        //RestartAllPositions();
    }






    [Header("GameObjects (DO NOT TOUCH)")]
    [SerializeField] private GameObject[] marionetteStringGameObjects;


    [Header("Speed Settings")]
    [SerializeField] private float movementSpeedRun = 50;
    [SerializeField] private float movementSpeedSearch = 25;
    [SerializeField] private float armSpeed = 50;

    [Header("Distance Settings")]
    [SerializeField] private float distanceBeforeTargetIsReached = 3f;
    [SerializeField] private float distanceBeforeTargetIsPickedUp = 0.5f;
    [SerializeField] private float distanceBeforeEnemyDespawn = 20f;

    [Header("Timers")]
    [SerializeField] private float timeBeforeTryingNewPickupTarget = 2f;
    [SerializeField] private float timeBeforeTryingNewTarget = 10f;
    [SerializeField] private float despawnAfterDeathTime = 10f;

    //Används för fulfixen för om objekt försvinner
    private bool isDead = false;


    private void Awake()
    {
        //rigidBody = GetComponent<Rigidbody>();
    }

    //Ta bort start?
    private void Start()
    {
        SetRelativeStartPosAndRotations();


        /*The following code isn't needed as it's running during OnEnable??*/
        //randomTargetArea.SetTargetPositionToPlayArea();
        //currentTargetObject = randomTargetObject;

        //if (stateMachineOn)
        //{
        //    currentState = new NewObjectThiefMoveToTargetState();
        //}
        
        //else{
        //    currentState = new NewObjectThiefEmptyStateForTesting();
        //}

        //currentState.Enter(this);
    }

    private void OnEnable()
    {

        //Enable and set marionette at correct position again.
        ToggleMarionetteStrings(true);
        //transform.position = new Vector3(transform.position.x, 3, transform.position.z);
        //rigidBodyMarionette.velocity = new Vector3(0, 0, 0);
        //rigidBodyArm.velocity = new Vector3(0, 0, 0);

        //for (int i = 0; i < marionetteStringGameObjects.Length; i++)
        //{

        //    marionetteStringGameObjects[0].transform.localPosition = new Vector3(0, 1.398f, 0);
        //    marionetteStringGameObjects[1].transform.localPosition = new Vector3(0, 0, 0);

        //}

        if(relativeStartPositions != null)
        {
            RestartAllPositions();
        }
        
        //Fullösning för att få de att ställa sig upp när de spawnas om de råkar vara liggandes när de despawnade.
        rigidBodyMarionette.AddForce(new Vector3(0, 500, 0));
        //transform.position = new Vector3(0, 5, 0);

        randomTargetArea.SetTargetPositionToPlayArea();
        currentTargetObject = randomTargetObject;

        if (stateMachineOn)
        {
            currentState = new NewObjectThiefMoveToTargetState();
        }

        else
        {
            currentState = new NewObjectThiefEmptyStateForTesting();
        }

        currentState.Enter(this);

        
    }

    private void Update()
    {
        
        //Fulfixar
        if(!isDead)
        {
            //Fullfix för om target-objekt försvinner.
            if (currentTargetObject == null)
            {
                returnedState = new NewObjectThiefSearchState();
                StateSwap();
            }

            //Fulfix för om hand-objektet försvinner
            //else if (currentTargetObject.tag == "OutOfBounds" && (objectInHand == null || objectInHand.activeInHierarchy == false))
            //{
            //    objectInHand = null;
            //    returnedState = new NewObjectThiefSearchState();
            //    StateSwap();
            //}
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


    /*  MOVE BODYPART FUNCTIONS  */
    public void MoveToTarget()
    {
        Vector3 directionToTarget = GetDirectionToTarget(rigidBodyMarionette.transform.position);
        enemyJump.TryJump();
        MovePart(rigidBodyMarionette, directionToTarget, movementSpeedRun);
    }

    public void MoveAwayFromTarget()
    {
        Vector3 directionAwayFromTarget = -GetDirectionToTarget(rigidBodyMarionette.transform.position);
        enemyJump.TryJump();
        MovePart(rigidBodyMarionette, directionAwayFromTarget, movementSpeedRun);
    }

    public void MoveToTargetDuringSearch()
    {
        Vector3 directionToTarget = GetDirectionToTarget(rigidBodyMarionette.transform.position);
        enemyJump.TryJump();
        MovePart(rigidBodyMarionette, directionToTarget, movementSpeedSearch);
    }

    public void MoveArm()
    {
        Vector3 directionToTarget = GetDirectionToTarget(rigidBodyArm.transform.position);
        MovePart(rigidBodyArm, directionToTarget, armSpeed);
    }

    private void MovePart(Rigidbody rigidBodyToMove, Vector3 moveAgainst, float forceToMoveWith)
    {
        moveAgainst = moveAgainst.normalized;
        Vector3 movement = new Vector3(moveAgainst.x, 0, moveAgainst.z);
        rigidBodyToMove.AddForce(movement * forceToMoveWith);
    }



    /*  DIRECTIONS & POSITIONS  */
    public Vector3 GetDirectionToTarget(Vector3 bodyPartToMovePosition)
    {
        if(currentTargetObject != null)
        {
            Vector3 directionToTarget = currentTargetObject.transform.position - bodyPartToMovePosition;
            return directionToTarget;
        }
        else
        {
            Debug.Log("no current target. Couldn't get direction");
            return new Vector3(0, 0, 0);
        }
        
    }

    public float GetDistanceToTarget(Vector3 fromPosition)
    {
        return Vector3.Distance(fromPosition, currentTargetObject.transform.position);
    }

    public Vector3 GetTargetPosition()
    {
        return currentTargetObject.transform.position;
    }

    public Vector3 GetMarionetteStringPosition()
    {
        return rigidBodyMarionette.transform.position;
    }

    public Vector3 GetArmPosition()
    {
        return rigidBodyArm.transform.position;
    }

    public float GetDistanceBeforeTargetIsReached()
    {
        return distanceBeforeTargetIsReached;
    }

    public float GetDistanceBeforeEnemyDespawn()
    {
        return distanceBeforeEnemyDespawn;
    }

    public float GetDistanceBeforeTargetIsPickedUp()
    {
        return distanceBeforeTargetIsPickedUp;
    }




    /*  FIND OBJECT  */
    public void FindNewCurrentTargetObjectWithTag(string tag)
    {
        currentTargetObject = GameObject.FindGameObjectWithTag(tag);
    }

    public void DespawnDuringFlee()
    {
        if(objectInHand != null)
        {
            //Destroy(objectInHand);
            PoolManager.ReturnPickup(objectInHand.GetComponent<Pickup>());
        }

        ReturnToObjectPool();
        //Destroy(gameObject);
        
    }

    public void ReturnToObjectPool()
    {
        if (objectSearcher.GetTargetType() == PickupType.Ornament)
        {
            PoolManager.ReturnEnemy(gameObject, EnemyType.OrnamentTheif);
        }
        else if (objectSearcher.GetTargetType() == PickupType.Body)
        {
            PoolManager.ReturnEnemy(gameObject, EnemyType.Zombie);
        }
        else
        {
            Debug.Log("Couldn't return enemy to pool!");
        }
    }

    public void DropItem()
    {
        pickupHand.DestroyJoint();
        objectInHand = null;
    }

    public void Die()
    {
        isDead = true;
        returnedState = new NewObjectThiefDeathState();
        StateSwap();
    }

    public void ToggleMarionetteStrings(bool active)
    {
        //for (int i = 0; i < marionetteStringGameObjects.Length; i++)
        //{
            //stringGameObjects[i]
            marionetteStringGameObjects[0].SetActive(active);
        //}
    }


    /* TIMERS */

    public float GetTimeBeforeTryingNewPickupTarget()
    {
        return timeBeforeTryingNewPickupTarget;
    }

    public float GetTimeBeforeTryingNewTarget()
    {
        return timeBeforeTryingNewTarget;
    }

    public float GetDespawnAfterDeathTime()
    {
        return despawnAfterDeathTime;
    }

}
