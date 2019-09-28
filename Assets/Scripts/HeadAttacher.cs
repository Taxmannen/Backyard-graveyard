using UnityEngine;

/* Script Made By Petter */
public class HeadAttacher : MonoBehaviour
{
    private Body body;
    private GameObject ghostObject;

    private void Awake()
    {
        body = transform.parent.GetComponent<Body>();
    }

    private void OnTriggerStay(Collider other)
    {
        Head head = other.GetComponent<Head>();
        if (!body.Head && other.CompareTag("Interactable") && head && !head.ConnectedBodyPart)
        {
            if (ghostObject == null)
            {
                ghostObject = other.GetComponent<Interactable>().CreateGhostObject(body.GetHeadPosition(), transform.rotation.eulerAngles);
                ghostObject.transform.SetParent(transform);
            }

            if (!head.ActiveHand || !body.ActiveHand)
            {
                if (!head.ActiveHand && !body.ActiveHand)
                {
                    return;
                }

                else
                {
                    body.AttachHead(head);
                    Destroy(ghostObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!body.IsInGrave && other.CompareTag("Interactable") && other.GetComponent<Head>())
        {
            if (ghostObject != null)
            {
                Destroy(ghostObject);
            }
            if (body.Head)
            {
                body.DetachHead();
            }
        }
    }
}
