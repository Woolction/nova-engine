using SweetLib.Collections.Unsafe.List;

namespace SweetEngine.MDI.Ingredients;

public static unsafe class IngredientStorage<T> where T : unmanaged
{
    public static UnsafeList<T>* Pointer;
}