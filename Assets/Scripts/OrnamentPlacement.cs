using UnityEngine;

public class OrnamentPlacement : MonoBehaviour
{
    //Debug
    [SerializeField] private Ornament placedOrnament;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            if (placedOrnament == null && other.gameObject.GetComponent<Ornament>() != null)
            {
                placedOrnament = other.gameObject.GetComponent<Ornament>();
                /*if (placedOrnament.CanPlace())*/ placedOrnament.PlaceOrnament(this, transform.position);
            }
        }
    }

    public void RemoveOrnament()
    {
        placedOrnament = null;
    }
}