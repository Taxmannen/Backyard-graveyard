using System.Collections.Generic;
using UnityEngine;

/* Script Made By Daniel */
public class BodyPart : Pickup
{
    #region Variables
    [Header("Treatment")]
    [SerializeField, ReadOnly] private TreatmentType treatmentType = TreatmentType.None;

    private List<Material[]> originalMaterials = new List<Material[]>();
    private List<Material[]> mummyMaterials = new List<Material[]>();
   
    public BodyPart ConnectedBodyPart { get; set; }
    #endregion

    protected override void Start()
    {
        base.Start();
        MaterialSetup();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) ClearTreatments();
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
                ClearTreatments();
                ConnectedBodyPart?.ClearTreatments();
                break;
            default:
                Debug.LogError("Something Went Wrong");
                break;
        }
    }

    private void ClearTreatments()
    {
        MakeUp(false);
        WashUp(false);
        Mummify(false);
        treatmentType = TreatmentType.None;
    }

    private void MakeUp(bool paintState, GameObject paintObject = null)
    {
        //paintObject.SetChild()
        if (paintState)
        {
            if (treatmentType != TreatmentType.MakeUp) treatmentType = TreatmentType.MakeUp;
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
        //Debug.Log(washUpState);
        if (washUpState && treatmentType != TreatmentType.WashUp) treatmentType = TreatmentType.WashUp;
    }

    private void Mummify(bool mummifyState)
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            if (mummifyState) standardMaterials[i] = mummyMaterials[i];
            else              standardMaterials[i] = originalMaterials[i];
            meshRenderers[i].materials = standardMaterials[i];
        }
        if (mummifyState && treatmentType != TreatmentType.Mummify) treatmentType = TreatmentType.Mummify;
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
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).CompareTag("Decal")) decalList.Add(transform.GetChild(i).gameObject);
        }
        return decalList;
    }

    public TreatmentType GetTreatmentType() { return treatmentType; }
}