using UnityEngine;

public enum HeadType { Red, Green, Blue, [System.ObsoleteAttribute] NumberOfTypes, [System.ObsoleteAttribute] None };

/* Script Created By Petter */
public class Head : BodyPart
{
    private HeadType headType;

    protected override void Awake()
    {
        base.Awake();
        SetColor();
    }

    public override Interactable Interact()
    {
        base.Interact();
        if (ConnectedBodyPart)
        {
            Collider[] bodyColliders = ConnectedBodyPart.GetComponentsInChildren<Collider>();
            if (collisionManager && collisionManager.GetCollisionTest())
            {
                collisionManager.SetColliderState(bodyColliders, true);
            }
        }
        return this;
    }

    public override void Drop()
    {
        if (ConnectedBodyPart && !ConnectedBodyPart.ActiveHand && collisionManager && collisionManager.GetCollisionTest())
        {
            Collider[] bodyColliders = ConnectedBodyPart.GetComponentsInChildren<Collider>();
            collisionManager.SetColliderState(bodyColliders, false);
            collisionManager.SetColliderState(colliders, false);
        }
        else if (!ConnectedBodyPart)
        {
            collisionManager.SetColliderState(colliders, false);
        }
        

        ActiveHand = null;
    }

    private void SetColor()
    {
        MeshRenderer[] meshRenderer = GetComponentsInChildren<MeshRenderer>();

        bool availableTasks = TaskManager.GetInstance().tasks != null && TaskManager.GetInstance().tasks.Count > 0;
        int randomChanceForMatch = RandomManager.GetRandomNumber(0, 101);
        if (availableTasks && randomChanceForMatch < PrototypeManager.GetInstance().GetCurrentWave().chanceOfCorrectBodyCombination)
        {
            HeadType taskHeadType = TaskManager.GetInstance().tasks[RandomManager.GetRandomNumber(0, TaskManager.GetInstance().tasks.Count)].Head;
            headType = taskHeadType;
        }
        else
        {
            headType = (HeadType)Random.Range(0, 3);
        }

        for (int i = 0; i < meshRenderer.Length; i++)
        {
            if (headType == HeadType.Blue)
            {
                meshRenderer[i].material.color = Color.blue;
            }
            else if (headType == HeadType.Green)
            {
                meshRenderer[i].material.color = Color.green;
            }
            else if (headType == HeadType.Red)
            {
                meshRenderer[i].material.color = Color.red;
            }
        }
    }

    public HeadType GetHeadType() { return headType; }
}