using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/* Script made by Petter */
public class TaskDoneBox : MonoBehaviour
{

    [Header("Text Related")]
    [SerializeField] Text taskText;

    [Header("Prefabs")]
    [SerializeField] GameObject newCardParent;

    [Header("Level Related")]
    [SerializeField, ReadOnly] int totalTasksForLevel;
    [SerializeField, ReadOnly] int numberOfTasksCompleted;
    public bool levelComplete { get; private set; }

    [Header("Other")]
    [SerializeField, ReadOnly] private List<GameObject> objectsInBox = new List<GameObject>();

    private Vector3 baseOffset = new Vector3(0, 0.06f, 0);
    private Vector3 baseRotation = new Vector3(90, 0, 0);


    private void OnEnable()
    {
        PlayButton.StopEvent += Reset;
    }

    private void OnDisable()
    {
        PlayButton.StopEvent -= Reset;
    }

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    private void Update()
    {
        if (PlayButton.isPlaying) UpdateCompletedTasksText();
    }

    public void Reset()
    {
        levelComplete = false;
        
        totalTasksForLevel = PrototypeManager.GetInstance().NrOfTasks;
        numberOfTasksCompleted = 0;
        ClearObjectsInBox();
        //ToggleText(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;
        if (other.gameObject == null) return;
        TaskCard taskCard = other.gameObject.GetComponent<TaskCard>();
        if (taskCard == null) return;

        if (taskCard.taskCompleted == true && !levelComplete)
        {
            taskCard.taskCompleted = false;
            PlaceTaskCard(taskCard.gameObject);
            UpdateCompletedTasksText();
            TaskManager.GetInstance().CheckLevelCompletion();
        }
    }

    private void PlaceTaskCard(GameObject taskCard)
    {
        if (numberOfTasksCompleted == totalTasksForLevel)
        {
            levelComplete = true;
            if (PrototypeManager.GetInstance().GetCurrentWave().PauseAfterCompletedWave) PrototypeManager.GetInstance().AdvanceWave();
        }

        Vector3 newOffset = new Vector3(baseOffset.x, baseOffset.y + (numberOfTasksCompleted * 0.01f), baseOffset.z);
        taskCard.gameObject.tag = "Untagged";
        Destroy(taskCard.GetComponent<PlaceablePickup>());
        Destroy(taskCard.GetComponent<Rigidbody>());
        Destroy(taskCard.GetComponent<Task>());
        Destroy(taskCard.GetComponentInChildren<ParticleSystem>().gameObject);
        taskCard.transform.localScale = new Vector3(0.15f, 0.15f, 0.1f);
        taskCard.transform.SetParent(newCardParent.transform);
        objectsInBox.Add(taskCard);

        taskCard.transform.localRotation = Quaternion.Euler(baseRotation);
        taskCard.transform.position = newCardParent.transform.position + (transform.rotation * newOffset);
        numberOfTasksCompleted++;
    }

    private void UpdateCompletedTasksText()
    {
        if (taskText != null)
        {
            DateTime startTime = PrototypeManager.GetInstance().waveStartTime;
            DateTime endTime = PrototypeManager.GetInstance().waveStartTime.AddSeconds(PrototypeManager.GetInstance().GetCurrentWave().timelimitForWave);
            TimeSpan timeSpan = endTime - DateTime.Now;
            string s = $"{timeSpan.Minutes:00}::{timeSpan.Seconds:00}";
            taskText.text = $"Tasks Completed\n {numberOfTasksCompleted} / {totalTasksForLevel} " +
                (PrototypeManager.GetInstance().GetCurrentWave().timeLimit ? 
                $"\n<color=red>Time to complete wave\n{s}</color>" :
                $"\n<color=red>No time limit.</color>");
        }
    }

    public void ToggleText(bool state)
    {
        taskText.gameObject.SetActive(state);
    }

    public void ClearObjectsInBox()
    {
        for (int i = 0; i < objectsInBox.Count; i++)
        {
            Destroy(objectsInBox[i]);
        }

        if (objectsInBox != null)
        {
            objectsInBox.Clear();
        }

        UpdateCompletedTasksText();
    }
}
