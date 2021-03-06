using OpenTK.Graphics.OpenGL4;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

public class Texture {
    protected int id;

    public Texture(string filePath) {
        this.id = GL.GenTexture();

        GL.BindTexture(TextureTarget.Texture2D, this.id);

        Image<Rgba32> image = Image.Load<Rgba32>(filePath);

        image.Mutate(x => x.Flip(FlipMode.Vertical));

        var pixels = new List<byte>(4 * image.Width * image.Height);

        for (int y = 0; y < image.Height; y++) {
            var row = image.GetPixelRowSpan(y);

            for (int x = 0; x < image.Width; x++) {
                pixels.Add(row[x].R);
                pixels.Add(row[x].G);
                pixels.Add(row[x].B);
                pixels.Add(row[x].A);
            }
        }
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels.ToArray());
        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
    }

    public int GetId() {
        return (this.id);
    }
}