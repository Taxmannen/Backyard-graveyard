using UnityEngine;
using UnityEngine.SceneManagement;

public enum Playmode { VR, PC }

/* Script Made By Daniel */
public class Player : Singleton<Player>
{
    [SerializeField] private Playmode playmode;

    [Header("Setup")]
    [SerializeField] private GameObject vr;
    [SerializeField] private GameObject pc;

    public KeyCode restartKey;
    public KeyCode reenableObjectsKey;
    public KeyCode disableObjectsKey;

    private void Start()
    {
        SetInstance(this);
    }

    private void Awake()
    {
        switch(playmode) 
        {
            case Playmode.VR:
                vr.SetActive(true);
                pc.SetActive(false);
                break;
            case Playmode.PC:
                vr.SetActive(false);
                pc.SetActive(true);
                break;
        }
    }

    // Made By Simon
    private void Update()
    {
        #if UNITY_EDITOR
            if (Input.GetKeyUp(restartKey)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            if (Input.GetKeyUp(reenableObjectsKey)) DisableAllObjectsOfType.EnableAllDisabledObjects<Interactable>();
            if (Input.GetKeyUp(disableObjectsKey)) DisableAllObjectsOfType.DisableAllObjects<Interactable>();
        #endif
    }

    public Playmode GetPlayMode() { return playmode; }
}