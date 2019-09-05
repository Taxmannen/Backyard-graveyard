using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Script by Christopher Tåqvist */

public class ObjectThief : MonoBehaviour
{
    //States & Commands
    ObjectThiefState currentState;
    ObjectThiefState returnedState;

    private Rigidbody rigidBody;

    [SerializeField] private string objectToStealName;
    private EnemyStealTarget currentStealTarget;

    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float pickupDistance;

    [SerializeField] private GameObject distanceCheckForDespawnObject;
    [SerializeField] private float distanceToDespawn;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        currentState = new ObjectThiefSearchState();
        currentState.Enter(this);
    }

    private void Update()
    {
        //Testing forces
        if(Input.GetKey(KeyCode.W))
        {
            rigidBody.AddForce(new Vector3(speed * 10 ,0,speed * 10));
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


    //Used for finding items
    [SerializeField] private float timeBetweenEachSearch = 10;
    public float GetTimeBetweenEachSearch()
    {
        return timeBetweenEachSearch;
    }

    public EnemyStealTarget[] FindAllEnemyStealTargets()
    {
        return FindObjectsOfType<EnemyStealTarget>();
    }

    public string GetNameOfObjectToSteal()
    {
        return objectToStealName;
    }

    public EnemyStealTarget GetCurrentTargetToSteal()
    {
        return currentStealTarget;
    }

    public void SetNewTargetToSteal(EnemyStealTarget newTarget)
    {
        currentStealTarget = newTarget;
    }



    public float GetSpeed()
    {
        return speed;
    }

    public float GetPickupDistance()
    {
        return pickupDistance;
    }


    //Rigidbody stuff
    public Rigidbody AccessRigidBody()
    {
        return rigidBody;
    }

    public void AddForceToRigidBody(Vector3 vector3)
    {
        rigidBody.AddForce(vector3);
    }

    public void Move(Vector2 direction)
    {
        rigidBody.velocity = new Vector3(direction.x * speed, rigidBody.velocity.y, direction.y * speed);
    }

    public float GetDistanceToDespawnCheckObject()
    {
        return Vector3.Distance(transform.position, distanceCheckForDespawnObject.transform.position);
    }

    public float GetDistanceToDespawn()
    {
        return distanceToDespawn;
    }


    public void DestroyStolenTarget()
    {
        Destroy(currentStealTarget.gameObject);
    }

    public void DestroyObjectThief()
    {
        Destroy(gameObject);
    }

}
