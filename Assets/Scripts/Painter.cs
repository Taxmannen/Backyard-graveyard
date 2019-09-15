using System.Collections.Generic;
using UnityEngine;

/* Script Made By Daniel */
public class Painter : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    [SerializeField] private Transform rayTransform;

    private List<GameObject> contactObjects = new List<GameObject>();
    private PaintPool pool;
    private Vector3 lastPaintPos;
    private float distance = 0.06f;

    private void Start()
    {
        pool = GetComponent<PaintPool>();
    }

    private void Update()
    {
        Vector3 forward = rayTransform.TransformDirection(Vector3.forward);
        Debug.DrawRay(rayTransform.position, forward * distance, Color.green);
        if (contactObjects.Count > 0) Paint(forward);
    }

    private void Paint(Vector3 forward)
    {
        if (Physics.Raycast(rayTransform.position, forward, out RaycastHit rayHit, distance))
        {
            //Debug.Log(rayHit.collider.gameObject.name);
            //float distanceBetweenPaint = (lastPaintPos - rayHit.point).sqrMagnitude;
            if (!rayHit.collider.isTrigger /*&& distanceBetweenPaint > 1*/)
            {
                GameObject paint = pool?.Get(rayHit.point, Quaternion.FromToRotation(Vector3.up, rayHit.normal), rayHit.collider.transform);
                if (paint)
                {
                    if (rayHit.collider.CompareTag("Interactable"))
                    {
                        BodyPart bodyPart = GetComponent<BodyPart>();
                        if (bodyPart) bodyPart.SetTreatment(TreatmentType.MakeUp, paint);
                    }
                    lastPaintPos = rayHit.point;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) contactObjects.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) contactObjects.Remove(other.gameObject);
    }
}