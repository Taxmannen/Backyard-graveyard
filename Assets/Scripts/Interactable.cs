using UnityEngine;

/* Script Made By Daniel and Petter */
public class Interactable : MonoBehaviour
{
    [Header("Pickup")]
    [SerializeField] private bool snapOnPickup;
    //[SerializeField] private bool snapWhenThrow; //Skall implementeras

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
        /*Material mat = (highlight == true) ? outlineMaterial : standardMaterials[0];
        if (meshRenderer.material != mat) meshRenderer.material = mat;*/

        if (highlight) meshRenderer.materials = outlineMaterials;
        else           meshRenderer.materials = standardMaterials;
    }
}