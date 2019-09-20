using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int fullHealth;
    
    [SerializeField] private NewObjectThief objectThief;

    private int currentHealth;

    private void Start()
    {
        currentHealth = fullHealth;
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0) {
            objectThief.Die();
        } /*Destroy(gameObject);*/
    }


    //Alla dessa grejer borde göras i ObjectThief
    
}
