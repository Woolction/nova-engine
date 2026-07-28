// Copyright © 2026 Zynres.
// Licensed under the Apache-2.0 License.

using System.Runtime.InteropServices;
using SweetEngine.MDI.Ingredients;
using SweetEngine.Editor.Windows;
using SweetEngine.Core.Enums;
using SweetEngine.MDI.Mixers;
using SweetEngine.Resources;
using SweetEngine.Graphics;
using SweetLib.Devices;
using SweetEngine.Core;
using System.Numerics;
using Sweet.Engine;


namespace SweetEngine;

unsafe class Program
{
    static void Main()
    {
        try
        {
            var engine = new Engine();
            var context = new EngineContext(
                &engine.Device, &engine.Intent, &engine.Kitchen, 
                &engine.Mixer, &engine.Resource, &engine.Editor);

            engine.Init(in context);

            var window = GraphicContext.Window;
            var glfw = GraphicContext.Glfw;
            var gl = GraphicContext.GL;

            var _baseMap = context.Resource->TextureLoader.Load(TextureType.BaseMap, AssetDirectories.Textures + "/sakuya-Base_Color.png");
            var _normalMap = context.Resource->TextureLoader.Load(TextureType.NormalMap, AssetDirectories.Textures + "/sakuya-Normal.png");
            var _metallicMap = context.Resource->TextureLoader.Load(TextureType.MetallicMap, AssetDirectories.Textures + "/sakuya-Metallic.png");

            Material* mat = (Material*)NativeMemory.Alloc((nuint)sizeof(Material));
            *mat = new Material(in context.Resource->TextureLoader);

            mat->Textures.Set(0, _baseMap);
            mat->Textures.Set(1, _normalMap);
            mat->Textures.Set(2, _metallicMap);
            mat->Color = new Vector4(1, 1, 1, 1);

            engine.Kitchen.CurrentKitchen.AddDessert(AssetDirectories.Models + "/NewSakuya.obj", mat);

            FrameBuffer* frameBuffer = (FrameBuffer*)NativeMemory.Alloc((nuint)sizeof(FrameBuffer)); 
            *frameBuffer = new FrameBuffer(640, 320);

            /*CameraMixer* cameraMixer = (CameraMixer*)NativeMemory.Alloc((nuint)sizeof(CameraMixer)); 
            *cameraMixer = new CameraMixer();*/

            //OpenGL.cameraMixer = cameraMixer;
            OpenGL.frameBuffer = frameBuffer;

            //engine.Mixer.Life.cameraMixer = cameraMixer;
            SceneWindow.Depends(frameBuffer);

            while (!glfw.WindowShouldClose(window))
            {
                glfw.PollEvents();

                engine.Device.Update();

                engine.Gui.Update(in context);

                engine.Renderer.Before(in context);
                engine.Mixer.Life.Whip(in context);
                engine.Renderer.AfterBefore(in context);

                engine.Renderer.Render(in context);

                engine.Gui.Render(in context);

                engine.Renderer.After(in context);

                engine.Intent.KickBackInvoke();
                
                Thread.Sleep(6);
            }

            Generated.Hello();

            engine.Dispose();

            frameBuffer->Dispose();

            //NativeMemory.Free(cameraMixer);
            NativeMemory.Free(frameBuffer);

            glfw.DestroyWindow(window);
            glfw.Terminate();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}