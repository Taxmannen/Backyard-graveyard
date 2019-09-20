public class StatuePool : ObjectPool
{
    protected override void Awake()
    {
        SetInstance(this);
        base.Awake();
    }
}