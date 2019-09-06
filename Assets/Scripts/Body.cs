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

    [SerializeField] private BoxCollider bodyCollider;
    [SerializeField] private BoxCollider headCollider;

    public FixedJoint fixedJoint { get; set; }

    public bool fullBody;
    public Head head;
    
    public enum MyColor {Blue, Green, Red};

    private void Awake()
    {
        SetColor();
        //Physics.IgnoreCollision(bodyCollider, headCollider);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SpawnFullBody(new Vector3(0,2,0));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactable") && other.GetComponent<Head>() && !fullBody)
        {
            //AttachHeadToCorrectPosition(other.gameObject, this.gameObject);

            //other.GetComponent<Collider>().enabled = false;

            if (!other.GetComponent<Head>().ActiveHand || !ActiveHand)
            {
                other.gameObject.transform.position = headPosition.position;
                other.transform.rotation = transform.rotation;

                if (fixedJoint != null)
                {
                    Destroy(fixedJoint);
                    fixedJoint = null;
                }
                fixedJoint = gameObject.AddComponent<FixedJoint>();
                fixedJoint.connectedBody = other.gameObject.GetComponent<Rigidbody>();

                other.transform.SetParent(transform);

                head = other.GetComponent<Head>();
                fullBody = true;
            }
            //other.GetComponent<Collider>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable") && other.GetComponent<Head>() && fullBody)
        {
            if (fixedJoint != null)
            {
                Destroy(gameObject.GetComponent<FixedJoint>());
                fixedJoint = null;
            }
            other.transform.SetParent(null);
            fullBody = false;
        }
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

    /*
    private void AttachHeadToCorrectPosition(GameObject head, GameObject body)
    {
        //head.GetComponent<Collider>().enabled = false;
        head.transform.SetParent(body.transform);
        head.transform.position = body.GetComponent<Body>().headPosition.position;
        head.transform.rotation = body.transform.rotation;

        if (fixedJoint != null)
        {
            Destroy(bodyScript.fixedJoint);
            fixedJoint = null;
        }
        fixedJoint = body.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = head.gameObject.GetComponent<Rigidbody>();

        fullBody = true;
        //head.GetComponent<Collider>().enabled = true;
    }

    public void SpawnFullBody(Vector3 spawnPosition)
    {
        GameObject newBody = Instantiate(bodyPrefab, spawnPosition, Quaternion.identity);
        GameObject newHead = Instantiate(headPrefab, spawnPosition, Quaternion.identity);

        AttachHeadToCorrectPosition(newHead, newBody);
        
        /*newHead.GetComponent<Collider>().enabled = false;
        newHead.transform.SetParent(newBody.transform);
        newHead.transform.position = newBody.GetComponent<Body>().headPosition.position;
        newHead.transform.rotation = newBody.transform.rotation;

        if (!newBody.GetComponent<FixedJoint>())
        {
            newBody.AddComponent<FixedJoint>();
        }
        if (newBody.GetComponent<FixedJoint>())
        {
            newBody.GetComponent<FixedJoint>().connectedBody = newHead.GetComponent<Rigidbody>();
        }
        newBody.GetComponent<Body>().fullBody = true;
        newHead.GetComponent<Collider>().enabled = true;*/
    //}
}
