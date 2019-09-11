using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollControllerTemp : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody rigidBody;

    [SerializeField] private Transform target;
    private Vector3 movementForce;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        
    }

    private void FixedUpdate()
    {
        movementForce = target.position - transform.position;
        movementForce = movementForce.normalized;

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        Vector3 movement = new Vector3(movementForce.x, 0.0f, movementForce.z);

        rigidBody.AddForce(movement * speed);
    }
}
