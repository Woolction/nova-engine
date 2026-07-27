using SweetEngine.Resources;
using SweetEngine.Graphics;
using SweetEngine.Editor;
using SweetLib.Intents;
using SweetLib.Devices;
using SweetEngine.MDI;
using SweetEngine.Core.APIs;

namespace SweetEngine.Core;

public unsafe ref struct Engine
{
    public Device Device;
    public Intent Intent;

    public KitchenManager Kitchen;
    public MixerManager Mixer;

    public RenderPipeline Renderer;
    public ResourceManager Resource;

    public EditorManager Editor;
    public GuiSystem Gui;

    public Engine()
    {
        Device = new();
        Intent = new();
        Kitchen = new();
        Mixer = new();
        Renderer = new();
        Resource = new();
        Editor = new();
        Gui = new();
    }

    public void Init(in EngineContext context)
    {
        Device.Init();
        Intent.Init(GraphicContext.Window, GraphicContext.Glfw);
        Kitchen.Init(in context);
        Mixer.Init();
        Resource.Init();
        Renderer.Init(in context);
        Editor.Init();
        Gui.Init(in context);
    }

    public void Dispose()
    {
        Intent.Dispose();

        Kitchen.Dispose();

        Mixer.Dispose();

        Resource.Dispose();
    }
}