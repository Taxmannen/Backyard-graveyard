using UnityEngine;

/* Script Made By Daniel */
public class Dirt : Pickup
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Grave>() != null)
        {
            Grave grave = other.gameObject.GetComponent<Grave>();
            if (!ActiveHand)
            {
                grave.GetComponent<Grave>().AddDirt(gameObject);
                Destroy(gameObject);
            } 
        }
    }
}