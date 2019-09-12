using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <author>Simon</author>
/// </summary>

[System.Serializable]
public class EndOfGameStrings {
    [TextArea(1, 2)] public string zeroCompletedTask = "You might have a very minor case of serious brain injury.";
    [TextArea(1, 2)] public string oneCompletedTask = "You're fired!";
    [TextArea(1, 2)] public string twoCompletedTasks = "Your success is a lie.";
    [TextArea(1, 2)] public string threeCompletedTasks = "3.6 completed tasks, not great, not terrible.";
    [TextArea(1, 2)] public string fourCompletedTasks = "Don't get cocky.";
    [TextArea(1, 2)] public string fiveCompletedTasks = "I'm proud of you... son.";
}

public class TaskManager : Singleton<TaskManager>
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

    [SerializeField] private EndOfGameStrings endOfGameStrings;

    private void Start() {
        SetInstance(this);

        foreach (Task task in tasks) {
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
                s += "\n" + endOfGameStrings.zeroCompletedTask;
                break;
            case 1:
                s += "\n" + endOfGameStrings.oneCompletedTask;
                break;
            case 2:
                s += "\n" + endOfGameStrings.twoCompletedTasks;
                break;
            case 3:
                s += "\n" + endOfGameStrings.threeCompletedTasks;
                break;
            case 4:
                s += "\n" + endOfGameStrings.fourCompletedTasks;
                break;
            case 5:
                s += "\n" + endOfGameStrings.fiveCompletedTasks;
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

        // Disable all InteractableObjects
        DisableAllObjectsOfType.DisableAllObjects<Interactable>();
    }
}
