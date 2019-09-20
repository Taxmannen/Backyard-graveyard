public class ZombiePool : ObjectPool
{
    private static ZombiePool instance;

    protected override void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        base.Awake();
    }

    public static ZombiePool GetInstance() { return instance; }
}