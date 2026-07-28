using SweetEngine.Core;
using ImGuiNET;

namespace SweetEngine.Editor.Windows;

/// <summary>
///     this code was automatically generated
/// </summary>
public readonly unsafe struct DockSpace
{
    private readonly HierarchyWindow hierarchyWindow;
    private readonly InspectorWindow inspectorWindow;
    private readonly ConsoleWindow consoleWindow;
    private readonly DebugWindow debugWindow;
    private readonly SceneWindow sceneWindow;
    private readonly GameWindow gameWindow;

    public DockSpace()
    {
        hierarchyWindow = new();
        inspectorWindow = new();
        consoleWindow = new();
        debugWindow = new();
        sceneWindow = new();
        gameWindow = new();
    }

    public void Draw(in EngineContext context)
    {
        ImGui.DockSpaceOverViewport(ImGui.GetID("main_dock_space"), ImGui.GetMainViewport());

        hierarchyWindow.Draw(in context);
        inspectorWindow.Draw(in context);
        consoleWindow.Draw(in context);
        debugWindow.Draw(in context);
        gameWindow.Draw(in context);
        sceneWindow.Draw(in context);
    }

    public void Dispose()
    {
        
    }
}