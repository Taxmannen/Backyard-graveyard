using System.Collections.Generic;
using UnityEngine;

/* Script Made By Daniel */
public class Painter : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject decal;
    [SerializeField] private Transform rayTransform;
    [SerializeField] private LayerMask layers;

    private List<GameObject> contactObjects = new List<GameObject>();
    private PaintPool pool;
    private Vector3 lastPaintPos;
    private float rayDistance = 0.06f;
    private float minDistance = 0.000025f;
    #endregion

    private void Start()
    {
        pool = GetComponent<PaintPool>();
    }

    private void Update()
    {
        if (contactObjects.Count > 0) Paint();
    }

    private void Paint()
    {
        Vector3 forward = rayTransform.TransformDirection(Vector3.forward);
        //Debug.DrawRay(rayTransform.position, forward * distance, Color.green);
        if (Physics.Raycast(rayTransform.position, forward, out RaycastHit rayHit, rayDistance, layers, QueryTriggerInteraction.Ignore))
        {
            //Debug.Log(rayHit.collider.gameObject.name);
            float distanceBetweenPaint = (lastPaintPos - rayHit.point).sqrMagnitude;
            if (!rayHit.collider.isTrigger && distanceBetweenPaint > minDistance)
            {
                GameObject paint = pool?.Get(rayHit.point, Quaternion.FromToRotation(Vector3.up, rayHit.normal), rayHit.collider.transform);
                if (paint)
                {
                    if (rayHit.collider.CompareTag("BodyPart"))
                    {
                        BodyPart bodyPart = rayHit.collider.transform.parent.GetComponent<BodyPart>();
                        if (bodyPart) bodyPart.SetTreatment(TreatmentType.MakeUp);
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