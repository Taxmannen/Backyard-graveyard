using System.Collections.Generic;
using UnityEngine;

/* Script Made By Daniel */
public class BodyPart : Pickup
{
    #region Variables
    [Header("Treatment")]
    [SerializeField, ReadOnly] private TreatmentType treatmentType = TreatmentType.None;
    [SerializeField] private Transform[] paintParents;
    [SerializeField] private ParticleSystem washParticleSystem;

    private List<Material[]> originalMaterials = new List<Material[]>();
    private List<Material[]> mummyMaterials = new List<Material[]>();
   
    public BodyPart ConnectedBodyPart { get; set; }
    #endregion

    protected override void Start()
    {
        base.Start();
        MaterialSetup();
    }

    public void SetTreatment(TreatmentType treatmentType, GameObject paint = null)
    {
        switch (treatmentType)
        {
            case TreatmentType.MakeUp:
                MakeUp(true);
                break;
            case TreatmentType.WashUp:
                WashUp(true);
                ConnectedBodyPart?.WashUp(true);
                break;
            case TreatmentType.Mummify:
                Mummify(true);
                ConnectedBodyPart?.Mummify(true);
                break;
            case TreatmentType.None:
                ClearAllTreatments();
                ConnectedBodyPart?.ClearAllTreatments();
                break;
            default:
                Debug.LogError("Something Went Wrong");
                break;
        }
    }
    
    private void ClearPreviousTreatments(TreatmentType treatmentType)
    {
        if (treatmentType != TreatmentType.MakeUp) MakeUp(false);
        if (treatmentType != TreatmentType.WashUp) WashUp(false);
        if (treatmentType != TreatmentType.Mummify) Mummify(false);
    }

    private void ClearAllTreatments()
    {
        MakeUp(false);
        WashUp(false);
        Mummify(false);
        treatmentType = TreatmentType.None;
    }

    private void MakeUp(bool paintState)
    {
        if (paintState)
        {
            if (treatmentType != TreatmentType.MakeUp)
            {
                treatmentType = TreatmentType.MakeUp;
                ClearPreviousTreatments(treatmentType);
            }
        }
        else
        {
            ObjectPool pool = PaintPool.GetInstance();
            List<GameObject> paintList = GetAllDecals();
            foreach (GameObject paint in paintList) pool.ReturnToPool(paint);
        }
    }

    private void WashUp(bool washUpState)
    {
        if (washUpState)
        {
            if (treatmentType != TreatmentType.WashUp)
            {
                treatmentType = TreatmentType.WashUp;
                ClearPreviousTreatments(treatmentType);
                washParticleSystem.Play();
            }
        }
        else washParticleSystem.Stop();
    }

    private void Mummify(bool mummifyState)
    {
        if (mummifyState)
        {
            if (treatmentType != TreatmentType.Mummify)
            {
                treatmentType = TreatmentType.Mummify;
                ClearPreviousTreatments(treatmentType);
            }
        }

        for (int i = 0; i < meshRenderers.Length; i++)
        {
            if (mummifyState) standardMaterials[i] = mummyMaterials[i];
            else              standardMaterials[i] = originalMaterials[i];
            meshRenderers[i].materials = standardMaterials[i];
        }
    }

    private void MaterialSetup()
    {
        Material mummyMaterial = Resources.Load<Material>("Materials/Mummy Material");
        foreach (MeshRenderer renderer in meshRenderers)
        {
            Material[] mummy = new Material[renderer.materials.Length];
            for (int i = 0; i < mummy.Length; i++) mummy[i] = mummyMaterial;
            originalMaterials.Add(renderer.materials);
            mummyMaterials.Add(mummy);
        }
    }

    private List<GameObject> GetAllDecals()
    {
        List<GameObject> decalList = new List<GameObject>();
        foreach (Transform paintParent in paintParents)
        {
            for (int i = 0; i < paintParent.childCount; i++)
            {
                if (paintParent.GetChild(i).CompareTag("Decal")) decalList.Add(paintParent.GetChild(i).gameObject);
            }
        }
        return decalList;
    }

    public TreatmentType GetTreatmentType() { return treatmentType; }
}