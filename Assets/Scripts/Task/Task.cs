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
    //public GameObject PrefabTaskCard;
    //[SerializeField, ReadOnly] private TaskCard taskCard;
    [Header("References")]
    [SerializeField] private TaskCard myTaskCard;
    [SerializeField] private Transform taskCardStartPos;
    [SerializeField] private Body bodyPrefab;

    [Header("Debug")]
    [SerializeField, ReadOnly] private HeadType head;
    [SerializeField, ReadOnly] private BodyType body;
    [SerializeField, ReadOnly] private OrnamentType[] ornamentType = new OrnamentType[3];
    [SerializeField, ReadOnly] private TreatmentType treatment;
    //[SerializeField, ReadOnly] private bool instantiateNewTaskCards = true;

    private bool taskEnded = false;

    TaskManager taskManager;

    private float maxTimeInSeconds = 5f;
    private DateTime startTime;
    private int minNrOfOrnaments;
    private int maxNrOfOrnaments;
    private float chanceOfTreatment;

    private bool initialised = false;

    //public TaskCard TaskCard { get => taskCard; private set => taskCard = value; }
    public TaskCard MyTaskCard { get => myTaskCard; private set => myTaskCard = value; }
    public TaskManager TaskManager { get => taskManager; set => taskManager = value; }
    public HeadType Head { get => head; private set => head = value; }
    public BodyType Body { get => body; private set => body = value; }

    private void Start()
    {
        TaskManager = TaskManager.GetInstance();
        if (!TaskManager.TaskManagerSpawnsTasks)
        {
            TaskManager.tasks.Add(this);
            Activate(TaskManager.MaxTimeInSeconds, TaskManager.MinNrOfOrnaments, TaskManager.MaxNrOfOrnaments, TaskManager.ChanceOfTreatment);
        }
        MyTaskCard.task = this;

        //Instantiate(bodyPrefab, TaskManager.GetInstance().bodySpawnPosition.position, Quaternion.identity);
        //Initialise();
    }

    public void Reinitialise()
    {
        if (TaskManager.GetInstance().TasksAvailableToSelect())
        {
            myTaskCard.gameObject.transform.position = taskCardStartPos.position;
            myTaskCard.gameObject.transform.localScale = new Vector3(5f, 5f, 5f);

            ResetVars();
            if (TaskManager.GetInstance().TasksAvailableToSelect())
            {
                //Reinitialise();
                RefreshTaskCardIngredients();
            }
            else
            {
                Debug.LogWarning("Task error: No tasks available to do");
                TaskManager.tasks.Remove(this);
                Destroy(myTaskCard.gameObject);
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Initialise()
    {
        //if (initialised)
        //    return;

        //if(TaskCard == null || instantiateNewTaskCards) {
        //    GameObject go = GameObject.Instantiate(PrefabTaskCard, transform.position, Quaternion.identity);
        //    //go.transform.localScale = new Vector3(5f, 5f, 5f);
        //    TaskCard = go.GetComponent<TaskCard>();
        //    TaskCard.task = this;
        //}
        //else{
        //    TaskCard.gameObject.SetActive(true);
        //}

        myTaskCard.gameObject.SetActive(true);
        ResetVars();
    }
    private void ResetVars()
    {
        taskEnded = false;
        initialised = true;
        gameObject.SetActive(false);
    }

    public void Activate(float maxTimeInSeconds, int minNrOfOrnaments, int maxNrOfOrnaments, float includeTreatment)
    {
        Initialise();

        this.maxTimeInSeconds = maxTimeInSeconds;
        this.chanceOfTreatment = includeTreatment;
        this.minNrOfOrnaments = minNrOfOrnaments;
        this.maxNrOfOrnaments = maxNrOfOrnaments;


        gameObject.SetActive(true);
        if (TaskManager.GetInstance().TasksAvailableToSelect())
        {
            //Reinitialise();
            RefreshTaskCardIngredients();
        }
        else
        {
            Debug.LogWarning("Task error: No tasks available to do");
            TaskManager.tasks.Remove(this);
            Destroy(myTaskCard.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        float secondsElapsed = (float)((DateTime.Now - startTime).TotalSeconds);
        float quotientCompleted = secondsElapsed / maxTimeInSeconds;

        if (quotientCompleted > 1f && !taskEnded)
        {
            CompleteTask(false);
            TaskManager.GetInstance().CheckLevelCompletion();
        }
        else
        {
            myTaskCard?.UpdateTimerBar(quotientCompleted);
        }
    }

    private void CompleteTask(bool success)
    {
        if (taskEnded) return;
        taskEnded = true;

        TaskManager.CompleteTask(this, success);

        if (success)
            myTaskCard.TaskCompleted();
        else
            myTaskCard.TaskFailed();
    }

    public void RefreshTaskCardIngredients()
    {
        // TODO: THIS SHOULD GENERATE A UNIQUE TASK?
        TaskManager.GetInstance().TasksInProgress++;

        int headIndex = RandomManager.GetRandomNumber(0, (int)HeadType.NumberOfTypes);
        Head = (HeadType)headIndex;
        int bodydIndex = RandomManager.GetRandomNumber(0, (int)BodyType.NumberOfTypes);
        body = (BodyType)bodydIndex;

        int nrOfOrnaments = RandomManager.GetRandomNumber(minNrOfOrnaments, maxNrOfOrnaments + 1);
        //Debug.LogError($"Fetching random number {minNrOfOrnaments}:{maxNrOfOrnaments} and getting {nrOfOrnaments}");
        //ornamentType = new OrnamentType[(int)OrnamentType.NumberOfTypes]; 
        for (int i = 0; i < 3; i++)
        {
            if (i < nrOfOrnaments)
                ornamentType[i] = (OrnamentType)RandomManager.GetRandomNumber(0, (int)OrnamentType.NumberOfTypes);
            else
                ornamentType[i] = OrnamentType.None;
        }

        if (TaskManager.GetInstance().IncludeTreatments)
        {
            bool includeTreatment = RandomManager.GetRandomNumber(0, 101) < chanceOfTreatment;

            int treatmentIndex = 0;
            if (includeTreatment)
            {
                treatmentIndex = RandomManager.GetRandomNumber(0, (int)TreatmentType.NumberOfTypes);
                treatment = (TreatmentType)treatmentIndex;
            }
            else
            {
                treatment = TreatmentType.None;
            }

            myTaskCard.SetTaskIngredients((int)ornamentType[0], (int)ornamentType[1], (int)ornamentType[2], bodydIndex, headIndex, treatmentIndex, includeTreatment);
        }
        else
        {
            myTaskCard.SetTaskIngredients((int)ornamentType[0], (int)ornamentType[1], (int)ornamentType[2], bodydIndex, headIndex);
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
    public bool CheckTask(HeadType head, BodyType body, List<OrnamentType> ornamentType, TreatmentType bodyTreatment, TreatmentType headTreatment)
    {
        if (taskEnded)
        {
            Debug.Log("Completed");
            return false;
        }

        Debug.Log("Checking stuff, our head " + this.Head + " == " + head + " our body " + this.Body + " == " + body);
        if (this.Head == head && this.Body == body &&
           (!TaskManager.GetInstance().IncludeTreatments || bodyTreatment == treatment || headTreatment == treatment))
        {
            //correct body, check OrnamentType

            List<OrnamentType> tmpOrnamentType = new List<OrnamentType>(ornamentType);
            foreach (OrnamentType ornament in this.ornamentType)
            {
                if (!tmpOrnamentType.Contains(ornament) && (ornament != OrnamentType.None))
                {
                    Debug.Log("OrnamentType did not contain: " + ornament);
                    return false;
                }
                else if (ornament == OrnamentType.None) { }
                else
                {
                    tmpOrnamentType.Remove(ornament);
                }
            }

            CompleteTask(true);
            return true;
        }

        return false;
    }
}
