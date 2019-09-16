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
                    //FlowerPool.GetInstance().ReturnToPool(other);
                    break;
                case OrnamentType.Candle:
                    Debug.Log("Candle");
                    //CandlePool.GetInstance().ReturnToPool(other);
                    break;
                case OrnamentType.Heart:
                    Debug.Log("Heart");
                    //HeartPool.GetInstance().ReturnToPool(other);
                    break;
                case OrnamentType.Statue:
                    Debug.Log("Statue");
                    //StatuePool.GetInstance().ReturnToPool(other);
                    break;
            }
            return true;
        }
        else return false;
    }
}