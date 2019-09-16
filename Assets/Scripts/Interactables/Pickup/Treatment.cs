using UnityEngine;

public enum TreatmentType { MakeUp, WashUp, Mummify, None, [System.ObsoleteAttribute] NumberOfTypes }

/* Script Made By Daniel */
public class Treatment : Pickup
{
    [Header("Treathment")]
    [SerializeField] private TreatmentType treatmentType;

    public TreatmentType GetTreatmentType() { return treatmentType; }

    private void OnTriggerEnter(Collider other)
    {
        if (treatmentType == TreatmentType.MakeUp || treatmentType == TreatmentType.None) return;
        if (other.CompareTag("Interactable"))
        {
            BodyPart bodyPart = other.GetComponent<BodyPart>();
            if (bodyPart && bodyPart.ActiveHand) bodyPart.SetTreatment(treatmentType);
        }
    }
}