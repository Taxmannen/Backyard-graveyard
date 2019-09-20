public class ZombiePool : ObjectPool
{
    protected override void Awake()
    {
        SetInstance(this);
        base.Awake();
    }
}