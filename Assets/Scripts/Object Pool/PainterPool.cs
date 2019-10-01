public class PainterPool : ObjectPool
{
    private static PainterPool instance;

    protected override void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        base.Awake();
    }

    public static PainterPool GetInstance() { return instance; }
}