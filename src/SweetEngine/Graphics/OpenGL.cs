using System.Runtime.InteropServices;
using SweetEngine.MDI.Mixers; 
using SweetEngine.Resources;
using SweetLib.Intents;
using SweetLib.Devices;
using SweetEngine.Core;
using SweetEngine.MDI;
using Silk.NET.OpenGL;
using System.Numerics;
using Silk.NET.GLFW;

namespace SweetEngine.Graphics;

public unsafe static class OpenGL
{
    public static CameraMixer* cameraMixer;
    public static FrameBuffer* frameBuffer;

    private static Matrix4x4 view;
    private static Matrix4x4 proj;

    private static bool isLineRender;
    private static bool isBinding;

    public static void Init(in EngineContext context)
    {
        var gl = GraphicContext.GL;
        
        isLineRender = false;
        isBinding = false;

        gl.FrontFace(FrontFaceDirection.Ccw);
        gl.Enable(EnableCap.DepthTest);

        gl.LineWidth(1f);

        context.Resource->ShaderLoader.Use(context.Resource->Shader);

        context.Resource->ShaderLoader.SetInt(context.Resource->Shader, "uBaseMap", 0);
        context.Resource->ShaderLoader.SetInt(context.Resource->Shader, "uNormalMap", 1);
        context.Resource->ShaderLoader.SetInt(context.Resource->Shader, "uMetallicMap", 2);

        context.Resource->ShaderLoader.SetVector3(context.Resource->Shader, "uLightDir", new Vector3(0f, 1f, 1f));
        context.Resource->ShaderLoader.SetFloat(context.Resource->Shader, "uLightIntensity", 2f);
    }

    public static void Before(in EngineContext context)
    {
        var gl = GraphicContext.GL;

        LineRenderMode();

        frameBuffer->Bind();

        gl.ClearColor(0.02f, 0.02f, 0.03f, 1.0f);
        gl.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));
    }

    public static void AfterBefore(in EngineContext context)
    {
        view = Matrix4x4.CreateLookAt(cameraMixer->Transform.Position, cameraMixer->Transform.Position + cameraMixer->Transform.GetForward(), Vector3.UnitY);
        proj = Matrix4x4.CreatePerspectiveFieldOfView((float)Math.PI / 4f, cameraMixer->Aspect, 0.1f, 1000f);

        context.Resource->ShaderLoader.Use(context.Resource->Shader);
        context.Resource->ShaderLoader.SetVector3(context.Resource->Shader, "uViewPos", cameraMixer->Transform.Position);
    }

    public static void Render(in EngineContext context)
    {
        var gl = GraphicContext.GL;

        for (uint i = 0; i < context.Kitchen->CurrentKitchen.Desserts.Length; i++)
        {
            ref Dessert _object = ref context.Kitchen->CurrentKitchen.Desserts[i];
            
            context.Resource->ShaderLoader.SetVector4(context.Resource->Shader, "uColor", _object.Renderer.material->Color);

            Matrix4x4 model = _object.Transform.LocalToWorldMatrix;
            Matrix4x4 mvp = model * view * proj;

            context.Resource->ShaderLoader.SetMatrix4(context.Resource->Shader, "uMVP", &mvp);
            context.Resource->ShaderLoader.SetMatrix4(context.Resource->Shader, "uModel", &model);

            gl.BindVertexArray(_object.Renderer.vao);

            if (isLineRender)
            {
                if (isBinding)
                {
                    _object.Renderer.material->UnBind(gl);

                    isBinding = false;
                }

                gl.DrawElements(
                    PrimitiveType.Lines,
                    _object.Renderer.lineIndices.Length,
                    DrawElementsType.UnsignedInt,
                    (void*)0);
            }
            else
            {
                if (!isBinding)
                    isBinding = true;

                _object.Renderer.material->Bind(gl);

                gl.DrawElements(
                    PrimitiveType.Triangles,
                    _object.Renderer.mesh.Indices.Length,
                    DrawElementsType.UnsignedInt,
                    (void*)0);
            }
        }

        frameBuffer->UnBind(in context.Device->Window);
    }

    public static void After(in EngineContext context)
    {
        GraphicContext.GL.BindVertexArray(0);
        GraphicContext.Glfw.SwapBuffers(GraphicContext.Window);
    }

    private static void LineRenderMode()
    {
        if (Input.IsHeld(Keys.Number1))
        {
            if (isLineRender)
            {
                isLineRender = false;

                Console.WriteLine($"[Render Mode] => Fill mode");
            }
        }
        else if (Input.IsHeld(Keys.Number2))
        {
            if (!isLineRender)
            {
                isLineRender = true;

                Console.WriteLine($"[Render Mode] => LineOnly");
            }
        }
    }
}