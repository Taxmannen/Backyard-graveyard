public class FlowerPool : ObjectPool
{
    private static FlowerPool instance;

    protected override void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        base.Awake();
    }

    public static FlowerPool GetInstance() { return instance; }
}