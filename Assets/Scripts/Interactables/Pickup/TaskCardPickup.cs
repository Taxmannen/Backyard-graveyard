using UnityEngine;

public class TaskCardPickup : PlaceablePickup
{
    [SerializeField] private GameObject taskCompleteParticle;

    private Vector3 localScale;

    protected override void Awake()
    {
        base.Awake();
        localScale = transform.localScale;
    }

    public void ScaleTaskCard(bool toBig)
    {
        if (toBig)
        {
            transform.localScale = localScale * 2;
            taskCompleteParticle.transform.localScale = localScale * 4;
        }
        else
        {
            transform.localScale = localScale;
            taskCompleteParticle.transform.localScale = localScale * 2;
        }
    }

    public void SetTaskCompletion(bool state)
    {
        //taskCompleteParticle.SetActive(state);
    }
}