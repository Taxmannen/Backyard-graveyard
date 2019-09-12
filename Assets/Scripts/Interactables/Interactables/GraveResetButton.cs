using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveResetButton : Interactable
{

    private Grave grave;
    private float cooldownTime = 3f;
    private bool onCooldown;

    private void Awake()
    {
        grave = GetComponentInParent<Grave>();
    }

    public override Interactable Interact()
    {
        if (!onCooldown)
        {
            StartCoroutine(Cooldown());
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.05f, gameObject.transform.position.z);
            grave.ResetGrave();
        }
        return this;
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        onCooldown = false;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.05f, gameObject.transform.position.z);
        yield return null;
    }
}
