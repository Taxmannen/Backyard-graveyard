using System.Collections.Generic;
using UnityEngine;

/* Script Made By Daniel */
public class Grave : Interactable
{
    #region Variables
    [Header("Grave")]
    [SerializeField] private int maxAmountOfDirtLayers;

    [SerializeField] private GameObject dirt;
    [SerializeField] private GameObject dirtLayer;
    [SerializeField] private Transform dirtLayerParent;
    [SerializeField] private Transform objectsInGraveTransform;

    [Header("Ornament Placements")]
    [SerializeField] private OrnamentPlacement[] ornamentPlacements;

    [Header("Body")]
    [SerializeField, ReadOnly] private Body body = null;

    [Header("Debug")]
    [SerializeField] private List<Pickup> objectsInGrave = new List<Pickup>();

    private List<GameObject> dirtLayerList = new List<GameObject>();
    private Vector3 bodyOffset = new Vector3(0, 0.1f, -0.125f);
    #endregion

    #region Awake
    private void Awake()
    {
        PlayButton.PlayEvent += ClearBodyAndAddDirt;
        for (int i = 0; i < maxAmountOfDirtLayers; i++) AddDirt();
    }
    #endregion

    #region On Trigger
    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other.CompareTag("Interactable"))
        {
            Pickup pickup = other.GetComponent<Pickup>();
            if (pickup && pickup.GetPickupType() != PickupType.TaskCard)
            {
                objectsInGrave.Add(pickup);
                pickup.transform.SetParent(objectsInGraveTransform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger && other.CompareTag("Interactable"))
        {
            Pickup pickup = other.GetComponent<Pickup>();
            if (pickup && pickup.GetPickupType() != PickupType.TaskCard)
            {
                objectsInGrave.Remove(pickup);
                pickup.transform.SetParent(null);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactable") && body == null)
        {
            Body body = other.GetComponent<Body>();
            if (body && !body.ActiveHand && !body.Head?.ActiveHand) AddBody(body);
        }
    }
    #endregion

    #region Interact
    public override Interactable Interact()
    {
        if (dirtLayerList.Count > 0)
        {
            RemoveDirt();
            return Instantiate(dirt).GetComponent<Interactable>();
        }
        else if (dirtLayerList.Count == 0 && body)
        {
            return RemoveBody();
        }
        else return null;
    }
    #endregion

    #region Body
    private void AddBody(Body newBody)
    {
        if (body == null && dirtLayerList.Count == 0)
        {
            body = newBody;
            body.transform.position = transform.position + (transform.rotation * bodyOffset);
            body.transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(90, 90, 90));
            body.SetRigidbodyConstraints(true);
            body.transform.SetParent(objectsInGraveTransform);
            body.IsInGrave = true;
        }
    }

    private Body RemoveBody()
    {
        Body currentBody = body;
        currentBody.SetRigidbodyConstraints(false);
        currentBody.IsInGrave = false;
        currentBody.SetSnapOnPickupAfterGrave();
        currentBody.transform.SetParent(null);
        currentBody.SetColliderState(true);
        body = null;
        return currentBody;
    }
    #endregion

    #region Dirt
    public void AddDirt(Dirt dirt = null)
    {
        if (dirtLayerList.Count < maxAmountOfDirtLayers)
        {
            float scale = 0.5f / (float)maxAmountOfDirtLayers;
            GameObject current = Instantiate(dirtLayer, dirtLayerParent);
            current.transform.localScale = new Vector3(current.transform.localScale.x, scale, current.transform.localScale.z);
            current.transform.localPosition = new Vector3(0, (scale / 2) + (scale * dirtLayerList.Count), 0);
            if (dirt) objectsInGrave.Remove(dirt);
            dirtLayerList.Add(current);
        }
        if (dirtLayerList.Count == maxAmountOfDirtLayers) CheckTaskCompletion();
    }

    private void RemoveDirt()
    {
        GameObject currentLayer = dirtLayerList[dirtLayerList.Count - 1];
        dirtLayerList.Remove(currentLayer);
        Destroy(currentLayer);
    }
    #endregion

    #region Grave
    public void ResetGrave()
    {
        ClearBodyAndAddDirt();
        foreach (OrnamentPlacement placement in ornamentPlacements) placement.ReturnOrnament();
    }

    private void ClearBodyAndAddDirt()
    {
        if (body != null)
        {
            Destroy(body.gameObject);
            body = null;
        }
        for (int i = 0; i < maxAmountOfDirtLayers; i++) AddDirt(); //Checka
        foreach (Pickup pickup in objectsInGrave)
        {
            if (pickup && !pickup.ActiveHand)
            {
                if (pickup.GetPickupType() == PickupType.Ornament) (pickup as Ornament).ReturnToPool();
                else Destroy(pickup.gameObject);
            }
        }
        objectsInGrave.Clear();
    }
    #endregion

    #region Task
    public void CheckTaskCompletion() //Made By Simon & Daniel
    {
        if (body == null || body.Head == null || dirtLayerList.Count != maxAmountOfDirtLayers) return;

        List<OrnamentType> ornamentType = new List<OrnamentType>();
        foreach (OrnamentPlacement placement in ornamentPlacements)
        {
            Ornament ornament = placement.GetPlacedOrnament();
            if (ornament) ornamentType.Add(ornament.GetOrnamentType());
        }

        Head head = body.Head;
        if (head != null)
        {
            foreach (Task task in FindObjectOfType<TaskManager>().tasks)
            {
                if (task.CheckTask(head.GetHeadType(), body.GetBodyType(), ornamentType, body.GetTreatmentType(), head.GetTreatmentType()))
                {
                    Debug.Log("TaskGrave: FINISHED TASK, AWW YEAH");
                    ResetGrave();
                    return;
                }
            }
        }
    }
    #endregion
}