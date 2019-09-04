using System.Collections.Generic;
using UnityEngine;

/* Script Made By Daniel and Petter */
[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
public class Interactable : MonoBehaviour
{
    [Header("Highlight")]
    [SerializeField] private MeshRenderer[] meshRenderers;

    private Material outlineMaterial;
    private List<Material[]> materials = new List<Material[]>();

    public Hand ActiveHand { get; set; } = null;

    private void Start()
    {
        outlineMaterial = Resources.Load<Material>("Materials/Outline Material");
        for (int i = 0; i < meshRenderers.Length; i++) materials.Add(meshRenderers[i].materials);

        /*
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null) meshRenderer = GetComponentInChildren<MeshRenderer>();

        //Krävs för att ändra alla material
        standardMaterials = meshRenderer.materials;
        outlineMaterials = new Material[meshRenderer.materials.Length];
        for (int i = 0; i < meshRenderer.materials.Length; i++) outlineMaterials[i] = outlineMaterial;
        */
    }

    public void SetToOutlineMaterial(bool highlight)
    {
        if (meshRenderers.Length > 0)
        {
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                MeshRenderer meshRender = meshRenderers[i];
                if (meshRender)
                {
                    if (highlight)
                    {
                        Material[] outline = new Material[meshRender.materials.Length];
                        for (int j = 0; j < meshRender.materials.Length; j++) outline[j] = outlineMaterial;
                        meshRender.materials = outline;
                    }
                    else meshRender.materials = materials[i];
                }
            }

            /*if (highlight) meshRenderer.materials = outlineMaterials;
            else           meshRenderer.materials = standardMaterials;*/
        }
    }

    public virtual Interactable Interact()
    {
        return this;
    }
}