using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Script by Christopher Tåqvist */

public class EnemyStealTarget : MonoBehaviour
{
    private Rigidbody rigidBody;

    [SerializeField] private string objectName;
    public bool pickedUpByEnemy = false;

    private ConfigurableJoint joint;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public string getObjectName()
    {
        return objectName;
    }

    public void AddSpringJoint(Rigidbody rigidBody)
    {
        joint = gameObject.AddComponent(typeof(ConfigurableJoint)) as ConfigurableJoint;
        joint.xMotion = ConfigurableJointMotion.Locked;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.zMotion = ConfigurableJointMotion.Locked;
        joint.connectedBody = rigidBody;
    }
}
