using SweetEngine.IO.Loaders;
using SweetEngine.Resources.Shaders;
using SweetLib.Devices;

namespace SweetEngine.Resources;

public struct ResourceManager
{
    public TextureLoader TextureLoader;
    public ShaderLoader ShaderLoader;

    public Dictionary<string, ObjLoader> MeshLoaders;

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

        MeshLoaders[".obj"] = new ObjLoader();
        TextureLoader = new();
    }

    public void Dispose()
    {
        TextureLoader.Dispose();
        Shader.Dispose(in ShaderLoader);
    }
}