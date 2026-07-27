namespace SweetEngine.Core.APIs;

public unsafe struct GuiAPI
{
    public delegate*<in EngineContext, void> Init;
    public delegate*<in EngineContext, void> Update;
    public delegate*<in EngineContext, void> Render;
}