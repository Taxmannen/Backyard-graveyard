using UnityEngine;

/* Script Made By Daniel */
public class Dirt : Interactable
{
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.GetComponent<Grave>() != null && timer > 1)
        {
            other.gameObject.GetComponent<Grave>().AddDirt();
            Destroy(gameObject);
        }
    }
}