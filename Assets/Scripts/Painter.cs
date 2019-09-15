using System.Collections.Generic;
using UnityEngine;

/* Script Made By Daniel */
public class Painter : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject decal;
    [SerializeField] private Transform rayTransform;

    private List<GameObject> contactObjects = new List<GameObject>();
    private PaintPool pool;
    private Vector3 lastPaintPos;
    private float distance = 0.06f;
    private float minDistance = 0.0003f;
    #endregion

    private void Start()
    {
        pool = GetComponent<PaintPool>();
    }

    private void Update()
    {
        Vector3 forward = rayTransform.TransformDirection(Vector3.forward);
        Debug.DrawRay(rayTransform.position, forward * distance, Color.green);
        if (contactObjects.Count > 0) Paint(forward);
        //if (Input.GetKey(KeyCode.Mouse0)) DebugPaint(); /* Remove before release */
    }

    private void Paint(Vector3 forward)
    {
        if (Physics.Raycast(rayTransform.position, forward, out RaycastHit rayHit, distance))
        {
            //Debug.Log(rayHit.collider.gameObject.name);
            //float distanceBetweenPaint = (lastPaintPos - rayHit.point).sqrMagnitude;
            if (!rayHit.collider.isTrigger /*&& distanceBetweenPaint > minDistance*/)
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

    /* Remove before release */
    private void DebugPaint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f))
        {
            if (!hitInfo.collider.isTrigger)
            {
                GameObject paint = pool?.Get(hitInfo.point, Quaternion.FromToRotation(Vector3.up, hitInfo.normal), hitInfo.collider.transform);
                //GameObject paint = Instantiate(decal, hitInfo.point, Quaternion.FromToRotation(Vector3.up, hitInfo.normal));
                if (paint)
                {
                    if (hitInfo.collider.gameObject.CompareTag("Interactable"))
                    {
                        BodyPart bodyPart = hitInfo.collider.GetComponent<BodyPart>();
                        if (bodyPart) bodyPart.SetTreatment(TreatmentType.MakeUp, paint);
                    }
                    lastPaintPos = hitInfo.point;
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