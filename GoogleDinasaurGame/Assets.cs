using Microsoft.Xna.Framework.Graphics;

namespace GoogleDinasaurGame
{
    public static class Assets
    {
        public static Texture2D DinasaurTexture, CactusTexture, GroundTexture, CloudTexture;
        public static SpriteFont ScoreFont;

        public static void Load()
        {
            DinasaurTexture = Globals.Content.Load<Texture2D>("Dinasaur");
            CactusTexture = Globals.Content.Load<Texture2D>("Cactus");
            GroundTexture = Globals.Content.Load<Texture2D>("Ground");
            CloudTexture = Globals.Content.Load<Texture2D>("Cloud");

            ScoreFont = Globals.Content.Load<SpriteFont>("ScoreFont");
        }
    }
}