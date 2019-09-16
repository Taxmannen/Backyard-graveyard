using UnityEngine;

/* Script made by Petter */
public class Incinerator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            if (!ReturnToPool(other.gameObject))
            {
                Destroy(other.gameObject);
            }
        }
    }

    // Made by Daniel 
    private bool ReturnToPool(GameObject other)
    {
        Ornament ornament = other.GetComponent<Ornament>();
        if (ornament)
        {
            switch (ornament.GetOrnamentType())
            {
                case OrnamentType.Flower:
                    Debug.Log("Flower");
                    break;
                case OrnamentType.Candle:
                    Debug.Log("Candle");
                    break;
                case OrnamentType.Heart:
                    Debug.Log("Heart");
                    break;
                case OrnamentType.Statue:
                    Debug.Log("Statue");
                    break;
            }
            return true;
        }
        else return false;
    }
}