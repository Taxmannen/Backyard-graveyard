using UnityEngine;

/* Script Made By Daniel */
public class Dirt : Interactable
{
    private float timer;
   // private bool added;

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnCollisionStay(Collision other)
    {
        if (/*!added &&*/ other.gameObject.GetComponent<Grave>() != null && timer > 1)
        {
            Drop();
            other.gameObject.GetComponent<Grave>().AddDirt();
            Destroy(gameObject);
            //added = true;
        }
    }


    //Behövs ej längre??
    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }
}