using Shader = SweetEngine.Resources.Shader;

using System.Numerics;
using Silk.NET.OpenGL;
using SweetLib.Devices;

namespace SweetEngine.IO.Loaders;

public unsafe struct ShaderLoader
{
    public uint Load(in string vertexSrc, in string fragmentSrc)
    {
        var gl = GraphicContext.GL;

        uint vertex = gl.CreateShader(ShaderType.VertexShader);
        gl.ShaderSource(vertex, vertexSrc);
        gl.CompileShader(vertex);
        CheckCompileErrors(gl, vertex, "VERTEX");

        uint fragment = gl.CreateShader(ShaderType.FragmentShader);
        gl.ShaderSource(fragment, fragmentSrc);
        gl.CompileShader(fragment);
        CheckCompileErrors(gl, fragment, "FRAGMENT");

        uint id = gl.CreateProgram();
        gl.AttachShader(id, vertex);
        gl.AttachShader(id, fragment);
        gl.LinkProgram(id);
        CheckLinkErrors(gl, id);

        gl.DetachShader(id, vertex);
        gl.DetachShader(id, fragment);
        gl.DeleteShader(vertex);
        gl.DeleteShader(fragment);

        return id;
    }

    private void CheckCompileErrors(GL gl, uint shader, string type)
    {
        gl.GetShader(shader, ShaderParameterName.CompileStatus, out int status);
        if (status == 0)
        {
            string info = gl.GetShaderInfoLog(shader);
            Console.WriteLine($"ERROR::SHADER_COMPILATION_ERROR of type: {type}\n{info}\n");
        }
    }

    private void CheckLinkErrors(GL gl, uint programId)
    {
        gl.GetProgram(programId, ProgramPropertyARB.LinkStatus, out int status);
        if (status == 0)
        {
            string info = gl.GetProgramInfoLog(programId);
            Console.WriteLine($"ERROR::PROGRAM_LINKING_ERROR\n{info}\n");
        }
    }

    public void Delete(ref Shader shader)
    {
        if (shader.Id != 0)
        {
            GraphicContext.GL.DeleteProgram(shader.Id);
            shader.Id = 0;
        }
    }

    public void Use(Shader shader) => GraphicContext.GL.UseProgram(shader.Id);

    public void SetInt(Shader shader, in string name, int value)
    {
        var gl = GraphicContext.GL;

        int loc = gl.GetUniformLocation(shader.Id, name);
        gl.Uniform1(loc, value);

        CheckCurrentProgram(gl, name, loc);
    }

    public void SetFloat(Shader shader, in string name, float value)
    {
        var gl = GraphicContext.GL;

        int loc = gl.GetUniformLocation(shader.Id, name);
        gl.Uniform1(loc, value);

        CheckCurrentProgram(gl, name, loc);
    }

    public void SetVector4(Shader shader, in string name, in Vector4 vec)
    {
        var gl = GraphicContext.GL;

        int loc = gl.GetUniformLocation(shader.Id, name);
        gl.Uniform4(loc, vec.X, vec.Y, vec.Z, vec.W);

        CheckCurrentProgram(gl, name, loc);
    }

    public void SetVector3(Shader shader, in string name, in Vector3 vec)
    {
        var gl = GraphicContext.GL;

        int loc = gl.GetUniformLocation(shader.Id, name);
        gl.Uniform3(loc, vec.X, vec.Y, vec.Z);

        CheckCurrentProgram(gl, name, loc);
    }

    public void SetMatrix4(Shader shader, in string name, Matrix4x4* mat, bool transpose = false)
    {
        var gl = GraphicContext.GL;

        int loc = gl.GetUniformLocation(shader.Id, name);
        gl.UniformMatrix4(loc, 1, transpose, (float*)mat);        
        CheckCurrentProgram(gl, name, loc);
    }

    private void CheckCurrentProgram(GL gl, string name, int loc)
    {
        /* Console.WriteLine($"{name} -> {loc}");

         int currentProgram;
         gl.GetInteger(GetPName.CurrentProgram, out currentProgram);

         Console.WriteLine($"Current = {currentProgram}, Expected = {id}");*/
    }
}