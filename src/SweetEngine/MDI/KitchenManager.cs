using SweetEngine.Core;

namespace SweetEngine.MDI;

public unsafe struct KitchenManager
{
    public Kitchen CurrentKitchen;
    
    public KitchenManager()
    {
        CurrentKitchen = new();
    }

    public void Init(in EngineContext context)
    {
        CurrentKitchen.Init(context.Resource);
    }

    public void Change()
    {
        
    }

    public void Dispose()
    {
        CurrentKitchen.Dispose();
    }
}