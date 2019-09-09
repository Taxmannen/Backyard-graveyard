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
    [SerializeField] private HeadType head = HeadType.None;
    [SerializeField] private BodyType body = BodyType.None;
    [SerializeField] private List<OrnamentType> ornament;

    private List<GameObject> stuffOnTop = new List<GameObject>();
    public bool ClearObjectsOnTopOnCompletion = true;
/*
    private void OnTriggerEnter(Collider other) {
        TaskObject taskObject = other.GetComponent<TaskObject>();
        if(taskObject != null) {
            if(taskObject.head != HeadType.None && taskObject.head != HeadType.NumberOfTypes) {
                head = taskObject.head;
            }
            else if (taskObject.body != BodyType.None && taskObject.body != BodyType.NumberOfTypes) {
                body = taskObject.body;
            }
            else if (taskObject.ornament != OrnamentType.None && taskObject.ornament != OrnamentType.NumberOfTypes) {
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
                head = HeadType.None;
                body = BodyType.None;
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
    }*/
}