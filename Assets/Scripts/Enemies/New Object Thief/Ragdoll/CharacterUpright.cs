using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script taken from tutorial https://medium.com/640-lab-tutorial-library/unity3d-tutorial-97-vert-active-ragdoll-ae7cc136ae08
public class CharacterUpright : MonoBehaviour
{

    new protected Rigidbody rigidBody;
    public bool keepUpright = true;
    public float uprightForce = 10;
    public float uprightOffset = 1.45f;
    public float additionalUpwardForce = 10;
    public float dampenAngularForce = 0;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.maxAngularVelocity = 40;
    }

    private void FixedUpdate()
    {
        if(keepUpright)
        {
            //USE TWO FORCES PULLING UP AND DOWN AT THE TOP AND BOTTOM OF THE OBJECT TO PULL UPRIGHT
            //THIS TECHNIQUE CAN BE USED FOR PULLING AN OBJECT TO FACE ANY VECTOR

            rigidBody.AddForceAtPosition(new Vector3(0, (uprightForce + additionalUpwardForce), 0),
                transform.position + transform.TransformPoint(new Vector3(0, uprightOffset, 0)), ForceMode.Force);

            rigidBody.AddForceAtPosition(new Vector3(0, (-uprightForce), 0),
                transform.position + transform.TransformPoint(new Vector3(0, -uprightOffset, 0)), ForceMode.Force);
        }

        if(dampenAngularForce > 0)
        {
            rigidBody.angularVelocity *= (1 - Time.deltaTime * dampenAngularForce);
        }
    }

}
