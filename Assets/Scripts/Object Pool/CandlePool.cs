public class CandlePool : ObjectPool
{
    private static CandlePool instance;

    protected override void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        base.Awake();
    }

    public static CandlePool GetInstance() { return instance; }
}