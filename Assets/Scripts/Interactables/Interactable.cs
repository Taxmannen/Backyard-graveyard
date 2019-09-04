using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
/* Script Made By Daniel and Petter */
public class Interactable : MonoBehaviour
{
    [Header("Pickup")]
    [SerializeField] private bool snapOnPickup;
    //[SerializeField] private bool snapWhenThrow; //Skall implementeras
    [SerializeField] protected bool shouldDespawnWhenOnGround;
    [SerializeField] protected float despawnTimeWhenOnGround;

    private MeshRenderer meshRenderer;
    private Material outlineMaterial;
    private Material[] standardMaterials;
    private Material[] outlineMaterials;

    public Hand ActiveHand { get; set; } = null;

    public bool SnapOnPickup
    {
        get { return snapOnPickup; }
    }

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null) meshRenderer = GetComponentInChildren<MeshRenderer>();

        outlineMaterial = Resources.Load<Material>("Materials/Outline Material");

        //Krävs för att ändra alla material
        standardMaterials = meshRenderer.materials;
        outlineMaterials = new Material[meshRenderer.materials.Length];
        for (int i = 0; i < meshRenderer.materials.Length; i++) outlineMaterials[i] = outlineMaterial;
    }

    public void SetToOutlineMaterial(bool highlight)
    {
        if (meshRenderer)
        {
            if (highlight) meshRenderer.materials = outlineMaterials;
            else           meshRenderer.materials = standardMaterials;
        }
    }

    public virtual Interactable Interact()
    {
        return this;
    }

    public virtual void Drop()
    {
        ActiveHand = null;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground" && ActiveHand == null &&shouldDespawnWhenOnGround)
        {
            Destroy(gameObject, despawnTimeWhenOnGround);
        }
    }

    private void OnDestroy()
    {
        if (ActiveHand != null) ActiveHand.Drop();
    }
}