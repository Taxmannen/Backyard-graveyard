using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int fullHealth;
    [SerializeField] private GameObject[] stringGameObjects;
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
            objectThief.GoToDeathState();
            for (int i = 0; i < stringGameObjects.Length; i++)
            {
                Destroy(stringGameObjects[i]);
            }
            
            UnrestManager.GetInstance().UpdateUnrest(+1);
            
        } /*Destroy(gameObject);*/
    }
}
