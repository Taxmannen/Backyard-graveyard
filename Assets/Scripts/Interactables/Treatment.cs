using UnityEngine;

public enum TreatmentType { MakeUp, Mummify, Wash, [System.ObsoleteAttribute] NumberOfTypes }

/* Script Made By  */
public class Treatment : Pickup
{
    [SerializeField] private TreatmentType treatmentType;

    public TreatmentType GetTreatmentType() { return treatmentType; }
}