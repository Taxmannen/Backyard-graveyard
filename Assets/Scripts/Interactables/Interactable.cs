using System.Collections.Generic;
using UnityEngine;

public enum MaterialType { Standard, Ghost, Outline }

/* Script Made By Daniel, Edited By Petter */
public class Interactable : MonoBehaviour
{
    [Header("Highlight")]
    [SerializeField] protected MeshRenderer[] meshRenderers;

    private Material outlineMaterial;
    private Material ghostMaterial;
    private List<Material[]> materials = new List<Material[]>();
    private MaterialType materialType = MaterialType.Standard;

    public Hand ActiveHand { get; set; } = null;

    protected virtual void Start()
    {
        outlineMaterial = Resources.Load<Material>("Materials/Outline Material");
        ghostMaterial   = Resources.Load<Material>("Materials/Ghost Material");
        for (int i = 0; i < meshRenderers.Length; i++) materials.Add(meshRenderers[i].materials);
        if (meshRenderers.Length == 0) Debug.LogError(gameObject.name + " " + "MESH RENDERER ÄR NULL!");
    }

    public void SetToOutlineMaterial(MaterialType matType)
    {
        if (materialType == matType) return;
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
            materialType = matType;
        }
    }

    public GameObject CreateGhostObject(Vector3 position, Vector3 rotation)
    {
        GameObject ghost = Instantiate(gameObject);
        Interactable ghostScript = ghost.GetComponent<Interactable>();
        ghostScript.Start();
        ghostScript.SetToOutlineMaterial(MaterialType.Ghost);

        Collider[] cols = ghost.GetComponents<Collider>();
        foreach (var collider in cols) Destroy(collider);

        MonoBehaviour[] monoBehaviours = ghost.GetComponents<MonoBehaviour>();
        foreach (var script in monoBehaviours) Destroy(script);

        Destroy(ghost.GetComponent<Rigidbody>());

        ghost.tag = "Untagged";
        ghost.transform.position = position;
        ghost.transform.rotation = Quaternion.Euler(rotation);
        ghost.gameObject.SetActive(false);

        ghost.SetActive(true);

        return ghost;
    }
 
    public virtual Interactable Interact() { return this; }
}