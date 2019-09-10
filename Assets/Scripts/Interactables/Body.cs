using UnityEngine;

public enum BodyType { Red, Green, Blue, [System.ObsoleteAttribute] NumberOfTypes, [System.ObsoleteAttribute] None };

/* Script Created By Petter and Helped By Daniel */
public class Body : Pickup
{
    [Header("Body")]
    [SerializeField] private BodyType bodyType;
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private GameObject headPrefab;
    [SerializeField] private Transform headPosition;

    private GameObject ghostObject;
    private FixedJoint fixedJoint;

    public Head Head { get; private set; }

    private void Awake()
    {
        SetColor();
        Head newHead = Instantiate(headPrefab).GetComponent<Head>();
        AttachHeadToCorrectPosition(newHead);
    }

    protected override void Start()
    {
        base.Start();
        SnapOnPickup = false;
    }

    private void OnTriggerStay(Collider other)
    {
        Head head = other.GetComponent<Head>();
        if (!Head && other.CompareTag("Interactable") && head)
        {
            if (ghostObject == null)
            {
                ghostObject = other.GetComponent<Interactable>().CreateGhostObject(headPosition.position, transform.rotation.eulerAngles);
                ghostObject.transform.SetParent(transform);
            }

            if (!head.ActiveHand || !ActiveHand)
            {
                if (!head.ActiveHand && !ActiveHand)
                {
                    return;
                }

                else
                {
                    AttachHeadToCorrectPosition(head);
                    Destroy(ghostObject);
                }
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

            if (Head)
            {
                if (fixedJoint != null)
                {
                    Destroy(gameObject.GetComponent<FixedJoint>());
                    fixedJoint = null;
                }
                other.transform.SetParent(null);
                Head = null;
            }
        }
    }

    private void SetColor()
    {
        bodyType = (BodyType)Random.Range(0, 3);
        if (bodyType == BodyType.Blue)
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
        }
        else if (bodyType == BodyType.Green)
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
        }
        else if (bodyType == BodyType.Red)
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        }
    }


    private void AttachHeadToCorrectPosition(Head head)
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
        Head = head;
    }

    public void SetRigidbodyConstraints(bool setConstraints)
    {
        Rigidbody[] allRigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidbody in allRigidbodies)
        {
            rigidbody.isKinematic = setConstraints;
            RigidbodyConstraints rbConstraints = setConstraints == true ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None;
            rigidbody.constraints = rbConstraints;

            Collider[] colliders = GetComponentsInChildren<Collider>();
            foreach (Collider col in colliders) if (!col.isTrigger) col.enabled = !setConstraints;
        }
    }

    public BodyType GetBodyType() { return bodyType; }

    public void Test()
    {
        ActiveHand = null;
        if (Head != null) Head.ActiveHand = null;
    }
}