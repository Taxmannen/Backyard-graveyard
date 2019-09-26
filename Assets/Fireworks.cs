using System.Collections;
using UnityEngine;

/* Script Made By Daniel */
public class Fireworks : MonoBehaviour
{
    [SerializeField] private Vector3 startPos;
    [SerializeField] private float stopTime = 5;

    private ParticleSystem ps;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        transform.position = startPos;
        StartCoroutine(StopPlayParticle());
    }

    private IEnumerator StopPlayParticle()
    {
        yield return new WaitForSecondsRealtime(stopTime);
        ps.Stop();
        Destroy(gameObject, 10);
    }
}
