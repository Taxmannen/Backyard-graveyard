using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThiefPickupHand : MonoBehaviour
{
    private ConfigurableJoint joint;

    public void AddSpringJoint(Rigidbody rigidBody)
    {
        joint = gameObject.AddComponent(typeof(ConfigurableJoint)) as ConfigurableJoint;
        joint.xMotion = ConfigurableJointMotion.Locked;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.zMotion = ConfigurableJointMotion.Locked;
        joint.connectedBody = rigidBody;
    }
}
