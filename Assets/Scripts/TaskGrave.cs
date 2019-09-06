using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <author>
/// Simon
/// Kristoffer
/// </author>
/// </summary>

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class TaskGrave : MonoBehaviour
{
    /// Instead of checking for individual body parts,
    /// an entire body should be inserted into the grave
    /// that body should/could have an interface to check
    /// what stuff it has on it
    /// maybe that data could somehow be sent directly into the task
    /// bypassing our storage
    [SerializeField] private Heads head = Heads.None;
    [SerializeField] private Bodies body = Bodies.None;
    [SerializeField] private List<Ornaments> ornament;

    private List<GameObject> stuffOnTop = new List<GameObject>();
    public bool ClearObjectsOnTopOnCompletion = true;

    private void OnTriggerEnter(Collider other) {
        TaskObject taskObject = other.GetComponent<TaskObject>();
        if(taskObject != null) {
            if(taskObject.head != Heads.None && taskObject.head != Heads.NumberOfTypes) {
                head = taskObject.head;
            }
            else if (taskObject.body != Bodies.None && taskObject.body != Bodies.NumberOfTypes) {
                body = taskObject.body;
            }
            else if (taskObject.ornament != Ornaments.None && taskObject.ornament != Ornaments.NumberOfTypes) {
                ornament.Add(taskObject.ornament);
                Debug.Log("Added " + taskObject.ornament);
            }
            else {
                Debug.LogWarning("Task object found but no valid ingredient found");
            }

            if(ClearObjectsOnTopOnCompletion) {
                // Is this tie correct object in the hierarchy?
                stuffOnTop.Add(taskObject.gameObject);
            }
        }
        else {
        }

        foreach (Task task in FindObjectOfType<TaskManager>().tasks) {
            if (task.CheckTask(head, body, ornament)) {
                Debug.Log("TaskGrave: FINISHED TASK, AWW YEAH");
                head = Heads.None;
                body = Bodies.None;
                ornament.Clear();

                if(ClearObjectsOnTopOnCompletion) {
                    foreach (GameObject go in stuffOnTop) {
                        Destroy(go);
                    }

                    stuffOnTop.Clear();

                    return;
                }
            }
        }
    }
}