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

    private void Start() {
        foreach(Task task in tasks) {
            completedTasks[task] = false;
            task.TaskManager = this;
        }
    }

    public void CompleteTask(Task task) {
        completedTasks[task] = true;
        CheckLevelCompletion();
    }

    public bool CheckLevelCompletion() {
        foreach (Task task in tasks) {
            if (!completedTasks[task])
                return false;
        }

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
