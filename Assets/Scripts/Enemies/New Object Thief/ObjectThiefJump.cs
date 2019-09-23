using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThiefJump : MonoBehaviour
{
    [Header("Components (DO NOT TOUCH)")]
    [SerializeField] private EnemyGroundCheck groundCheck;
    [SerializeField] private Rigidbody bodyRigidBody;
    [SerializeField] private Rigidbody marionetteRigidBody;
    //private Rigidbody pickupObjectRigidBody;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 100;
    [SerializeField] private float fleeJumpForce = 2000;

    /*JumpCheck*/
    private bool canJump = false;


    public void TryJump()
    {
        if (groundCheck.GetGrounded() && canJump)
        {
            Jump(jumpForce, bodyRigidBody);
            canJump = false;
        }

        if (bodyRigidBody.velocity.y < 0)
        {
            canJump = true;
        }
    }

    public void FleeJump(Rigidbody pickupRigidBody)
    {
        Jump(fleeJumpForce, marionetteRigidBody);
        if(pickupRigidBody != null)
        {
            Jump(fleeJumpForce, pickupRigidBody);
        }
        
    }


    private void Jump(float jumpForce, Rigidbody rigidBody)
    {
        rigidBody.AddForce(0, jumpForce, 0);
    }

    
}
