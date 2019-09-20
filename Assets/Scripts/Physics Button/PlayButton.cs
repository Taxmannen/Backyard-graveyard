/* Script Made By Daniel */
using UnityEngine;

public class PlayButton : PhysicsButton
{
    protected override void ButtonPush()
    {
        //Start Game
        Debug.Log("Play");
        PrototypeManager.GetInstance().AdvanceWave();
        base.ButtonPush();
    }
}