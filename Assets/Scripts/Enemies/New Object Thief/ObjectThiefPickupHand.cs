using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThiefPickupHand : MonoBehaviour
{
    [SerializeField] private ObjectThiefObjectSearcher objectSearcher;
    [SerializeField] private NewObjectThief newObjectThief;
    private Ornament ornament;
    private ConfigurableJoint joint;

    public void PickupObject(Rigidbody objectRigidBody)
    {
        if(objectSearcher.GetTargetType() == PickupType.Ornament)
        {
            SetOrnamentAsHeldByThiefAndDetachFromPlacementIfPlaced();
        }

        AddJoint(objectRigidBody);
    }

    private void SetOrnamentAsHeldByThiefAndDetachFromPlacementIfPlaced()
    {
        ornament = newObjectThief.currentTargetObject.GetComponent<Ornament>();
        ornament.ThiefIsHolding = true;
        DetachOrnamentFromPlacement();
    }

    public void DetachOrnamentFromPlacement()
    {
        if(ornament.Placement)
        {
            //ornament.PickupOrnamentFromPlacement();
            ornament.PickupObjectFromPlacement();
        }
    }

    private void AddJoint(Rigidbody objectRigidBody)
    {
        joint = gameObject.AddComponent(typeof(ConfigurableJoint)) as ConfigurableJoint;
        joint.xMotion = ConfigurableJointMotion.Locked;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.zMotion = ConfigurableJointMotion.Locked;
        joint.connectedBody = objectRigidBody;
    }

    public void DestroyJoint()
    {
        if(ornament != null)
        {
            ornament.ThiefIsHolding = false;
        }
        Destroy(joint);
    }
}
