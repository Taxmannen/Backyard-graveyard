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

    private bool completed = false;

    TaskManager taskManager;

    public TaskCard TaskCard { get => taskCard; private set => taskCard = value; }
    public TaskManager TaskManager { get => taskManager; set => taskManager = value; }


    private void Start() {
        GameObject go = GameObject.Instantiate(PrefabTaskCard, transform.position, transform.rotation);
        TaskCard = go.GetComponent<TaskCard>();

        RefreshTaskCardIngredients();
    }

    private void CompleteTask() {
        // Clean up task card, etc?

        Debug.Log("Task completed");

        //RefreshTaskCardIngredients();
        TaskCard.TaskCompleted();
        TaskManager.CompleteTask(this);
    }

    private void RefreshTaskCardIngredients() {
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
    }

    /// <summary>
    /// Returns true if the task completed this frame
    /// </summary>
    /// <param name="head"></param>
    /// <param name="body"></param>
    /// <param name="ornaments"></param>
    /// <returns>Returns true if the task completed this frame</returns>
    public bool CheckTask(Heads head, Bodies body, List<Ornaments> ornaments) {
        if (completed) {
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
