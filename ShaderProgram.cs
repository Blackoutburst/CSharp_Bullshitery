using OpenTK.Graphics.OpenGL4;

public class ShaderProgram {

    protected int id;

    public ShaderProgram(int id) {
        this.id = id;
    }

    public int GetId() {
        return (this.id);
    }

    public static ShaderProgram LoadShaderProgram(string vertexPath, string fragmentPath) {
        ShaderProgram program = new ShaderProgram(GL.CreateProgram());

        Shader vertexShader = Shader.LoadShader(vertexPath, ShaderType.VertexShader);
        Shader fragmentShader = Shader.LoadShader(fragmentPath, ShaderType.FragmentShader);

        GL.AttachShader(program.id, vertexShader.GetId());
        GL.AttachShader(program.id, fragmentShader.GetId());
        GL.LinkProgram(program.id);
        GL.DetachShader(program.id, vertexShader.GetId());
        GL.DetachShader(program.id, fragmentShader.GetId());
        GL.DeleteShader(vertexShader.GetId());
        GL.DeleteShader(fragmentShader.GetId());

        string log = GL.GetProgramInfoLog(program.id);

        if (!string.IsNullOrEmpty(log)) Console.WriteLine(log);

        return (program);
    }
}