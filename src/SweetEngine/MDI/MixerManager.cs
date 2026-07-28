namespace SweetEngine.MDI;

/// <summary>
///     the manager responsible for oberseeing mixers and the lifecycle.
/// </summary>
public struct MixerManager
{
    public LifeCycle Life;

    public MixerManager()
    {
        Life = new();
    }

    public void Init()
    {
    
    }
    
    public void Dispose()
    {
        
    }
}