using SweetEngine.Core.APIs;
using SweetEngine.Core;

namespace SweetEngine.Editor;

public unsafe struct GuiSystem
{
    private GuiAPI API;

    public void Init(in EngineContext context)
    {
        API = new GuiAPI()
        { 
            Init = &ImGUI.Init,
            Update = &ImGUI.Update,
            Render = &ImGUI.Render
        };  

        API.Init(in context);
    }

    public readonly void Update(in EngineContext context)
    {
        API.Update(in context);
    }

    public void Render(in EngineContext context)
    {
        API.Render(in context);
    }
}