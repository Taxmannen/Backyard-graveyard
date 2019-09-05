using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <author>
/// Simon
/// Kristoffer
/// </author>
/// </summary>

public enum Heads { Red, Green, Blue, NumberOfTypes, None };
public enum Bodies { Red, Green, Blue, NumberOfTypes, None };
public enum Ornaments { Candle, Flower, NumberOfTypes, None };

public class Task : MonoBehaviour
{
    public GameObject PrefabTaskCard;
    private TaskCard taskCard;

    private Heads head;
    private Bodies body;
    private Ornaments[] ornaments = new Ornaments[3];

    private void Start() {
        GameObject go = GameObject.Instantiate(PrefabTaskCard, transform.position, transform.rotation);
        taskCard = go.GetComponent<TaskCard>();

        RefreshTaskCardIngredients();
    }

    private void CompleteTask() {
        // Clean up task card, etc?

        Debug.Log("Task completed");

        RefreshTaskCardIngredients();
    }

    private void RefreshTaskCardIngredients() {
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

        taskCard.SetTaskIngredients(ornament1, ornament2, ornament3, bodydIndex, headIndex);
    }

    public bool CheckTask(Heads head, Bodies body, List<Ornaments> ornaments) {
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
