using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script Created By Petter */
public class Body : Pickup
{
    [SerializeField] private Transform headPosition;

    [SerializeField] private FixedJoint fixedJoint;

    public bool fullBody;
    
    public enum MyColor {Blue, Green, Red};

    private void Awake()
    {
        MyColor myColor = (MyColor)Random.Range(0, 3);
        if (myColor == MyColor.Blue)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else if (myColor == MyColor.Green)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else if (myColor == MyColor.Red)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Head>())
        {
            other.gameObject.transform.position = headPosition.position;
            other.gameObject.transform.rotation = Quaternion.Euler((transform.rotation.x - 90), transform.rotation.y, transform.rotation.z);
            //other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            fullBody = true;
            if (fixedJoint != null)
            {
                Destroy(fixedJoint);
                fixedJoint = null;
            }
            fixedJoint = gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = other.gameObject.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Head>())
        {
            if (fixedJoint != null)
            {
                Destroy(gameObject.GetComponent<FixedJoint>());
                fixedJoint = null;
            }
            //other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            fullBody = false;
        }
    }
}
