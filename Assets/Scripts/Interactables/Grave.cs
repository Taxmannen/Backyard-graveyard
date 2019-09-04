using System.Collections.Generic;
using UnityEngine;

/* Script Made By Daniel */
public class Grave : Interactable
{
    [Header("Grave")]
    [SerializeField] private int maxAmountOfDirtLayers;

    [SerializeField] private GameObject dirt;
    [SerializeField] private GameObject dirtLayer;
    [SerializeField] private Transform dirtLayerParent;

    private List<GameObject> dirtLayerList = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < maxAmountOfDirtLayers; i++) AddDirt();
    }
         
    public override Interactable Interact()
    {
        if (dirtLayerList.Count > 0)
        {
            RemoveDirt();
            return Instantiate(dirt).GetComponent<Interactable>();
        }
        else return null;
    }

    private void RemoveDirt()
    {
        GameObject currentLayer = dirtLayerList[dirtLayerList.Count - 1];
        dirtLayerList.Remove(currentLayer);
        Destroy(currentLayer);
    }

    public void AddDirt()
    {
        if (dirtLayerList.Count < maxAmountOfDirtLayers)
        {
            float scale = 1 / (float)maxAmountOfDirtLayers;
            GameObject current = Instantiate(dirtLayer, dirtLayerParent);
            current.transform.localScale = new Vector3(current.transform.localScale.x, 1 / (float)maxAmountOfDirtLayers, current.transform.localScale.z);
            current.transform.localPosition = new Vector3(0, (-0.5f + (scale/2)) + (scale * dirtLayerList.Count), 0);
            dirtLayerList.Add(current);
        }   
    }
}