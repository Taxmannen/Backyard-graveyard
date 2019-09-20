using UnityEngine;

public enum BodyType { Red, Green, Blue, [System.ObsoleteAttribute] NumberOfTypes, [System.ObsoleteAttribute] None };

/* Script Created By Petter and Helped By Daniel */
public class Body : BodyPart
{
    [Header("Body")]
    [SerializeField] private BodyType bodyType;
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private GameObject headPrefab;
    [SerializeField] private Transform headPosition;

    private FixedJoint fixedJoint;

    public Head Head { get; private set; }

    public bool IsInGrave { get; set; } = false;

    protected override void Awake()
    {
        base.Awake();
        SetColor();
        Head newHead = Instantiate(headPrefab).GetComponent<Head>();
        AttachHead(newHead);
    }

    protected override void Start()
    {
        base.Start();
        SnapOnPickup = false;
    }

    private void SetColor()
    {
        bool availableTasks = TaskManager.GetInstance().tasks != null && TaskManager.GetInstance().tasks.Count > 0;
        int randomChanceForMatch = RandomManager.GetRandomNumber(0, 101);
        if (availableTasks && randomChanceForMatch < PrototypeManager.GetInstance().GetCurrentWave().chanceOfCorrectBodyCombination)
        {
            BodyType taskBodyType = TaskManager.GetInstance().tasks[RandomManager.GetRandomNumber(0, TaskManager.GetInstance().tasks.Count)].Body;
            bodyType = taskBodyType;
        }
        else
        {
            bodyType = (BodyType)Random.Range(0, 3);
        }
        
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

    public void AttachHead(Head head)
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
        ConnectedBodyPart = head;
        head.ConnectedBodyPart = this;
        Head = head;
    }

    public void DetachHead()
    {
        if (fixedJoint != null)
        {
            Destroy(gameObject.GetComponent<FixedJoint>());
            fixedJoint = null;
        }

        ConnectedBodyPart = null;
        Head.ConnectedBodyPart = null;

        Head.transform.SetParent(null);
        Head = null;
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
            //foreach (Collider col in colliders) if (!col.isTrigger) col.enabled = !setConstraints;
        }
    }


    public BodyType GetBodyType() { return bodyType; }

    public Vector3 GetHeadPosition() { return headPosition.position; }
}