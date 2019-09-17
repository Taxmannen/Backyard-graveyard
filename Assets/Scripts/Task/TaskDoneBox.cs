using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/* Script made by Petter */
public class TaskDoneBox : MonoBehaviour
{

    [Header("Text Related")]
    [SerializeField] Text taskText;

    [Header("Prefabs")]
    [SerializeField] GameObject taskCardInBox;
    [SerializeField] GameObject newCardParent;

    [Header("Level Related")]
    int totalTasksForLevel;
    int numberOfTasksCompleted;
    public bool levelComplete { get; private set; }

    [Header("Other")]
    private List<GameObject> objectsInBox = new List<GameObject>();
    private Vector3 baseOffset;


    // Start is called before the first frame update
    void Start()
    {
        levelComplete = false;
        baseOffset = new Vector3((0), (0.06f), (- 0.15f));
        totalTasksForLevel = PrototypeManager.GetInstance().NrOfTasks;
        numberOfTasksCompleted = 0;
        UpdateCompletedTasksText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TaskCard>().taskCompleted == true && !levelComplete)
        {
            //Destroy(other.gameObject);
            other.GetComponent<TaskCard>().task.Reinitialise();
            CreateNewTaskCard();
            UpdateCompletedTasksText();
        }
    }

    private void CreateNewTaskCard()
    {
        Vector3 newOffset = new Vector3(baseOffset.x, baseOffset.y + (numberOfTasksCompleted * 0.01f), baseOffset.z);
        GameObject newcard = Instantiate(taskCardInBox, newCardParent.transform);
        objectsInBox.Add(newcard);
        newcard.transform.position = newCardParent.transform.position + (transform.rotation * newOffset);
        numberOfTasksCompleted++;
        if (numberOfTasksCompleted == totalTasksForLevel)
        {
            levelComplete = true;
            PrototypeManager.GetInstance().AdvanceWave();
        }
    }

    private void UpdateCompletedTasksText()
    {
        if (taskText != null)
        {
            taskText.text = "Tasks Completed:\n" + numberOfTasksCompleted + "/" + totalTasksForLevel;
        }
    }

    //If LevelManager needs to emty the box when changing level.
    public void ClearObjectsInBox()
    {
        foreach (var card in objectsInBox)
        {
            Destroy(card);
        }

        if (objectsInBox != null)
        {
            objectsInBox.Clear();
        }

        UpdateCompletedTasksText();
    }
}
