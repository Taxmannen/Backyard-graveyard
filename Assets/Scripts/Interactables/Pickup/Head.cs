using UnityEngine;

public enum HeadType { Red, Green, Blue, [System.ObsoleteAttribute] NumberOfTypes, [System.ObsoleteAttribute] None };

/* Script Created By Petter */
public class Head : BodyPart
{
    private HeadType headType;

    private void Awake()
    {
        SetColor();
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