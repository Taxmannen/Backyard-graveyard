using UnityEngine;
using Valve.VR;

/* Script Made By Daniel */
public class Weapon : Interactable
{
    [Header("Weapon")]
    [SerializeField] private int damagePerForce;

    private SteamVR_Behaviour_Pose pose = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && ActiveHand != null) 
        {
            if (pose == null) pose = ActiveHand.GetComponent<SteamVR_Behaviour_Pose>();
            float velocity = Mathf.Abs(pose.GetVelocity().x + pose.GetVelocity().y + pose.GetVelocity().z);
            if (velocity < 1) velocity = 1;
            int dmg = (int)velocity * damagePerForce;
            other.GetComponent<EnemyHealth>()?.TakeDamage(dmg);
        }
    }

    public override void Drop()
    {
        base.Drop();
        pose = null;
    }
}