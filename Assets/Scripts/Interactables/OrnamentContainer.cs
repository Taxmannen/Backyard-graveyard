using UnityEngine;

/* Script Made By Daniel */
public class OrnamentContainer : MonoBehaviour
{
    private Ornament placedOrnament;

    private void OnCollisionStay(Collision other)
    {
        if (placedOrnament == null && other.gameObject.CompareTag("Interactable"))
        {
            if (other.gameObject.GetComponent<Ornament>() != null)
            {
                Ornament currentOrnamnet = other.gameObject.GetComponent<Ornament>();
                if (currentOrnamnet.ActiveHand == null)
                {
                    placedOrnament = currentOrnamnet;
                    placedOrnament.PlaceOrnament(this, transform.position);
                }
            }
        }
    }

    public void RemoveOrnament()
    {
        placedOrnament = null;
    }
}