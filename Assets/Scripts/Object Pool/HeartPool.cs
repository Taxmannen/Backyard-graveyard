public class HeartPool : ObjectPool
{
    protected override void Awake()
    {
        SetInstance(this);
        base.Awake();
    }
}