using UnityEngine;

public class GraveResetButton : PhysicsButton
{
    [SerializeField] private Grave grave;
    [SerializeField] private float timeToExecute;

    protected override void ButtonPush()
    {
        grave.ResetGrave();
        base.ButtonPush();
    }
}