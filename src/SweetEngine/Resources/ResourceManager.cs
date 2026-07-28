using SweetLib.Collections.Unsafe.Dictionary;
using SweetLib.Collections.Unsafe.Text;
using SweetEngine.Resources.Shaders;
using SweetEngine.IO.Loaders;

namespace SweetEngine.Resources;

public struct ResourceManager
{
    public TextureLoader TextureLoader;
    public ShaderLoader ShaderLoader;

    public UnsafeDictionary<U8String, ObjLoader> MeshLoaders;

    public Shader Shader;

    public ResourceManager()
    {
        ShaderLoader = new();
        MeshLoaders = new(1);
    }

    public void Init()
    {
        GraphicShader shader = new();
        Shader = new(
            in ShaderLoader, in shader.vertexSrc, in shader.fragmentSrc);

        MeshLoaders[".obj"u8] = new ObjLoader();

        TextureLoader = new();
    }

    public void Dispose()
    {
        TextureLoader.Dispose();
        Shader.Dispose(in ShaderLoader);
        
        for (uint i = 0; i < MeshLoaders.Length; i++)
        {
            MeshLoaders.Get(i).Key.Dispose();
        }
        
        MeshLoaders.Dispose();
    }
}