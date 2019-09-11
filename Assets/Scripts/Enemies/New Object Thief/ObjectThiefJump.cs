using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThiefJump : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBodyToJump;
    [SerializeField] private float forceToAdd = 10;
    [SerializeField] private ConstantForce constantForce;
    [SerializeField] private EnemyGroundCheck groundCheck;

    //Kolla vilkey håll velocity.y går åt
    //Sätt canJump = false
    //När velocity.y


    private bool canJump = false;

    private float jumpTimer = 0.5f;

    public void TryJump()
    {
        if (groundCheck.GetGrounded() && canJump)
        {
            //Debug.Log("Jumped");
            Jump();
            canJump = false;
        }

        if (rigidBodyToJump.velocity.y < 0)
        {
            canJump = true;
        }
    }

    private void Jump()
    {
        rigidBodyToJump.AddForce(0, forceToAdd, 0);
    }

    
}
