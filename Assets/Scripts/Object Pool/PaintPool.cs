/* Script Made By Daniel */

public class PaintPool : ObjectPool
{
    public static PaintPool Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
        Setup();
    }
}