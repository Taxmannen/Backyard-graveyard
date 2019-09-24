using UnityEngine;
using System.Collections;

/* Script Made By Daniel */
public class GraveMover : MonoBehaviour
{
    #region Variables
    [SerializeField, Range(0.25f, 5)] private float speed = 2;
    [SerializeField, Range(0.00f, 2)] private float endPosY = 0.5f;
    [SerializeField] private Transform grave;

    private Coroutine coroutine;
    private Vector3 startPos;
    private Vector3 endPos;
    #endregion

    private void Start()
    {
        startPos = grave.position;
        endPos = new Vector3(grave.position.x, grave.position.y + endPosY, grave.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (coroutine != null) Stop();
            coroutine = StartCoroutine(MoveGrave(endPos));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (coroutine != null) Stop();
            coroutine = StartCoroutine(MoveGrave(startPos));
        }
    }

    private IEnumerator MoveGrave(Vector3 targetPos)
    {
        float distance = (targetPos - grave.position).sqrMagnitude;
        while (distance > 0.0001f)
        {
            grave.position = Vector3.MoveTowards(grave.position, targetPos, Time.deltaTime * speed);
            distance = (targetPos - grave.position).sqrMagnitude;
            yield return new WaitForSecondsRealtime(0.001f);
        }
        coroutine = null;
    }

    private void Stop()
    {
        StopCoroutine(coroutine);
        coroutine = null;
    }
}
