using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Script Made By Daniel */
public class GameManager : Singleton<GameManager>
{
    public static event Action<bool> PauseEvent;

    private bool paused;

    private void Awake()
    {
        SetInstance(this);
        if (!Application.isEditor) Cursor.visible = false;
    }

    private void Pause(bool state)
    {
        paused = state;
        if (paused) Time.timeScale = 0;
        else        Time.timeScale = 1;
        PauseEvent?.Invoke(state);
    }
}