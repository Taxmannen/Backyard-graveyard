﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThiefPickupHand : MonoBehaviour
{
    private ConfigurableJoint joint;
    [SerializeField] private ObjectThiefObjectSearcher objectSearcher;
    [SerializeField] private NewObjectThief newObjectThief;
    private Ornament ornament;

    public void AddJoint(Rigidbody rigidBody)
    {
        if(objectSearcher.GetTargetType() == PickupType.Ornament)
        {
            DetachOrnamentFromPlacement();
        }

        joint = gameObject.AddComponent(typeof(ConfigurableJoint)) as ConfigurableJoint;
        joint.xMotion = ConfigurableJointMotion.Locked;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.zMotion = ConfigurableJointMotion.Locked;
        joint.connectedBody = rigidBody;
    }

    private void DetachOrnamentFromPlacement()
    {
        ornament = newObjectThief.currentTargetObject.GetComponent<Ornament>();

        if(ornament.Placement)
        {
            //ornament.ThiefPickup();
        }
    }

    public void DestroyJoint()
    {

        Destroy(joint);
    }
}
