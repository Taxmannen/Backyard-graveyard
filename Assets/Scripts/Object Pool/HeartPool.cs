public class HeartPool : ObjectPoolOrnament
{
    private static HeartPool instance;

    protected override void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        base.Awake();
    }

    public static HeartPool GetInstance() { return instance; }
}