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

    private Vector3 baseOffset;

    int totalTasksForLevel;
    int numberOfTasksCompleted;
    public bool levelComplete { get; private set; }

    
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
        taskText.text = "Tasks Completed:\n" + numberOfTasksCompleted + "/" + totalTasksForLevel;
    }
}
