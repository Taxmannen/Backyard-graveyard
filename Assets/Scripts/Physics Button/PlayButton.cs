using System;
using UnityEngine;

/* Script Made By Daniel */
public class PlayButton : PhysicsButton
{
    [SerializeField] private GameObject button;

    public static event Action PlayEvent;
    public static event Action StopEvent;
    public static bool isPlaying;

    protected override void ButtonPush()
    {
        PlayEvent?.Invoke();
        ClearRemaingObjects(); 
        isPlaying = true;
        button.SetActive(false);
    }

    public void StopPlaying()
    {
        if (isPlaying)
        {
            StopEvent?.Invoke();
            isPlaying = false;
            button.SetActive(true);
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
}