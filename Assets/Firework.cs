using System.Collections;
using UnityEngine;

public class Firework : MonoBehaviour
{
    [SerializeField] private Vector3 startPos;
    [SerializeField] private float stopTime = 5;

    private ParticleSystem ps;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        transform.position = startPos;
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        yield return new WaitForSecondsRealtime(stopTime);
        ps.Stop();
        Destroy(gameObject, 2);
    }
}
