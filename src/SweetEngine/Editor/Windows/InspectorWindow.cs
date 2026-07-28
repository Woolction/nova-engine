using SweetLib.Generator.Attributes;
using SweetLib.Generator.Enums;
using SweetEngine.Core;
using ImGuiNET;

namespace SweetEngine.Editor.Windows;

[Window]
public struct InspectorWindow
{
    [Stage(EditorStages.Draw)]
    public readonly void Draw(in EngineContext context)
    {
        ImGui.Begin("Inspector");

        ImGui.End();
    }
}