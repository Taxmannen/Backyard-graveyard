public class OrnamentThiefPool : ObjectPool
{
    private static OrnamentThiefPool instance;

    protected override void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        base.Awake();
    }
    public static OrnamentThiefPool GetInstance() { return instance; }
}