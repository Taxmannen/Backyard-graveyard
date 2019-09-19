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
    [SerializeField, ReadOnly] private TaskCard taskCard;
    [SerializeField] private Transform taskCardStartPos;

    [Header("Debug")]
    [SerializeField, ReadOnly] private HeadType head;
    [SerializeField, ReadOnly] private BodyType body;
    [SerializeField, ReadOnly] private OrnamentType[] ornamentType = new OrnamentType[3];
    [SerializeField, ReadOnly] private TreatmentType treatment;
    [SerializeField, ReadOnly] private bool instantiateNewTaskCards = true;

    private bool taskEnded = false;


    TaskManager taskManager;

    private float maxTimeInSeconds = 5f;
    private DateTime startTime;
    private int minNrOfOrnaments;
    private int maxNrOfOrnaments;

    private bool initialised = false;

    public TaskCard TaskCard { get => taskCard; private set => taskCard = value; }
    public TaskManager TaskManager { get => taskManager; set => taskManager = value; }
    public HeadType Head { get => head; private set => head = value; }
    public BodyType Body { get => body; private set => body = value; }

    private void Start() {
        TaskManager = TaskManager.GetInstance();
        //Initialise();
    }

    public void Reinitialise() {
        if (TaskManager.GetInstance().TasksAvailableToSelect()) {
            TaskCard.gameObject.transform.position = taskCardStartPos.position;
            TaskCard.gameObject.transform.localScale = new Vector3(5f, 5f, 5f);

            ResetVars();
            RefreshTaskCardIngredients();
        }
        else {
            Destroy(gameObject);
        }
    }
    public void Initialise() {
        //if (initialised)
        //    return;

        if(TaskCard == null || instantiateNewTaskCards) {
            GameObject go = GameObject.Instantiate(PrefabTaskCard, taskCardStartPos.position, Quaternion.identity);
            //go.transform.localScale = new Vector3(5f, 5f, 5f);
            TaskCard = go.GetComponent<TaskCard>();
            TaskCard.task = this;
        }
        else{
            TaskCard.gameObject.SetActive(true);
        }

        ResetVars();
    }
    private void ResetVars() {
        taskEnded = false;
        initialised = true;
        gameObject.SetActive(false);
    }

    public void Activate(float maxTimeInSeconds, int minNrOfOrnaments, int maxNrOfOrnaments) {
        Initialise();

        this.maxTimeInSeconds = maxTimeInSeconds;
        this.minNrOfOrnaments = minNrOfOrnaments;
        this.maxNrOfOrnaments = maxNrOfOrnaments;

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
        Head = (HeadType)headIndex;
        int bodydIndex = RandomManager.GetRandomNumber(0, (int)BodyType.NumberOfTypes);
        body = (BodyType)bodydIndex;

        int nrOfOrnaments = RandomManager.GetRandomNumber(minNrOfOrnaments, maxNrOfOrnaments);
        ornamentType = new OrnamentType[nrOfOrnaments]; 
        for (int i = 0; i < nrOfOrnaments; i++) {
            ornamentType[i] = (OrnamentType)RandomManager.GetRandomNumber(0, (int)OrnamentType.NumberOfTypes);
        }

        if (TaskManager.GetInstance().IncludeTreatments) {
            int treatmentIndex = RandomManager.GetRandomNumber(0, (int)TreatmentType.NumberOfTypes);
            treatment = (TreatmentType)treatmentIndex;

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
    public bool CheckTask(HeadType head, BodyType body, List<OrnamentType> OrnamentType, TreatmentType bodyTreatment, TreatmentType headTreatment) {
        if (taskEnded) {
            Debug.Log("Completed");
            return false;
        }

        Debug.Log("Checking stuff, our head " + this.Head + " == " + head + " our body " + this.Body + " == " + body);
        if (this.Head == head && this.Body == body && (!TaskManager.GetInstance().IncludeTreatments || bodyTreatment == treatment || headTreatment == treatment)) {
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
