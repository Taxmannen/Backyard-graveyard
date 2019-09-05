using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script Created By Petter */
public class Body : Pickup
{
    [Header("Body")]
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private GameObject headPrefab;

    [SerializeField] private Transform headPosition;

    [SerializeField] private FixedJoint fixedJoint;
    [SerializeField] private BoxCollider myCol;
    [SerializeField] private SphereCollider otherCol;

    public bool fullBody;
    
    public enum MyColor {Blue, Green, Red};

    private void Awake()
    {
        SetColor();
        Physics.IgnoreCollision(myCol, otherCol);

        //For test, delete!
        //SpawnFullBody(new Vector3(0, 0, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Head>())
        {
            other.gameObject.transform.position = headPosition.position;
            other.transform.rotation = transform.rotation;
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
        /*if (other.GetComponent<Head>())
        {
            if (fixedJoint != null)
            {
                Destroy(gameObject.GetComponent<FixedJoint>());
                fixedJoint = null;
            }
            fullBody = false;
        }*/
    }

    private void SetColor()
    {
        MyColor myColor = (MyColor)Random.Range(0, 3);
        if (myColor == MyColor.Blue)
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
        }
        else if (myColor == MyColor.Green)
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
        }
        else if (myColor == MyColor.Red)
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        }
    }

    public void SpawnFullBody(Vector3 spawnPosition)
    {
        GameObject newBody = Instantiate(bodyPrefab, spawnPosition, Quaternion.identity);
        GameObject newHead = Instantiate(headPrefab, spawnPosition, Quaternion.identity);
        newHead.transform.position = newBody.GetComponent<Body>().headPosition.position;
        //newHead.transform.rotation = Quaternion.Euler()
        if (!newBody.GetComponent<FixedJoint>())
        {
            newBody.AddComponent<FixedJoint>();
        }
        if (newBody.GetComponent<FixedJoint>())
        {
            newBody.GetComponent<FixedJoint>().connectedBody = newHead.GetComponent<Rigidbody>();
        }
        newBody.GetComponent<Body>().fullBody = true;
    }
}
