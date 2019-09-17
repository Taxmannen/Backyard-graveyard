using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHolder : MonoBehaviour
{
    [SerializeField] private MeshRenderer fireMaterial;
    [SerializeField] private Material white;
    [SerializeField] private Material green;

    public bool isLit;
    private float burningTime = 10f;
    private float timeSinceLit;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision");
        if (other.tag == "FireHolder")
        {
            FireHolder otherFireHolder = other.GetComponent<FireHolder>();
            Debug.Log("right tag");
            if (isLit && !otherFireHolder.isLit)
            {
                Debug.Log("Running coroutine");
                other.GetComponent<FireHolder>().LightUp();
            }
        }
    }

    public void LightUp()
    {
        StartCoroutine(StartBurning());
    }

    private IEnumerator StartBurning()
    {
        isLit = true;
        fireMaterial.material = green;
        yield return new WaitForSeconds(burningTime);
        fireMaterial.material = white;
        isLit = false;
        yield return null;
    }
}
