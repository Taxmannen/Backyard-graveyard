public class OrnamentThiefPool : ObjectPool
{
    protected override void Awake()
    {
        SetInstance(this);
        base.Awake();
    }
}