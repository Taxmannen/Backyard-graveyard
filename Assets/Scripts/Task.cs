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

//public enum HeadType { Red, Green, Blue, [System.ObsoleteAttribute] NumberOfTypes, [System.ObsoleteAttribute] None };
//public enum Bodies { Red, Green, Blue, [System.ObsoleteAttribute] NumberOfTypes, [System.ObsoleteAttribute] None };
//public enum OrnamentType { Candle, Flower, [System.ObsoleteAttribute] NumberOfTypes, [System.ObsoleteAttribute] None };

public class Task : MonoBehaviour
{
    public GameObject PrefabTaskCard;
    private TaskCard taskCard;

    private HeadType head;
    private BodyType body;
    private OrnamentType[] ornamentType = new OrnamentType[3];

    private bool taskEnded = false;

    TaskManager taskManager;

    public TaskCard TaskCard { get => taskCard; private set => taskCard = value; }
    public TaskManager TaskManager { get => taskManager; set => taskManager = value; }

    private float maxTimeInSeconds = 5f;
    private DateTime startTime;

    private bool initialised = false;

    private void Start() {
        Initialise();
    }

    public void Initialise() {
        if (initialised)
            return;

        GameObject go = GameObject.Instantiate(PrefabTaskCard, transform.position, transform.rotation);
        go.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        TaskCard = go.GetComponent<TaskCard>();

        initialised = true;
    }

    private void Update() {
        float secondsElapsed = (float)((DateTime.Now - startTime).TotalSeconds);
        float quotientCompleted = secondsElapsed / maxTimeInSeconds;

        if(quotientCompleted > 1f && !taskEnded) {
            FailTask();
        }
        else {
            taskCard.UpdateTimerBar(quotientCompleted);
        }
    }

    private void CompleteTask() {
        // Clean up task card, etc?

        //RefreshTaskCardIngredients();
        TaskCard.TaskCompleted();
        TaskManager.CompleteTask(this);
    }

    private void FailTask() {
        taskEnded = true;
        TaskCard.TaskFailed();
        TaskManager.FailTask(this);
    }

    public void RefreshTaskCardIngredients() {
        // TODO: THIS SHOULD GENERATE A UNIQUE TASK?

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

        TaskCard.SetTaskIngredients(ornament1, ornament2, ornament3, bodydIndex, headIndex);

        maxTimeInSeconds = RandomManager.GetRandomNumber(taskManager.TimeLimitInSecondsMin, taskManager.TimeLimitInSecondsMax);

        startTime = DateTime.Now;
    }

    /// <summary>
    /// Returns true if the task completed this frame
    /// </summary>
    /// <param name="head"></param>
    /// <param name="body"></param>
    /// <param name="OrnamentType"></param>
    /// <returns>Returns true if the task completed this frame</returns>
    public bool CheckTask(HeadType head, BodyType body, List<OrnamentType> OrnamentType) {
        if (taskEnded) {
            Debug.Log("Completed");
            return false;
        }

        Debug.Log("Checking stuff, our head " + this.head + " == " + head + " our body " + this.body + " == " + body);
        if (this.head == head && this.body == body) {
            //correct body, check OrnamentType

            List<OrnamentType> tmpOrnamentType = new List<OrnamentType>(OrnamentType);
            foreach(OrnamentType ornament in this.ornamentType) {
                if (!tmpOrnamentType.Contains(ornament)) {
                    Debug.Log("oH NOES they dId NoT COnTAiN " + ornament);
                    return false;
                }
                else {
                    tmpOrnamentType.Remove(ornament);
                }
            }

            CompleteTask();
            return true;
        }

        return false;
    }
}
