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

public enum Heads { Red, Green, Blue, [System.ObsoleteAttribute] NumberOfTypes, [System.ObsoleteAttribute] None };
public enum Bodies { Red, Green, Blue, [System.ObsoleteAttribute] NumberOfTypes, [System.ObsoleteAttribute] None };
public enum Ornaments { Candle, Flower, [System.ObsoleteAttribute] NumberOfTypes, [System.ObsoleteAttribute] None };

public class Task : MonoBehaviour
{
    public GameObject PrefabTaskCard;
    private TaskCard taskCard;

    private Heads head;
    private Bodies body;
    private Ornaments[] ornaments = new Ornaments[3];

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

        int headIndex = RandomManager.GetRandomNumber(0, (int)Heads.NumberOfTypes);
        head = (Heads)headIndex;
        int bodydIndex = RandomManager.GetRandomNumber(0, (int)Bodies.NumberOfTypes);
        body = (Bodies)bodydIndex;
        int ornament1 = RandomManager.GetRandomNumber(0, (int)Ornaments.NumberOfTypes);
        ornaments[0] = (Ornaments)ornament1;
        int ornament2 = RandomManager.GetRandomNumber(0, (int)Ornaments.NumberOfTypes);
        ornaments[1] = (Ornaments)ornament2;
        int ornament3 = RandomManager.GetRandomNumber(0, (int)Ornaments.NumberOfTypes);
        ornaments[2] = (Ornaments)ornament3;

        TaskCard.SetTaskIngredients(ornament1, ornament2, ornament3, bodydIndex, headIndex);

        maxTimeInSeconds = RandomManager.GetRandomNumber(taskManager.TimeLimitInSecondsMin, taskManager.TimeLimitInSecondsMax);

        startTime = DateTime.Now;
    }

    /// <summary>
    /// Returns true if the task completed this frame
    /// </summary>
    /// <param name="head"></param>
    /// <param name="body"></param>
    /// <param name="ornaments"></param>
    /// <returns>Returns true if the task completed this frame</returns>
    public bool CheckTask(Heads head, Bodies body, List<Ornaments> ornaments) {
        if (taskEnded) {
            Debug.Log("Completed");
            return false;
        }

        Debug.Log("Checking stuff, our head " + this.head + " == " + head + " our body " + this.body + " == " + body);
        if (this.head == head && this.body == body) {
            //correct body, check ornaments

            List<Ornaments> tmpOrnaments = new List<Ornaments>(ornaments);
            foreach(Ornaments ornament in this.ornaments) {
                if (!tmpOrnaments.Contains(ornament)) {
                    Debug.Log("oH NOES they dId NoT COnTAiN " + ornament);
                    return false;
                }
                else {
                    tmpOrnaments.Remove(ornament);
                }
            }

            CompleteTask();
            return true;
        }

        return false;
    }
}
