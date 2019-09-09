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
    public Text levelCompletedText;
    Dictionary<Task, bool> completedTasks = new Dictionary<Task, bool>();
    Dictionary<Task, bool> failedTasks = new Dictionary<Task, bool>();

    [SerializeField][Range(0, 600)]
    private int timeLimitInSecondsMin = 5;
    [SerializeField][Range(0, 600)]
    private int timeLimitInSecondsMax = 10;

    public int TimeLimitInSecondsMin { get => timeLimitInSecondsMin; private set => timeLimitInSecondsMin = value; }
    public int TimeLimitInSecondsMax { get => timeLimitInSecondsMax; private set => timeLimitInSecondsMax = value; }

    private void Start() {
        foreach(Task task in tasks) {
            completedTasks[task] = false;
            failedTasks[task] = false;
            task.TaskManager = this;

            task.Initialise();
            task.RefreshTaskCardIngredients();
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

        string s = "You completed " + nrOfCompletedTasks + " tasks and failed " + nrOfFailedTasks + " tasks";
        switch (nrOfCompletedTasks) {
            case 0:
                s += "\nYou might have a very minor case of serious brain injury.";
                break;
            case 1:
                s += "\nYou're fired!";
                break;
            case 2:
                s += "\nYour success is a lie.";
                break;
            case 3:
                s += "\n3.6 completed tasks, not great, not terrible.";
                break;
            case 4:
                s += "\nDon't get cocky.";
                break;
            case 5:
                s += "\nI'm proud of you... son.";
                break;
        }
        levelCompletedText.text = s;
        Debug.Log(s);

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
