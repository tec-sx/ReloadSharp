namespace Core.Resources.Textures
{
    using Raylib_cs;

    public class Texture_RL : ITexture
    {
        private Texture2D _texture;

        public Texture_RL(Texture2D texture)
        {
            _texture = texture;
        }

        public void Render(int x, int y, int w, int h)
        {
            Raylib.DrawTexture(_texture, x, y, Color.WHITE);
        }

        public void Update()
        {

        }
    }
}
