using System.Collections.Generic;
using UnityEngine;

/* Script Made By Daniel */
public class Painter : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    [SerializeField] private Transform rayTransform;

    [SerializeField] private List<GameObject> contactObjects = new List<GameObject>();

    private PaintPool pool;
    private float distance = 0.06f;
    //private Vector3 lastPaintPos; //Kanske

    private void Start()
    {
        pool = GetComponent<PaintPool>();
    }

    private void Update()
    {
        Vector3 fwd = rayTransform.TransformDirection(Vector3.forward);
        Debug.DrawRay(rayTransform.position, fwd * distance, Color.green);
        if (contactObjects.Count > 0) Paint(fwd);
    }

    private void Paint(Vector3 fwd)
    {
        if (Physics.Raycast(rayTransform.position, fwd, out RaycastHit rayHit, distance))
        {
            //Debug.Log(rayHit.collider.gameObject.name);
            //float distance = (lastPaintPos - rayHit.point).sqrMagnitude;
            if (!rayHit.collider.isTrigger /*&& distance > 1*/)
            {
                GameObject paint = pool?.Get();
                if (paint)
                {
                   
                    paint.SetActive(true);
                    paint.transform.position = rayHit.point;
                    paint.transform.rotation = Quaternion.FromToRotation(Vector3.up, rayHit.normal);
                    paint.transform.SetParent(rayHit.collider.transform);
                    //lastPaintPos = rayHit.point;
                }
                //GameObject paint = Instantiate(effect, rayHit.point, Quaternion.FromToRotation(Vector3.up, rayHit.normal));
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