using System.Collections.Generic;
using UnityEngine;

public enum MaterialType { Standard, Ghost, Outline }

/* Script Made By Daniel, Edited By Petter */
public class Interactable : MonoBehaviour
{
    #region Variables
    [Header("Highlight")]
    [SerializeField] protected MeshRenderer[] meshRenderers;
    [SerializeField] private GameObject ghostPrefab;

    protected List<Material[]> standardMaterials = new List<Material[]>();
    private List<Material[]> outlineMaterials = new List<Material[]>();
    private List<Material[]> ghostMaterials = new List<Material[]>();

    private MaterialType materialType = MaterialType.Standard;

    public Hand ActiveHand { get; set; } = null;
    #endregion

    protected virtual void Start()
    {
        if (meshRenderers.Length == 0) Debug.LogError(gameObject.name + " " + "MESH RENDERER ÄR NULL!");
        MaterialSetup();
    }

    public void SetToOutlineMaterial(MaterialType materialType)
    {
        if (this.materialType == materialType) return;
        if (meshRenderers.Length > 0)
        {
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                if (meshRenderers[i] != null)
                {
                    switch (materialType)
                    {
                        case MaterialType.Standard:
                            meshRenderers[i].materials = standardMaterials[i];
                            break;
                        case MaterialType.Outline:
                            meshRenderers[i].materials = outlineMaterials[i];
                            break;
                        case MaterialType.Ghost:
                            meshRenderers[i].materials = ghostMaterials[i];
                            break;
                    }
                }
            }
            this.materialType = materialType;
        }
    }

    private void MaterialSetup()
    {
        //Material outlineMaterial = Resources.Load<Material>("Materials/Outline Material");
        Material outlineMaterial = Resources.Load<Material>("Materials/Outline");
        Material ghostMaterial = Resources.Load<Material>("Materials/Ghost Material");
        foreach (MeshRenderer renderer in meshRenderers)
        {
            Material[] outlines = new Material[renderer.materials.Length];
            Material[] ghosts = new Material[renderer.materials.Length];
            for (int i = 0; i < outlines.Length; i++)
            {
                outlines[i] = outlineMaterial;
                ghosts[i] = ghostMaterial;
            }
            standardMaterials.Add(renderer.materials);
            outlineMaterials.Add(outlines);
            ghostMaterials.Add(ghosts);
        }
    }

    public GameObject CreateGhostObject(Vector3 position, Vector3 rotation)
    {
        if (ghostPrefab) return Instantiate(ghostPrefab, position, Quaternion.Euler(rotation));
        else
        {
            Debug.LogError("No Ghost Prefab", gameObject);
            return null;
        }
    }

    public virtual Interactable Interact() { return this; }
}