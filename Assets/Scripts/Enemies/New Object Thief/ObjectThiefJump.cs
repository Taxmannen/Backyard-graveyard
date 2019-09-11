using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThiefJump : MonoBehaviour
{
    [Header("Components (DO NOT TOUCH)")]
    [SerializeField] private EnemyGroundCheck groundCheck;
    [SerializeField] private Rigidbody rigidBodyToJump;
    [SerializeField] private ConstantForce constantForce;
    
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 100;

    /*JumpCheck*/
    private bool canJump = false;


    public void TryJump()
    {
        if (groundCheck.GetGrounded() && canJump)
        {
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
        rigidBodyToJump.AddForce(0, jumpForce, 0);
    }

    
}
