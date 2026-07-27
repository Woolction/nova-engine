using SweetEngine.Core;
using SweetEngine.Core.APIs;

namespace SweetEngine.Graphics;

public unsafe struct RenderPipeline
{
    private RenderingAPI API;
    
    public void Init(in EngineContext context)
    {
        API = new RenderingAPI() { 
            Init = &OpenGL.Init,
            Before = &OpenGL.Before,
            AfterBefore = &OpenGL.AfterBefore,
            Render = &OpenGL.Render,
            After = &OpenGL.After
        };

        API.Init(in context);
    }

    public void Before(in EngineContext context)
    {
        API.Before(in context);
    }

    public void AfterBefore(in EngineContext context)
    {
        API.AfterBefore(in context);
    }

    public void Render(in EngineContext context)
    {
        API.Render(in context);
    }

    public void After(in EngineContext context)
    {
        API.After(in context);
    }
}