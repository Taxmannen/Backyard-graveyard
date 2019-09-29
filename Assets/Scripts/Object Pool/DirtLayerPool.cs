public class DirtLayerPool : ObjectPool
{
    private static DirtLayerPool instance;

    protected override void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        base.Awake();
    }

    public static DirtLayerPool GetInstance() { return instance; }
}