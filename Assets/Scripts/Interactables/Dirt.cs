using UnityEngine;

/* Script Made By Daniel */
public class Dirt : Pickup
{
    private float timer; //Fixa

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.GetComponent<Grave>() != null && timer > 0.5f)
        {
            other.gameObject.GetComponent<Grave>().AddDirt();
            Destroy(gameObject);
        }
    }
}