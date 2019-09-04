using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script Made By Svedlund */
public class Door : MonoBehaviour
{
    public Door targetDoor;
    public bool doorOnCooldown = false;
    

    public IEnumerator DoorCooldown()
    {
        Debug.Log(name);
        doorOnCooldown = true;
        yield return new WaitForSeconds(3f);
        doorOnCooldown = false;
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!doorOnCooldown && other.tag == "Player")
        {
            StartCoroutine(targetDoor.DoorCooldown());
            Debug.Log("Door activated");
            Vector3 relativePositionToDoor = transform.position - other.gameObject.transform.root.position;
            other.gameObject.transform.root.position = targetDoor.transform.position - relativePositionToDoor;
            //other.gameObject.transform.root.LookAt(targetDoor.transform);
        }
    }
}
