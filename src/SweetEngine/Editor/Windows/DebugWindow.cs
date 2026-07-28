using SweetLib.Generator.Attributes;
using SweetLib.Generator.Enums;
using SweetEngine.Core;
using ImGuiNET;

namespace SweetEngine.Editor.Windows;

[Window]
public unsafe struct DebugWindow
{    
    [Stage(EditorStages.Draw)]
    public readonly void Draw(in EngineContext context)
    {
        ImGui.Begin("Debug");

        ImGui.Text($"Application average {1000f / ImGui.GetIO().Framerate:F3} ms/frame ({ImGui.GetIO().Framerate:F1} FPS)");
        ImGui.Text($"Cursor pos: {context.Device->Mouse.Position}");
        ImGui.Text($"Window size: {context.Device->Window.Size}");
        ImGui.End();
    }
}
