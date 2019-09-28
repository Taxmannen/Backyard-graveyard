using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMarionette : MonoBehaviour
{
    [SerializeField] private SpringJoint headJoint;
    [SerializeField] private SpringJoint marionetteJoint;
    [SerializeField] private Rigidbody headRigidBody;
    [SerializeField] private Rigidbody marionetteRigidBody;

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Z))
        //{
        //    AddHeadJoint();
        //    AddMarionetteJoint();
        //}

        //if(Input.GetKeyDown(KeyCode.U))
        //{
        //    DestroyJoints();
        //}
    }

    private void OnEnable()
    {
        AddHeadJoint();
        AddMarionetteJoint();

    }

    private void OnDisable()
    {
        DestroyJoints();
    }

    private SpringJoint AddJoint(Rigidbody objectRigidBody, Vector3 connectedAnchor, SpringJoint joint)
    {
        joint = gameObject.AddComponent(typeof(SpringJoint)) as SpringJoint;
        joint.connectedAnchor.Set(connectedAnchor.x, connectedAnchor.y, connectedAnchor.z);
        joint.spring = 250;
        joint.enablePreprocessing = true;
        joint.connectedBody = objectRigidBody;
        return joint;
    }

    private void AddHeadJoint()
    {
        headJoint = gameObject.AddComponent(typeof(SpringJoint)) as SpringJoint;
        headJoint.connectedAnchor.Set(-0.005345016f, -2.980232e-07f, 1.963171f);
        headJoint.spring = 250;
        headJoint.enablePreprocessing = true;
        headJoint.connectedBody = headRigidBody;
    }

    private void AddMarionetteJoint()
    {
        marionetteJoint = gameObject.AddComponent(typeof(SpringJoint)) as SpringJoint;
        marionetteJoint.connectedAnchor.Set(0,0,0);
        marionetteJoint.spring = 250;
        marionetteJoint.enablePreprocessing = true;
        marionetteJoint.connectedBody = marionetteRigidBody;
    }

    public void DestroyJoints()
    {
        Destroy(headJoint);
        Destroy(marionetteJoint);
    }
}
