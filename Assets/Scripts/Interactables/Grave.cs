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

    private int counter;

    private List<Transform> apa = new List<Transform>();

    private void Awake()
    {
        for (int i = 0; i < maxAmountOfDirtLayers; i++)
        {
            Transform current = Instantiate(dirtLayer, dirtLayerParent).transform;
            current.localScale = new Vector3(current.localScale.x,  1 / (float)maxAmountOfDirtLayers, current.localScale.z);
            current.localPosition = new Vector3(0, -0.45f + (current.localScale.y * i), 0);
            apa.Add(current);
            counter++;
        }
    }
         
    public override Interactable Interact()
    {
        if (counter > 0)
        {
            Dig();
            return Instantiate(dirt).GetComponent<Interactable>();
        }
        else return null;
    }

    private void Dig()
    {
        counter--;
        Destroy(apa[counter].gameObject);
    }

    private void AddDirt()
    {

    }
}