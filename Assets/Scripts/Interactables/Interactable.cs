using System.Collections.Generic;
using UnityEngine;

/* Script Made By Daniel and Petter */
[RequireComponent(typeof(Rigidbody))]
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
                    if (meshRender == null) Debug.Log(gameObject.name + " " + "MeshRenderer är nöll");
                    if (highlight)
                    {
                        Material[] outline = new Material[meshRender.materials.Length];
                        for (int j = 0; j < meshRender.materials.Length; j++)
                        {
                            if (outline[j] == null)
                            {
                                Debug.Log(gameObject.name + " " + "Outline är nöll");
                                outline[j] = outlineMaterial;
                            }
                        }
                        meshRender.materials = outline;
                    }
                    else meshRender.materials = materials[i];
                }
            }
        }
    }

    public virtual Interactable Interact() { return this; }
}