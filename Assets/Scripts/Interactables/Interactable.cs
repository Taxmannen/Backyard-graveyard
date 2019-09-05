using System.Collections.Generic;
using UnityEngine;

public enum MaterialType { Standard, Ghost, Outline }

/* Script Made By Daniel and Petter */
public class Interactable : MonoBehaviour
{
    [Header("Highlight")]
    [SerializeField] private MeshRenderer[] meshRenderers;

    private Material outlineMaterial;
    private Material ghostMaterial;
    private List<Material[]> materials = new List<Material[]>();

    public Hand ActiveHand { get; set; } = null;

    protected virtual void Start()
    {
        outlineMaterial = Resources.Load<Material>("Materials/Outline Material");
        ghostMaterial   = Resources.Load<Material>("Materials/Ghost Material");
        for (int i = 0; i < meshRenderers.Length; i++) materials.Add(meshRenderers[i].materials);
    }

    public void SetToOutlineMaterial(MaterialType matType)
    {
        if (meshRenderers.Length > 0)
        {
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                MeshRenderer meshRender = meshRenderers[i];
                if (meshRender)
                {
                    if (matType == MaterialType.Standard) meshRender.materials = materials[i];
                    else
                    {
                        Material[] newMaterial = new Material[meshRender.materials.Length];
                        for (int j = 0; j < meshRender.materials.Length; j++)
                        {
                            if (matType == MaterialType.Outline) newMaterial[j] = outlineMaterial;
                            else                                 newMaterial[j] = ghostMaterial;
                        }
                        meshRender.materials = newMaterial;
                    }
                }
            }
        }
    }

    public virtual Interactable Interact() { return this; }
}