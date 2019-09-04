using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script Created By Petter */
public class Body : Pickup
{
    [SerializeField] private Transform headPosition;

    public bool hasHead = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Head>() && !hasHead)
        {
            other.gameObject.transform.position = headPosition.position;
            gameObject.GetComponent<SpringJoint>().connectedBody = other.gameObject.GetComponent<Rigidbody>();
            hasHead = true;
            gameObject.AddComponent<FullBody>();
        }
    }
}
