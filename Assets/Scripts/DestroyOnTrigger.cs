using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DestroyOnTrigger : MonoBehaviour
{
    Collider destroyTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) Destroy(gameObject);
    }
}
