public class StatuePool : ObjectPool
{
    private static StatuePool instance;

    protected override void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        base.Awake();
    }

    public static StatuePool GetInstance() { return instance; }
}