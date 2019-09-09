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

    [Header("Ornament Containers")]
    [SerializeField] private OrnamentContainer[] ornamentContainers;

    [Header("Body")]
    [SerializeField] private Body body = null; //For Debug!

    private List<GameObject> dirtLayerList = new List<GameObject>();
    private Vector3 bodyOffset = new Vector3(0, -0.5f, 0);
    #endregion

    private void Awake()
    {
        for (int i = 0; i < maxAmountOfDirtLayers; i++) AddDirt();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactable") && body == null)
        {
            Body body = other.GetComponent<Body>();
            if (body != null) AddBody(body);
        }
    }

    public override Interactable Interact()
    {
        if (dirtLayerList.Count > 0)
        {
            RemoveDirt();
            return Instantiate(dirt).GetComponent<Interactable>();
        }
        else if (dirtLayerList.Count == 0 && body != null)
        {
            body.SetRigidbodyConstraints(false);
            return body;
        }
        else return null;
    }

    private void AddBody(Body newBody)
    {
        if (body == null && dirtLayerList.Count == 0)
        {
            body = newBody;
            body.SetRigidbodyConstraints(true);
            body.transform.position = transform.position + bodyOffset;
            body.transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(90, 90, 90));
        }
    }

    public void AddDirt()
    {
        if (dirtLayerList.Count < maxAmountOfDirtLayers)
        {
            float scale = 1 / (float)maxAmountOfDirtLayers;
            GameObject current = Instantiate(dirtLayer, dirtLayerParent);
            current.transform.localScale = new Vector3(current.transform.localScale.x, 1 / (float)maxAmountOfDirtLayers, current.transform.localScale.z);
            current.transform.localPosition = new Vector3(0, (-0.5f + (scale / 2)) + (scale * dirtLayerList.Count), 0);
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

    public void ResetGrave()
    {
        if (body != null) Destroy(body);
        foreach (OrnamentContainer container in ornamentContainers) container.DestroyOrnament();
        for (int i = 0; i < (maxAmountOfDirtLayers - dirtLayerList.Count); i++) AddDirt();
    }

    public void CheckTaskCompletion()
    {
        List<OrnamentType> tempo = new List<OrnamentType>();
        foreach (OrnamentContainer container in ornamentContainers)
        {
            tempo.Add(container.GetPlacedOrnament().GetOrnamentType());
        }

        foreach (Task task in FindObjectOfType<TaskManager>().tasks)
        {
            if (task.CheckTask(body.Head.GetHeadType(), body.GetBodyType(), tempo))
            {
                Debug.Log("TaskGrave: FINISHED TASK, AWW YEAH");
            }
        }
    }
}