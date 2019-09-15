using UnityEngine;

/*
    What it’s used for: Treatments are parts of Tasks. Every body will require exactly one treatment. (Might want to lower the requirement to zero treatments, but never more).
    How it works
    Once a treatment is applied to a body, all previously performed treatments on that body are cleared. 

    Type of Treatments:
    Make-up - Paint objects and environment with a lipstick. One dot of lipstick on a body part is enough to fulfill the task. “Unity Decal” for applying the make-up?
    Wash-up - Clean the deceased with a sponge. One touch on a body part is enough to fulfill the task. Particle system with sparkles whenever an object is sponged?
    Mummify - Cover the body in mummy-cloth. Can we add a mummy-material to any object that the cloth is applied to?
 */

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
            if (bodyPart) bodyPart.SetTreatment(treatmentType);
        }
    }
}