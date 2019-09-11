using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundCheck : MonoBehaviour
{
    [SerializeField] private float groundHeight = 0.5f;
    [SerializeField] private float checkRate = 0.5f;
    private bool grounded = false;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float heightOffSet = 0.25f;

    private void Start()
    {
        InvokeRepeating("GroundCheck", 0, checkRate);
    }

    private void GroundCheck()
    {
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + heightOffSet, transform.position.z), Vector3.down, groundHeight + heightOffSet, groundLayer))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    public bool GetGrounded()
    {
        return grounded;
    }

}
