using SweetLib.Collections.Unsafe.List;
using SweetEngine.Core.APIs;
using SweetEngine.Core;

namespace SweetEngine.MDI;

/// <summary>
///     the manager responsible for oberseeing mixers and the lifecycle.
/// </summary>
public unsafe struct MixerManager
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