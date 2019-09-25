using System;
using UnityEngine;

/* Script Made By Daniel */
public class PlayButton : PhysicsButton
{
    #region Variables
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject[] taskObjects;

    private static PlayButton instance;

    public static event Action PlayEvent;
    public static event Action StopEvent;
    public static bool isPlaying;
    #endregion

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        foreach (GameObject taskObject in taskObjects) taskObject.SetActive(false);
    }

    protected override void ButtonPush()
    {
        PlayEvent?.Invoke();
        ClearRemaingObjects(); 
        isPlaying = true;
        foreach (GameObject taskObject in taskObjects) taskObject.SetActive(true);
        button.SetActive(false);
    }

    public void StopPlaying()
    {
        if (isPlaying)
        {
            StopEvent?.Invoke();
            isPlaying = false;
            button.SetActive(true);
            foreach (GameObject taskObject in taskObjects) taskObject.SetActive(false);
        }
    }

    private void ClearRemaingObjects()
    {
        PaintPool paintPool = PaintPool.GetInstance();
        GameObject[] decals = GameObject.FindGameObjectsWithTag("Decal");
        foreach (GameObject decal in decals) paintPool.ReturnToPool(decal);

        Head[] heads = FindObjectsOfType<Head>();
        foreach (Head head in heads) Destroy(head.gameObject);
    }

    public static PlayButton GetInstance() { return instance; }
}