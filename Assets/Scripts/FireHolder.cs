using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHolder : MonoBehaviour
{
    [SerializeField] private MeshRenderer fireMaterial;
    [SerializeField] private Material unlitMaterial;
    [SerializeField] private Material litMaterial;

    public bool isLit;
    private GameObject fireParticleSystem;
    private float burningTime = 60f;
    private float timeSinceLit;

    private void OnEnable()
    {
        PlayButton.PlayEvent += ResetFires;
    }

    private void OnDisable()
    {
        PlayButton.PlayEvent -= ResetFires;
    }

    private void Awake()
    {
        try
        {
            fireParticleSystem = GetComponentInChildren<ParticleSystem>().gameObject;
            fireParticleSystem.SetActive(false);
        }
        catch (System.Exception)
        {
            Debug.LogWarning("FireHolder in " + transform.root.name + " is missing it's Particle System");
            //throw;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FireHolder")
        {
            FireHolder otherFireHolder = other.GetComponent<FireHolder>();
            if (isLit && !otherFireHolder.isLit)
            {
                otherFireHolder.LightUp();
                LightUp();
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
        fireParticleSystem.SetActive(true);
        fireMaterial.material = litMaterial;
        yield return new WaitForSeconds(burningTime);
        isLit = false;
        fireParticleSystem.SetActive(false);
        fireMaterial.material = unlitMaterial;
        yield return null;
    }

    private void ResetFires()
    {
        StopCoroutine(StartBurning());
        isLit = false;
        fireParticleSystem.SetActive(false);
        fireMaterial.material = unlitMaterial;
    }
}
