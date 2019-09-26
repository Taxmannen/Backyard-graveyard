using UnityEngine;
using Valve.VR;

/* Script Made By Daniel */
public class Weapon : Pickup
{
    [Header("Weapon")]
    [SerializeField] private int damagePerForce = 3;
    [SerializeField] private ParticleSystem weaponEffect;

    private SteamVR_Behaviour_Pose pose = null;

    private float hardestVelocityYet = 0;
    private float weakestVelocityYet = 1000000000000;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && ActiveHand != null) 
        {
            if (pose == null) pose = ActiveHand.GetComponent<SteamVR_Behaviour_Pose>();
            float velocity = Mathf.Abs(pose.GetVelocity().x + pose.GetVelocity().y + pose.GetVelocity().z);

            //if(hardestVelocityYet < velocity)
            //{
            //    hardestVelocityYet = velocity;
            //    Debug.Log("Hardest Velocity updated to: " + hardestVelocityYet);
            //}

            //if (weakestVelocityYet > velocity)
            //{
            //    weakestVelocityYet = velocity;
            //    Debug.Log("Weakest Velocity updated to: " + weakestVelocityYet);
            //}

            ActiveHand.Vibrate(ActiveHand.vibrationValues.weaponHit, velocity, 6);

            if (velocity < 1) velocity = 1;
            int dmg = (int)velocity * damagePerForce;
            other.GetComponent<EnemyHealth>()?.TakeDamage(dmg);

            
            
           
        }
    }

    public void CreatePoofEffect()
    {
        if (weaponEffect != null) Instantiate(weaponEffect, weaponEffect.transform.position, weaponEffect.transform.rotation).Play();
    }

    public override void Drop()
    {
        base.Drop();
        pose = null;
    }
}