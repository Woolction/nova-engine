using SweetLib.Collections.Unsafe.List;
using SweetEngine.MDI.Ingredients;
using System.Numerics;

namespace SweetEngine.MDI;

public struct Dessert
{
    public UnsafeList<IngredientPtr> IngPointers;

    public Transform Transform;
    public MeshRenderer Renderer;

    public void Dispose()
    {
        IngPointers.Dispose();
        Renderer.Dispose();
    }
}
