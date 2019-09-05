using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class TaskGrave : MonoBehaviour
{
    [SerializeField] private Heads head = Heads.None;
    [SerializeField] private Bodies body = Bodies.None;
    [SerializeField] private List<Ornaments> ornament;

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Hello I am körs");

        TaskObject taskObject = other.GetComponent<TaskObject>();
        if(taskObject != null) {
            if(taskObject.head != Heads.None && taskObject.head != Heads.NumberOfTypes) {
                head = taskObject.head;
                Debug.Log("Added " + head);
                //return;
            }
            else if (taskObject.body != Bodies.None && taskObject.body != Bodies.NumberOfTypes) {
                body = taskObject.body;
                Debug.Log("Added " + body);
                //return;
            }
            else if (taskObject.ornament != Ornaments.None && taskObject.ornament != Ornaments.NumberOfTypes) {
                ornament.Add(taskObject.ornament);
                Debug.Log("Added " + taskObject.ornament);
                //return;
            }
            else {
                Debug.LogWarning("Task object found but no valid ingredient found");
            }
        }
        else {
            Debug.LogWarning("No task object found");
        }

        Debug.Log("Checking all the tasks, all " + FindObjectOfType<TaskManager>().tasks.Length);

        foreach (Task task in FindObjectOfType<TaskManager>().tasks) {
            if (task.CheckTask(head, body, ornament)) {
                Debug.Log("TaskGrave: FINISHED TASK, AWW YEAH");
                head = Heads.None;
                body = Bodies.None;
                ornament.Clear();
            }
        }
    }
}