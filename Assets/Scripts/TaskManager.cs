using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <author>Simon</author>
/// </summary>

public class TaskManager : MonoBehaviour
{
    public Task[] tasks;
    public GameObject levelCompletedImage;
    Dictionary<Task, bool> completedTasks = new Dictionary<Task, bool>();
    Dictionary<Task, bool> failedTasks = new Dictionary<Task, bool>();

    private void Start() {
        foreach(Task task in tasks) {
            completedTasks[task] = false;
            failedTasks[task] = false;
            task.TaskManager = this;
        }
    }

    public void CompleteTask(Task task) {
        completedTasks[task] = true;
        CheckLevelCompletion();
    }

    public void FailTask(Task task) {
        failedTasks[task] = true;
        CheckLevelCompletion();
    }

    public bool CheckLevelCompletion() {
        int nrOfCompletedTasks = 0;
        int nrOfFailedTasks = 0;

        foreach (Task task in tasks) {
            if (!completedTasks[task]) {
                if (!failedTasks[task]) {
                    return false;
                }
                else {
                    nrOfFailedTasks++;
                }
            }
            else {
                nrOfCompletedTasks++;
            }
        }

        Debug.Log("You completed " + nrOfCompletedTasks + " tasks and failed " + nrOfFailedTasks + " tasks");

        CompleteLevel();
        return true;
    }

    private void CompleteLevel() {
        foreach (Task task in tasks) {
            task.TaskCard.Disable();
        }

        levelCompletedImage.SetActive(true);
    }
}
