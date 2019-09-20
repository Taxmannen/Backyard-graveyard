public class PaintPool : ObjectPool
{
    protected override void Awake()
    {
        SetInstance(this);
        base.Awake();
    }
}