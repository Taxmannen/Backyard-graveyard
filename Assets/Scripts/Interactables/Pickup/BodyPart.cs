using System.Collections.Generic;
using UnityEngine;

/* Script Made By Daniel */
public class BodyPart : Pickup
{
    private List<Material[]> originalMaterials = new List<Material[]>();
    private List<Material[]> mummyMaterials = new List<Material[]>();
    private List<GameObject> paintList = new List<GameObject>();

    public TreatmentType TreatmentType { get; set; } = TreatmentType.None;

    public BodyPart ConnectedBodyPart { get; set; }

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
                MakeUp(true, paint);
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
                ClearTreatment();
                ConnectedBodyPart?.ClearTreatment();
                break;
            default:
                Debug.LogError("Something Went Wrong");
                break;
        }
    }

    private void ClearTreatment()
    {
        MakeUp(false);
        WashUp(false);
        Mummify(false);
        TreatmentType = TreatmentType.None;
    }


    private void MakeUp(bool paintState, GameObject paintObject = null)
    {
        if (paintState)
        {
            if (TreatmentType != TreatmentType.MakeUp) TreatmentType = TreatmentType.MakeUp;
            paintList.Add(paintObject);
        }
        else
        {
            foreach (GameObject paint in paintList) Destroy(paint);
        }
    }

    private void WashUp(bool washUpState)
    {
        //Debug.Log(washUpState);
        if (washUpState && TreatmentType != TreatmentType.WashUp) TreatmentType = TreatmentType.WashUp;
    }

    private void Mummify(bool mummifyState)
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            if (mummifyState) standardMaterials[i] = mummyMaterials[i];
            else standardMaterials[i] = originalMaterials[i];

            meshRenderers[i].materials = standardMaterials[i];
        }
        if (mummifyState && TreatmentType != TreatmentType.Mummify) TreatmentType = TreatmentType.Mummify;
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
}