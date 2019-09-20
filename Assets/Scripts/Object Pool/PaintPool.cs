public class PaintPool : ObjectPool
{
    private static PaintPool instance;

    protected override void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        base.Awake();
    }

    public static PaintPool GetInstance() { return instance; }
}