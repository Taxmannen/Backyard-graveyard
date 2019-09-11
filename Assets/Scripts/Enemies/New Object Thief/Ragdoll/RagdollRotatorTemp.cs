using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollRotatorTemp : MonoBehaviour
{
    private Rigidbody rigidBody;
    public float rotateSpeed;

    Vector3 rotationLeft;
    Vector3 rotationRight;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rotationLeft.Set(0f, -rotateSpeed, 0f);
        rotationRight.Set(0f, rotateSpeed, 0f);

        rotationLeft = -rotationLeft.normalized * -rotateSpeed;
        rotationRight = rotationRight.normalized * rotateSpeed;

        Quaternion deltaRotationLeft = Quaternion.Euler(rotationLeft * Time.fixedDeltaTime);
        Quaternion deltaRotationRight = Quaternion.Euler(rotationRight * Time.fixedDeltaTime);

        if(Input.GetKey(KeyCode.E))
        {
            rigidBody.MoveRotation(rigidBody.rotation * deltaRotationLeft);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            rigidBody.MoveRotation(rigidBody.rotation * deltaRotationRight);
        }
    }
}
