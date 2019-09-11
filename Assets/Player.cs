using UnityEngine;

public enum Playmode { VR, PC }
public class Player : MonoBehaviour
{
    [SerializeField] private Playmode playmode;

    [Header("Setup")]
    [SerializeField] private GameObject vr;
    [SerializeField] private GameObject pc;

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
}