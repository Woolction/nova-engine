using SweetLib.Collections.Unsafe.List;
using SweetEngine.MDI.Ingredients;
using SweetEngine.Resources;
using SweetLib.Devices;
using Silk.NET.OpenGL;
using SweetEngine.IO;

namespace SweetEngine.MDI;

public unsafe struct Kitchen
{
    public UnsafeList<Dessert> Desserts;

    private ResourceManager* resource;

    public Kitchen()
    {
        Desserts = new UnsafeList<Dessert>(2);
    }

    public void Init(ResourceManager* resource)
    {
        this.resource = resource;
    }

    public void AddDessert(string path, Material* mat)
    {
        var gl = GraphicContext.GL;

        var mesh = resource->MeshLoaders[Path.GetExtension(path)].Load(path);

        uint _vao = gl.GenVertexArray();
        uint _vbo = gl.GenBuffer();
        uint _ebo = gl.GenBuffer();

        gl.BindVertexArray(_vao);

        // Vertex buffer
        gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);
        gl.BufferData(
            BufferTargetARB.ArrayBuffer, (nuint)(mesh.Vertices.Length * sizeof(float)),
            mesh.Vertices.Data, BufferUsageARB.StaticDraw);

        // Index buffer       
        gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, _ebo);
        gl.BufferData(
            BufferTargetARB.ElementArrayBuffer, (nuint)(mesh.Indices.Length * sizeof(uint)),
            mesh.Indices.Data, BufferUsageARB.StaticDraw);

        uint stride = 11 * sizeof(float);

        // position
        gl.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, stride, (void*)0);
        gl.EnableVertexAttribArray(0);

        // normal
        gl.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, stride, (void*)(3 * sizeof(float)));
        gl.EnableVertexAttribArray(1);

        // uv
        gl.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, stride, (void*)(6 * sizeof(float)));
        gl.EnableVertexAttribArray(2);

        // tangent
        gl.VertexAttribPointer(3, 3, VertexAttribPointerType.Float, false, stride, (void*)(8 * sizeof(float)));
        gl.EnableVertexAttribArray(3);

        gl.BindVertexArray(0);

        gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);

        int modelLocation = gl.GetUniformLocation(resource->Shader.Id, "uModel");
        int mvpLocation = gl.GetUniformLocation(resource->Shader.Id, "uMVP");

        var obj = new Dessert()
        {
            Transform = new Transform()
            {
                ModelLoc = modelLocation,
                MvpLoc = mvpLocation
            },
            Renderer = new MeshRenderer()
            {
                lineIndices = MeshUtils.GenerateUniqueEdges(in mesh.Indices),
                material = mat,
                mesh = mesh,
                vao = _vao,
                vbo = _vbo,
                ebo = _ebo
            }
        };

        Desserts.Add(obj);
    }

    public void Dispose()
    {
        var gl = GraphicContext.GL;
        
        for (uint i = 0; i < Desserts.Length; i++)
        {
            ref Dessert dessert = ref Desserts[i];

            gl.DeleteBuffer(dessert.Renderer.vbo);
            gl.DeleteVertexArray(dessert.Renderer.vao);

            dessert.Dispose();
        }

        Desserts.Dispose();
    }
}