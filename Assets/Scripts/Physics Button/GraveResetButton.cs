using UnityEngine;

public class GraveResetButton : PhysicsButton
{
    [SerializeField] private Grave grave;

    protected override void ButtonPush()
    {
        grave.ResetGrave();
    }
}