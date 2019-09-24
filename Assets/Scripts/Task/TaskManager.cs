using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <author>Simon</author>
/// </summary>

[System.Serializable]
public class EndOfGameStrings
{
    [TextArea(1, 2)] public string s = "You are worth x completed tasks..";

    public override string ToString()
    {
        return s;
    }
}

public class TaskManager : Singleton<TaskManager>
{
    [Header("Settings")]
    [SerializeField] private bool taskManagerSpawnsTasks = false;

    [Header("References")]
    public List<Task> tasks;
    [SerializeField] private EndOfGameStrings[] endOfGameStrings = new EndOfGameStrings[6];
    [SerializeField] public Transform bodySpawnPosition;

    public GameObject levelCompletedImage;
    public Text levelCompletedText;
    [SerializeField] List<bool> completedTasks = new List<bool>();
    private int tasksInProgress = 0;

    [Header("Settings")]
    [SerializeField] private bool includeTreatments = true;

    [Header("Timer")]
    [SerializeField]
    [Range(0, 600)]
    private int timeLimitInSecondsMin = 5;
    [SerializeField]
    [Range(0, 600)]
    private int timeLimitInSecondsMax = 10;

    private int unrestValueChange;

    [Header("Debug")]
    //comment
    [SerializeField, ReadOnly] private float maxTimeInSeconds, chanceOfTreatment;
    [SerializeField, ReadOnly] private int maxNumberOfTasks, minNrOfOrnaments, maxNrOfOrnaments;

    public int TimeLimitInSecondsMin { get => timeLimitInSecondsMin; private set => timeLimitInSecondsMin = value; }
    public int TimeLimitInSecondsMax { get => timeLimitInSecondsMax; private set => timeLimitInSecondsMax = value; }
    public bool IncludeTreatments { get => includeTreatments; private set => includeTreatments = value; }
    public int TasksInProgress { get => tasksInProgress; set => tasksInProgress = value; }
    public bool TaskManagerSpawnsTasks { get => taskManagerSpawnsTasks; private set => taskManagerSpawnsTasks = value; }
    public float MaxTimeInSeconds { get => maxTimeInSeconds; private set => maxTimeInSeconds = value; }
    public float ChanceOfTreatment { get => chanceOfTreatment; private set => chanceOfTreatment = value; }
    public int MaxNumberOfTasks { get => maxNumberOfTasks; private set => maxNumberOfTasks = value; }
    public int MinNrOfOrnaments { get => minNrOfOrnaments; private set => minNrOfOrnaments = value; }
    public int MaxNrOfOrnaments { get => maxNrOfOrnaments; private set => maxNrOfOrnaments = value; }

    private void Awake()
    {
        SetInstance(this);
    }

    public void ActivateTasks(float maxTimeInSeconds, int maxNumberOfTasks, int minNrOfOrnaments, int maxNrOfOrnaments, float chanceOfTreatment)
    {
        Reset();

        this.maxTimeInSeconds = maxTimeInSeconds;
        this.maxNumberOfTasks = maxNumberOfTasks;
        this.minNrOfOrnaments = minNrOfOrnaments;
        this.maxNrOfOrnaments = maxNrOfOrnaments;
        this.chanceOfTreatment = chanceOfTreatment;

        if (!TaskManagerSpawnsTasks) return;

        for (int i = 0; i < maxNumberOfTasks; i++)
        {
            Task task = GetAvailableTask();

            if (task != null && task.gameObject.activeSelf == false)
                task.Activate(maxTimeInSeconds, minNrOfOrnaments, maxNrOfOrnaments, chanceOfTreatment);
        }

        return;
    }

    public void Reset()
    {
        Debug.LogError("RESETTING TASK MANAGER EVERYBODY PICNIC");

        completedTasks.Clear();
        levelCompletedImage.SetActive(false);
        levelCompletedText.text = "";
    }

    private Task GetAvailableTask()
    {
        for (int j = 0; j < tasks.Count; j++)
        {
            if (tasks[j].gameObject.activeSelf == false)
            {
                return tasks[j];
            }
        }

        // There were no more inactive tasks
        return null;
    }

    public void CompleteTask(Task task, bool success)
    {
        completedTasks.Add(success);
        TasksInProgress--;
        //CheckLevelCompletion();
        unrestValueChange = success ? 1 : -1;
        UnrestManager.GetInstance().UpdateUnrest(unrestValueChange);
    }

    public bool CheckLevelCompletion()
    {
        int nrOfCompletedTasks = GetNrOfSuccessfulTasks();
        int nrOfFailedTasks = GetNrOfFailedTasks();

        string s = "You completed " + nrOfCompletedTasks + " tasks and failed " + nrOfFailedTasks + " tasks";
        Debug.Log(s);

        if (!CompletedAllTasks()) return false;

        CompleteLevel();
        return true;
    }

    public int GetNrOfSuccessfulTasks() { return completedTasks.Count(x => x == true); }
    public int GetNrOfFailedTasks() { return completedTasks.Count(x => x == false); }
    public int GetNrOfCompletedTasks() { return GetNrOfSuccessfulTasks() + GetNrOfFailedTasks(); }
    public int TasksRemaniningToComplete() { return MaxNumberOfTasks - GetNrOfCompletedTasks(); }
    public int TasksRemaniningToSelect() { return MaxNumberOfTasks - (GetNrOfCompletedTasks() + TasksInProgress); }
    public bool CompletedAllTasks() { return TasksRemaniningToComplete() <= 0; }
    public bool TasksAvailableToSelect() { return TasksRemaniningToSelect() > 0; }

    private void CompleteLevel()
    {
        //foreach (Task task in tasks)
        //{
        //    task?.MyTaskCard?.Disable();
        //}

        //levelCompletedImage.SetActive(true);

        // Disable all InteractableObjects
        //DisableAllObjectsOfType.DisableAllObjects<Interactable>();
        PrototypeManager.GetInstance().CompleteWave();
    }

    public void ResetTasks()
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            Task task = tasks[i];
            if (task == null)
            {
                tasks.Remove(task);
            }
            else
            {
                task.gameObject.SetActive(false);
            }
        }
    }
}
