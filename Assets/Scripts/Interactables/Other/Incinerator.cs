using UnityEngine;

/* Script made by Petter */
/* Edited by Tåqvist */
public class Incinerator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            TaskCard taskCard;
            if (taskCard = other.GetComponent<TaskCard>())
            {
                taskCard.StartReturnToBoxCoroutine();
            }

            else
            {
                Ornament ornament = other.GetComponent<Ornament>();
                if (!PoolManager.ReturnOrnament(ornament))
                {
                    Destroy(other.gameObject);
                }
            }
            
        }
    }
}