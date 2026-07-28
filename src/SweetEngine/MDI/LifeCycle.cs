// auto-generated

using SweetEngine.MDI.Mixers;
using SweetEngine.Core;

namespace SweetEngine.MDI;

/// <summary>
///     this code was automatically generated
/// </summary>
public unsafe partial struct LifeCycle
{
    public CameraMixer cameraMixer;

    public LifeCycle()
    {
        cameraMixer = new();
    }

    public void Whip(in EngineContext context)
    {
        cameraMixer.Whip(in *context.Intent, in context.Device->Time, in context.Device->Mouse);
    }
}