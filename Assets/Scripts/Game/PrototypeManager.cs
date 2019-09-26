using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <author>Simon</author>
/// </summary>

[Serializable]
public class GameWaveObjects
{
    public List<GameObject> gameObjectsForWave;
}

public class PrototypeManager : Singleton<PrototypeManager>
{
    [Header("Levels")]
    [SerializeField] private LevelSO[] levels;
    [SerializeField, ReadOnly] private int currentLevel = -1;
    [SerializeField, ReadOnly] private int currentWave = -1;
    [SerializeField] private bool clearInteractablesOnPickup = false;
    [SerializeField] private List<GameWaveObjects> gameWaveObjects;

    [Header("References")]
    public TaskDoneBox taskDoneBox;
    //[SerializeField] private EnemySpawner zombieSpawner;
    //[SerializeField] private EnemySpawner graveRobberSpawner;

    private PlayButton playButton;
    private DateTime waveStartTime;

    public int NrOfTasks { get => GetCurrentWave().nrOfTasks; private set => GetCurrentWave().nrOfTasks = value; }
    public int CurrentLevel {
        get => currentLevel;
        set {
            Debug.Log($"Setting currentLevel from {currentLevel} to {value}");
            currentLevel = value;
        }
    }

    private void Awake()
    {
        SetInstance(this);

        if (levels[0] == null) throw new System.Exception("No levels set in PrototypeManager");

        //CurrentLevel = 0;
        //currentWave = 0;
        //SetLevel(0);
        playButton = PlayButton.GetInstance();
        PlayButton.PlayEvent += StartNewGame;
    }

    private void Reset()
    {
        currentLevel = -1;
        currentWave = -1;
        waveStartTime = DateTime.Now;
    }

    void Start()
    {
        //This swhould not happen here pls
        //Make start functrion or somth smh
        //AdvanceWave();
        //TaskManager.GetInstance().ActivateTasks(GetCurrentWave().timePerTask, GetCurrentWave().nrOfTasks, GetCurrentWave().minNrOfOrnamnets, GetCurrentWave().maxNrOfOrnamnets);
    }

    private void Update()
    {
        if (!PlayButton.isPlaying) return;

        if (
            GetCurrentWave().timeLimit == true &&
            (DateTime.Now - waveStartTime).TotalSeconds > GetCurrentWave().timelimitForWave)
        {
            LoseGame("You ran out of time");
            //Lose game here
        }
    }

    public void LoseGame(string reason)
    {
        Debug.Log("Time limit over: you are Lose game?", this);
        TaskManager.GetInstance().levelCompletedText.text = reason + "\nPress the button to try again.";
        playButton.StopPlaying();
        TaskManager.GetInstance().ResetTasks();
        TaskManager.GetInstance().HideTaskFrames();
    }

    public LevelSO GetCurrentLevel() { return levels[CurrentLevel]; }
    public EnemyWaveSO GetCurrentWave() { return levels[CurrentLevel].gameWaves[currentWave]; }

    public void AdvanceLevel()
    {
        if(CurrentLevel + 1 < levels.Length)
        {
            CurrentLevel++;
            currentWave = -1;
            AdvanceWave();
            //SetWaveProperties();
        }
        else
        {
            Debug.Log("No more levels! You are win!");
            Instantiate(Resources.Load("Fireworks Particle"));
            playButton.StopPlaying();
            TaskManager.GetInstance().levelCompletedText.text = "No more levels! You are win!";
        }
    }

    public void CompleteWave()
    {
        if (GetCurrentWave().PauseAfterCompletedWave)
        {
            //Pause
            playButton.StopPlaying();
        }
        else StartCoroutine(AdvanceWaveOnDelay(5)); // Just continue... on a delay
    }
    IEnumerator AdvanceWaveOnDelay(int delay)
    {
        TaskManager.GetInstance().HideTaskFrames();
        for (int i = delay; i > 0; i--)
        {
            TaskManager.GetInstance().levelCompletedText.text = $"Completed wave {currentWave + 1}.. \nAdvancing in {i}";
            yield return new WaitForSecondsRealtime(1f);
        }

        TaskManager.GetInstance().levelCompletedText.text = "";
        //TaskManager.GetInstance().ShowTaskFrames();
        AdvanceWave();
    }
    public void AdvanceWave()
    {
        if(currentWave + 1 < GetCurrentLevel().gameWaves.Length)
        {
            //taskDoneBox.Reset();
            currentWave++;
            SetWaveProperties();
            taskDoneBox.Reset();

            GameWaveObjects gameWaveObject;
            try
            {
                gameWaveObject = gameWaveObjects[currentWave];
            }
            catch (Exception e)
            {
                // No wave objects for this wave
                return;
            }

            //The gameWaveObjects at current wave exists, but does the array containt any objects?
            gameWaveObject = gameWaveObjects[currentWave];
            if (gameWaveObject.gameObjectsForWave == null || gameWaveObject.gameObjectsForWave.Count < 1) return;

            for (int i = 0; i < gameWaveObject.gameObjectsForWave.Count; i++)
            {
                gameWaveObjects[currentWave].gameObjectsForWave[i].SetActive(true);
                //Invoke(go.SetActive(false), 10f);
            }
        }
        else
        {
            // No more waves in this level, move on to the next level
            AdvanceLevel();
        }
    }

    private void SetWaveProperties()
    {
        Debug.Log("Setting properties for wave " + currentWave);

        try { TaskManager.GetInstance().ResetTasks(); } catch (System.Exception e) { Debug.LogError(e); }

        waveStartTime = DateTime.Now;
        SetEnemySpawnerProperties();

        TaskManager.GetInstance().ActivateTasks(GetCurrentWave().timePerTask, GetCurrentWave().nrOfTasks, GetCurrentWave().minNrOfOrnamnets, GetCurrentWave().maxNrOfOrnamnets, GetCurrentWave().chanceOfTreatment);
    }

    //void SetLevel(LevelSO levelSO)
    //{
    //    levels[currentWave] = levelSO;
    //    if (levels[currentWave].gameWaves[0] == null) throw new System.Exception("No levels set in currentLevel");

    //    currentWave = 0;
    //    waveStartTime = DateTime.Now;
    //}

    //void SetLevel(int nr)
    //{
    //    try { LevelSO tmp = levels[nr]; }
    //    catch (Exception e) { Debug.Log("No level"); }
    //    try { EnemyWaveSO tmp = levels[nr].gameWaves[0]; }
    //    catch (Exception e){ Debug.Log("No more waves!"); }

    //    CurrentLevel = nr;
    //    Debug.Log($"Setting level to {nr}");
    //    currentWave = 0;
    //    waveStartTime = DateTime.Now;
    //    SetWaveProperties();
    //}

    private void SetEnemySpawnerProperties()
    {
        EnemySpawner.GetInstance()?.SetWavesProperties(GetCurrentWave().timeBetweenEnemySpawns, GetCurrentWave().unrestModifier, GetCurrentWave().nrOfSpawnsPerWave);
    }

    private void StartNewGame()
    {
        Reset();
        AdvanceLevel();
        //AdvanceWave();
    }
}
