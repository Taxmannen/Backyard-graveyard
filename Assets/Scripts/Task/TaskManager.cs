using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <author>Simon</author>
/// </summary>

[System.Serializable]
public class EndOfGameStrings {
    [TextArea(1, 2)] public string s = "You are worth x completed tasks..";

    public override string ToString() {
        return s;
    }
}

public class TaskManager : Singleton<TaskManager>
{
    [Header("References")]
    public Task[] tasks;
    [SerializeField] private EndOfGameStrings[] endOfGameStrings = new EndOfGameStrings[6];

    public GameObject levelCompletedImage;
    public Text levelCompletedText;
    [SerializeField] List<bool> completedTasks = new List<bool>();
    private int tasksInProgress = 0;

    [Header("Settings")]
    [SerializeField] private bool includeTreatments = false;

    [Header("Timer")]
    [SerializeField][Range(0, 600)]
    private int timeLimitInSecondsMin = 5;
    [SerializeField][Range(0, 600)]
    private int timeLimitInSecondsMax = 10;

    private int maxNumberOfTasks;

    public int TimeLimitInSecondsMin { get => timeLimitInSecondsMin; private set => timeLimitInSecondsMin = value; }
    public int TimeLimitInSecondsMax { get => timeLimitInSecondsMax; private set => timeLimitInSecondsMax = value; }
    public bool IncludeTreatments { get => includeTreatments; private set => includeTreatments = value; }
    public int TasksInProgress { get => tasksInProgress; set => tasksInProgress = value; }

    private void Awake() {
        SetInstance(this);
    }

    public void ActivateTasks(float maxTimeInSeconds, int maxNumberOfTasks) {
        this.maxNumberOfTasks = maxNumberOfTasks;

        for (int i = 0; i < maxNumberOfTasks; i++) {
            Task task = GetAvailableTask();

            task.Activate(maxTimeInSeconds);
        }

        return;
    }

    private Task GetAvailableTask() {
        for (int j = 0; j < tasks.Length; j++) {
            if (tasks[j].gameObject.activeSelf == false) {
                return tasks[j];
            }
        }

        // There were no more inactive tasks
        return null;
    }

    public void CompleteTask(Task task, bool success) {
        completedTasks.Add(success);
        TasksInProgress--;
        CheckLevelCompletion();
    }

    public bool CheckLevelCompletion() {
        int nrOfCompletedTasks = GetNrOfSuccessfulTasks();
        int nrOfFailedTasks = GetNrOfFailedTasks();

        if (!CompletedAllTasks()) return false;

        string s = "You completed " + nrOfCompletedTasks + " tasks and failed " + nrOfFailedTasks + " tasks";
        s += "\n" + endOfGameStrings[nrOfCompletedTasks];

        levelCompletedText.text = s;
        Debug.Log(s);

        CompleteLevel();
        return true;
    }

    public int GetNrOfSuccessfulTasks() { return completedTasks.Count(x => x == true); }
    public int GetNrOfFailedTasks() { return completedTasks.Count(x => x == false); }
    public int GetNrOfCompletedTasks() { return GetNrOfSuccessfulTasks() + GetNrOfFailedTasks(); }
    public int TasksRemaniningToComplete() { return maxNumberOfTasks - GetNrOfCompletedTasks(); }
    public int TasksRemaniningToSelect() { return maxNumberOfTasks - (GetNrOfCompletedTasks() + TasksInProgress); }
    public bool CompletedAllTasks() { return TasksRemaniningToComplete() <= 0; }
    public bool TasksAvailableToSelect() { return TasksRemaniningToSelect() > 0; }

    private void CompleteLevel() {
        foreach (Task task in tasks) {
            task.TaskCard.Disable();
        }

        levelCompletedImage.SetActive(true);

        // Disable all InteractableObjects
        DisableAllObjectsOfType.DisableAllObjects<Interactable>();
    }
}
