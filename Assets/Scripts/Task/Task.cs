using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <author>
/// Simon
/// Kristoffer
/// </author>
/// </summary>

[DefaultExecutionOrder(-200)]
public class Task : MonoBehaviour
{
    public GameObject PrefabTaskCard;
    [SerializeField] private TaskCard taskCard;

    private HeadType head;
    private BodyType body;
    private OrnamentType[] ornamentType = new OrnamentType[3];
    private TreatmentType treatment;

    private bool taskEnded = false;

    TaskManager taskManager;

    private float maxTimeInSeconds = 5f;
    private DateTime startTime;

    private bool initialised = false;

    public TaskCard TaskCard { get => taskCard; private set => taskCard = value; }
    public TaskManager TaskManager { get => taskManager; set => taskManager = value; }

    private void Start() {
        TaskManager = TaskManager.GetInstance();
        Initialise();
    }

    public void Reinitialise() {
        if (TaskManager.GetInstance().TasksAvailableToSelect()) {
            TaskCard.gameObject.transform.position = transform.position;
            TaskCard.gameObject.transform.localScale = new Vector3(5f, 5f, 5f);

            ResetVars();
            RefreshTaskCardIngredients();
        }
        else {
            Destroy(gameObject);
        }
    }
    public void Initialise() {
        if (initialised)
            return;

        GameObject go = GameObject.Instantiate(PrefabTaskCard, transform.position, transform.rotation, transform);
        go.transform.localScale = new Vector3(5f, 5f, 5f);
        TaskCard = go.GetComponent<TaskCard>();
        TaskCard.task = this;

        ResetVars();
    }
    private void ResetVars() {
        taskEnded = false;
        initialised = true;
        gameObject.SetActive(false);
    }

    public void Activate(float maxTimeInSeconds) {
        this.maxTimeInSeconds = maxTimeInSeconds;

        gameObject.SetActive(true);
        RefreshTaskCardIngredients();
    }

    private void Update() {
        float secondsElapsed = (float)((DateTime.Now - startTime).TotalSeconds);
        float quotientCompleted = secondsElapsed / maxTimeInSeconds;

        if(quotientCompleted > 1f && !taskEnded) {
            CompleteTask(false);
        }
        else {
            taskCard.UpdateTimerBar(quotientCompleted);
        }
    }

    private void CompleteTask(bool success) {
        // Clean up task card, etc?
        if (taskEnded) return;
        taskEnded = true;

        TaskManager.CompleteTask(this, success);

        if (success)
            TaskCard.TaskCompleted();
        else
            TaskCard.TaskFailed();

        if (TaskManager.GetInstance().TasksAvailableToSelect()) {
            //Reinitialise();
            //RefreshTaskCardIngredients();
        }
        else {
            //gameObject.SetActive(false);
        }
    }

    public void RefreshTaskCardIngredients() {
        // TODO: THIS SHOULD GENERATE A UNIQUE TASK?
        TaskManager.GetInstance().TasksInProgress++;

        int headIndex = RandomManager.GetRandomNumber(0, (int)HeadType.NumberOfTypes);
        head = (HeadType)headIndex;
        int bodydIndex = RandomManager.GetRandomNumber(0, (int)BodyType.NumberOfTypes);
        body = (BodyType)bodydIndex;
        int ornament1 = RandomManager.GetRandomNumber(0, (int)OrnamentType.NumberOfTypes);
        ornamentType[0] = (OrnamentType)ornament1;
        int ornament2 = RandomManager.GetRandomNumber(0, (int)OrnamentType.NumberOfTypes);
        ornamentType[1] = (OrnamentType)ornament2;
        int ornament3 = RandomManager.GetRandomNumber(0, (int)OrnamentType.NumberOfTypes);
        ornamentType[2] = (OrnamentType)ornament3;

        if (TaskManager.GetInstance().IncludeTreatments) {
            int treatmentIndex = RandomManager.GetRandomNumber(0, (int)TreatmentType.NumberOfTypes);

            TaskCard.SetTaskIngredients(ornament1, ornament2, ornament3, bodydIndex, headIndex, treatmentIndex);
        }
        else {
            TaskCard.SetTaskIngredients(ornament1, ornament2, ornament3, bodydIndex, headIndex);
        }

        //maxTimeInSeconds = RandomManager.GetRandomNumber(taskManager.TimeLimitInSecondsMin, taskManager.TimeLimitInSecondsMax);

        startTime = DateTime.Now;
        //taskEnded = false;
    }

    /// <summary>
    /// Returns true if the task completed this frame
    /// </summary>
    /// <param name="head"></param>
    /// <param name="body"></param>
    /// <param name="OrnamentType"></param>
    /// <returns>Returns true if the task completed this frame</returns>
    public bool CheckTask(HeadType head, BodyType body, List<OrnamentType> OrnamentType, TreatmentType treatment) {
        if (taskEnded) {
            Debug.Log("Completed");
            return false;
        }

        Debug.Log("Checking stuff, our head " + this.head + " == " + head + " our body " + this.body + " == " + body);
        if (this.head == head && this.body == body && (!TaskManager.GetInstance().IncludeTreatments || this.treatment == treatment)) {
            //correct body, check OrnamentType

            List<OrnamentType> tmpOrnamentType = new List<OrnamentType>(OrnamentType);
            foreach(OrnamentType ornament in this.ornamentType) {
                if (!tmpOrnamentType.Contains(ornament)) {
                    Debug.Log("OrnamentType did not contain: " + ornament);
                    return false;
                }
                else {
                    tmpOrnamentType.Remove(ornament);
                }
            }

            CompleteTask(true);
            return true;
        }

        return false;
    }
}
