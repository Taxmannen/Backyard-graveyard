using UnityEngine;

public enum TreatmentType { MakeUp, WashUp, Mummify, [System.ObsoleteAttribute] NumberOfTypes, None }

/* Script Made By Daniel */
public class Treatment : Pickup
{
    [Header("Treathment")]
    [SerializeField] private TreatmentType treatmentType = TreatmentType.None;

    public TreatmentType GetTreatmentType() { return treatmentType; }

    private void OnTriggerEnter(Collider other)
    {
        if (treatmentType == TreatmentType.MakeUp || treatmentType == TreatmentType.None) return;
        if (other.CompareTag("Interactable"))
        {
            BodyPart bodyPart = other.GetComponent<BodyPart>();
            if (bodyPart && ActiveHand) bodyPart.SetTreatment(treatmentType);
        }
    }
}