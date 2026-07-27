using SweetEngine.IO.Loaders;

namespace SweetEngine.Resources;

public struct Shader
{
    public uint Id;

    public Shader(in ShaderLoader loader, in string vertexSrc, in string fragmentSrc)
    {
       Id = loader.Load(in vertexSrc, in fragmentSrc);
    }

    public void Dispose(in ShaderLoader loader)
    {
        loader.Delete(ref this);
    }
}