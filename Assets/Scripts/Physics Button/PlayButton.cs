using System;
using UnityEngine;

/* Script Made By Daniel */
public class PlayButton : PhysicsButton
{
    #region Variables
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject[] taskObjects;
    [SerializeField] private GameObject particle;
    [SerializeField] private Transform spawnPosition;

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
        if (particle)
        {
            GameObject firework = Instantiate(particle);
            firework.transform.SetParent(spawnPosition);
            firework.transform.localPosition = new Vector3(0, 0, 0);
        }
        SetPlayState(true);
    }

    public void StopPlaying()
    {
        if (isPlaying)
        {
            StopEvent?.Invoke();
            SetPlayState(false);
        }
    }

    private void SetPlayState(bool state)
    {
        isPlaying = state;
        button.SetActive(!state);
        foreach (GameObject taskObject in taskObjects) taskObject.SetActive(state);
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