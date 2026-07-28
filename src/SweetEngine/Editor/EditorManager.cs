using SweetEngine.Editor.Windows;
using SweetEngine.Core;

namespace SweetEngine.Editor;

public unsafe struct EditorManager
{
    private DockSpace dockSpace;

    public void Init()
    {
        _ = AssetDirectories.Root;

        dockSpace = new();
    }

    public readonly void Draw(in EngineContext context)
    {
        dockSpace.Draw(in context);
    }

    public void Dispose()
    {
        dockSpace.Dispose();
    }
}