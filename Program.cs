using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;

public class Program {

	public Program() {
		this.Init();
	}

	private void Init() {
		GameWindowSettings gws = GameWindowSettings.Default;
		gws.IsMultiThreaded = false;
		gws.RenderFrequency = 165;
		gws.UpdateFrequency = 60;

		NativeWindowSettings nws = NativeWindowSettings.Default;
		nws.APIVersion = Version.Parse("4.1.0");
		nws.Size = new Vector2i(600, 900);
		nws.Title = "C# c'est du troll";

		GameWindow window = new GameWindow(gws, nws);
		
		ShaderProgram program = new ShaderProgram(0);
		window.Load += () => {
			program = ShaderProgram.LoadShaderProgram("shaders/vertex.glsl", "shaders/fragment.glsl");
			new Texture("res/test.png");
		};

		window.RenderFrame += (_) => {
			this.Render(window, program);
		};

		window.Run();
	}

	private void Render(GameWindow window, ShaderProgram program) {
		GL.Enable(EnableCap.CullFace);
		GL.Enable(EnableCap.Blend);
		GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
		GL.CullFace(CullFaceMode.Back);
		GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
		GL.Clear(ClearBufferMask.ColorBufferBit);

		GL.UseProgram(program.GetId());

		float[] verts = 
		{-0.8f, -0.8f, 0.8f, 0.0f, 0.0f, 0.0f, 0.0f, // Bottom left
		0.8f, -0.8f, 0.0f, 0.8f, 0.0f, 1.0f, 0.0f, // Bottom Right
		0.8f, 0.8f, 0.0f, 0.0f, 0.8f, 1.0f, 1.0f, // Top right
		0.8f, 0.8f, 0.0f, 0.0f, 0.8f, 1.0f, 1.0f, // Top Right
		-0.8f, 0.8f, 0.8f, 0.0f, 0.8f, 0.0f, 1.0f, // Top Left
		-0.8f, -0.8f, 0.8f, 0.0f, 0.0f, 0.0f, 0.0f // Bottom left
		};

		int vao = GL.GenVertexArray();
		int vertices = GL.GenBuffer();
		GL.BindVertexArray(vao);
		GL.BindBuffer(BufferTarget.ArrayBuffer, vertices);
		GL.BufferData(BufferTarget.ArrayBuffer, verts.Length * 4, verts, BufferUsageHint.StaticDraw);
		GL.EnableVertexAttribArray(0);
		GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 28, 0);
		GL.EnableVertexAttribArray(1);
		GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 28, 8);
		GL.EnableVertexAttribArray(2);
		GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 28, 20);
		
		GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
		
		GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
		GL.BindVertexArray(0);
		GL.DeleteVertexArray(vao);
		GL.DeleteBuffer(vertices);

		GL.UseProgram(0);

		window.SwapBuffers();
	}

	public static void Main(string[] args) {
		new Program();
	}
}

