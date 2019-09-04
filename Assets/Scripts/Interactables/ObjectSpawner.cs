using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : Interactable
{
    [SerializeField] private GameObject spanwPrefab;

    public override Interactable Interact()
    {
        return Instantiate(spanwPrefab).GetComponent<Interactable>();
    }
}
