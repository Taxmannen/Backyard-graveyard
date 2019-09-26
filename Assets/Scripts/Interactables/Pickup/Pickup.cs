using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

public enum CollisionTest { None, Drop, Collider }

public enum PickupType { Other, Weapon, Ornament, Head, Body, TaskCard, None }

/* Script Made By Daniel */
[RequireComponent(typeof(Rigidbody))]
public class Pickup : Interactable
{
    #region Variables
    [Header("Pickup")]
    [SerializeField] private PickupType pickupType;
    [SerializeField] private bool snapOnPickup;
    [SerializeField] protected bool shouldDespawnWhenOnGround;
    [SerializeField] protected float despawnTimeWhenOnGround;

    [Header("Particles")]
    [SerializeField] private GameObject destoryParticle;
    [SerializeField] private Vector3 particleOffset;

    [Header("Sound FXs")]
    
    [SerializeField] private PlaySound soundFxsPutDown;

    protected CollisionManager collisionManager;
    protected AudioManager audioManager;
    protected Collider[] colliders;
    protected AudioClip despawnClip;
    private Coroutine coroutine;
    protected bool isQuitting = false;
    #endregion

    protected virtual void Awake()
    {
        collisionManager = CollisionManager.GetInstance();
        audioManager = AudioManager.GetInstance();
        colliders = GetComponentsInChildren<Collider>();
        despawnClip = Resources.Load<AudioClip>("Audio/ObjectDespawnSound01");
    }

    public bool SnapOnPickup
    {
        get { return snapOnPickup; }
        set { snapOnPickup = value; }
    }

    public override Interactable Interact()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        if (collisionManager && collisionManager.GetCollisionTest()) collisionManager.SetColliderState(colliders, true);

        PlayInteractSound();

        return this;
    }

    public virtual void Drop()
    {
        if (collisionManager && collisionManager.GetCollisionTest()) collisionManager.SetColliderState(colliders, false);

        if(soundFxsPutDown != null)
        {
            soundFxsPutDown.Play();
        }
        

        ActiveHand = null;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground" && ActiveHand == null && shouldDespawnWhenOnGround)
        {
            if (coroutine == null) coroutine = StartCoroutine(DestoryMe());
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (ActiveHand == null)
        {
            if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Static"))
            {
                //Made By Petter
                Rigidbody rb = this.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                rb.isKinematic = false;
                /*if (rb.velocity.sqrMagnitude >= Vector3.zero.sqrMagnitude)
                {
                    rb.AddForce(-rb.velocity);
                    rb.velocity = Vector3.zero;
                    Debug.Log(rb.velocity);
                }*/
            }
        }
    }

    public void ExecuteParticle()
    {
        if (destoryParticle != null) Instantiate(destoryParticle, transform.position + particleOffset, Quaternion.identity);
    }

    private void OnDestroy()
    {
        Debug.Log("On Destroy");
        if (isQuitting) return;
        IsBeingDestroyed = true;
        if (ActiveHand != null) ActiveHand.Drop();
        ExecuteParticle();
        if (audioManager)
        {
            audioManager.PlaySoundAtPosition(despawnClip, transform);
        }
    }

    private IEnumerator DestoryMe()
    {
        yield return new WaitForSeconds(despawnTimeWhenOnGround);
        Destroy(gameObject);
    }

    public PickupType GetPickupType() { return pickupType; }

    private void OnApplicationQuit()
    {
        Debug.Log("On Application QUIT");
        isQuitting = true;
    }
}