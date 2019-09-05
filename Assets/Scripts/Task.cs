using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Heads { Red, Green, Blue, NumberOfTypes };
public enum Bodies { Red, Green, Blue, NumberOfTypes };
public enum Ornaments { Candle, Flower, NumberOfTypes };

public class Task : MonoBehaviour
{
    public GameObject PrefabTaskCard;
    private TaskCard taskCard;

    private Heads head;
    private Bodies body;
    private Ornaments[] ornaments;

    private void Start() {
        GameObject go = GameObject.Instantiate(PrefabTaskCard, transform.position, transform.rotation);
        taskCard = go.GetComponent<TaskCard>();

        RefreshTaskCardIngredients();
    }

    private void CompleteTask() {
        // Clean up task card, etc?
        RefreshTaskCardIngredients();
    }

    private void RefreshTaskCardIngredients() {
        int headIndex = RandomManager.GetRandomNumber(0, (int)Heads.NumberOfTypes);
        int bodydIndex = RandomManager.GetRandomNumber(0, (int)Bodies.NumberOfTypes);
        int ornament1 = RandomManager.GetRandomNumber(0, (int)Ornaments.NumberOfTypes);
        int ornament2 = RandomManager.GetRandomNumber(0, (int)Ornaments.NumberOfTypes);
        int ornament3 = RandomManager.GetRandomNumber(0, (int)Ornaments.NumberOfTypes);

        taskCard.SetTaskIngredients(ornament1, ornament2, ornament3, bodydIndex, headIndex);
    }

    public bool CheckTask(Heads head, Bodies body, List<Ornaments> ornaments) {
        if(this.head == head && this.body == body) {
            //correct body, check ornaments

            foreach(Ornaments ornament in this.ornaments) {
                if (!ornaments.Contains(ornament)) {
                    return false;
                }
            }

            CompleteTask();
            return true;
        }

        return false;
    }
}
