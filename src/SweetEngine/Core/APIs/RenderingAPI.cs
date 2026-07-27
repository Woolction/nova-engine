namespace SweetEngine.Core.APIs;

public unsafe struct RenderingAPI
{
    public delegate*<in EngineContext, void> Init;
    public delegate*<in EngineContext, void> Before;
    public delegate*<in EngineContext, void> AfterBefore;
    public delegate*<in EngineContext, void> Render;
    public delegate*<in EngineContext, void> After;
}