using System.Collections;
using UnityEngine;

/* Script Made By Daniel */
public class OrnamentPlacement : Placement
{
    [Header("Ornament Placement")]
    [SerializeField] private Grave grave;

    //Petters Variables
    private float timeOnGroundBeforeUnrestIncrease = 15f;
    private Coroutine coroutine;
    private int value = 1;

    private void Start()
    {
        Destroy(GetComponentInChildren<SpriteRenderer>().gameObject);
    }

    public void CheckGraveCompletion()
    {
        grave.CheckTaskCompletion();
    }

    public void ReturnOrnament()
    {
        if (placedObject)
        {
            placedObject.GetComponent<Ornament>().ReturnToPool();
            RemovePlacedObject();
        }
    }

    public Ornament GetPlacedOrnament()
    {
        if (placedObject != null) return placedObject.GetComponent<Ornament>();
        else return null;
    }

    public void SetUnrestIEnumerator(bool state)
    {
        if (coroutine != null && !state)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        else if (coroutine == null && state)
        {
            coroutine = StartCoroutine(UnrestChange());
        }
    }
    
    //Made by Petter
    private IEnumerator UnrestChange()
    {
        while (placedObject != null)
        {
            yield return new WaitForSecondsRealtime(timeOnGroundBeforeUnrestIncrease);
            UnrestManager.GetInstance().UpdateUnrest(value);
        }
    }
}