using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script Created By Petter and Helped By Daniel */
public class Body : Pickup
{
    [Header("Body")]
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private GameObject headPrefab;
    [SerializeField] private Transform headPosition;
    public bool fullBody;

    [Header("Head")]
    public GameObject myCurrentHead;
    [SerializeField] GameObject ghostObject;

    public FixedJoint fixedJoint;
    
    public enum MyColor {Blue, Green, Red};

    private void Awake()
    {
        SetColor();
        GameObject newHead = Instantiate(headPrefab);
        AttachHeadToCorrectPosition(newHead);
    }

    protected override void Start()
    {
        base.Start();
        SnapOnPickup = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactable") && other.GetComponent<Head>() && !fullBody)
        {
            if (ghostObject == null)
            {
                ghostObject = other.GetComponent<Interactable>().CreateGhostObject(headPosition.position, transform.rotation.eulerAngles);
                ghostObject.transform.SetParent(transform);
            }

            if (!other.GetComponent<Head>().ActiveHand || !ActiveHand)
            {
                if (!other.GetComponent<Head>().ActiveHand && !ActiveHand) { return; }
                
                else { AttachHeadToCorrectPosition(other.gameObject); Destroy(ghostObject); }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable") && other.GetComponent<Head>())
        {
            if (ghostObject != null)
            {
                Destroy(ghostObject);
            }

            if (fullBody)
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

    
    private void AttachHeadToCorrectPosition(GameObject head)
    {
        head.transform.position = headPosition.position;
        head.transform.rotation = transform.rotation;

        if (fixedJoint != null)
        {
            Destroy(fixedJoint);
            fixedJoint = null;
        }
        fixedJoint = gameObject.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = head.gameObject.GetComponent<Rigidbody>();

        head.transform.SetParent(transform);
        myCurrentHead = head;
        fullBody = true;
    }

    public void SetRigidbodyConstraints(bool setConstraints)
    {
        Rigidbody[] allRigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidbody in allRigidbodies)
        {
            rigidbody.isKinematic = setConstraints;
            RigidbodyConstraints rbConstraints = setConstraints == true ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None;
            rigidbody.constraints = rbConstraints;
            rigidbody.GetComponent<Collider>().enabled = setConstraints;
        }
    }
}
