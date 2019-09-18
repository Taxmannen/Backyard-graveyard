using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int fullHealth;
    [SerializeField] private GameObject[] stringGameObjects;

    private int currentHealth;

    private void Start()
    {
        currentHealth = fullHealth;
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0) {
            for(int i = 0; i < stringGameObjects.Length; i++)
            {
                Destroy(stringGameObjects[i]);
            }
            UnrestManager.GetInstance().UpdateUnrest(-1);
            
        } /*Destroy(gameObject);*/
    }
}
