using OpenTK.Graphics.OpenGL4;

public class Shader {

    protected int id;

    public Shader(int id) {
        this.id = id;
    }

    public int GetId() {
        return (this.id);
    }

    public static Shader LoadShader(string filePath, ShaderType type) {
        Shader shader = new Shader(GL.CreateShader(type));

        GL.ShaderSource(shader.id, File.ReadAllText(filePath));
        GL.CompileShader(shader.id);

        string log = GL.GetShaderInfoLog(shader.id);

        if (!string.IsNullOrEmpty(log)) {
            Console.WriteLine(log);
        }

        return (shader);
    }
}