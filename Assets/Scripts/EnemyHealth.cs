using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int fullHealth;

    private int currentHealth;

    private void Start()
    {
        currentHealth = fullHealth;
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0) Destroy(gameObject);
    }
}
