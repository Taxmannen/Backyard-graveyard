using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/* Script made by Petter */
public class TaskDoneBox : MonoBehaviour
{

    [SerializeField] Text taskText;

    private List<TaskCard> completedCards = new List<TaskCard>();

    int totalTasksForLevel;
    int numberOfTasksCompleted;

    
    // Start is called before the first frame update
    void Start()
    {
        totalTasksForLevel = PrototypeManager.GetInstance().TotalNrOfTasks;
        numberOfTasksCompleted = 0;
        UpdateCompletedTasksText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TaskCard>().taskCompleted == true && numberOfTasksCompleted != totalTasksForLevel && !completedCards.Contains(other.GetComponent<TaskCard>()))
        {
            completedCards.Add(other.GetComponent<TaskCard>());
            numberOfTasksCompleted++;
            UpdateCompletedTasksText();
        }
    }

    private void UpdateCompletedTasksText()
    {
        taskText.text = "Tasks Completed:\n" + numberOfTasksCompleted + "/" + totalTasksForLevel;
    }
}
