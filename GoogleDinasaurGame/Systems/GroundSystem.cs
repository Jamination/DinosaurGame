using GoogleDinasaurGame.Components;
using Microsoft.Xna.Framework;

namespace GoogleDinasaurGame.Systems
{
    public static class GroundSystem
    {
        public static Ground[] Grounds = new Ground[2];

        public static void Load()
        {
            for (int i = 0; i < Grounds.Length; i++)
            {
                Grounds[i] = new Ground();
            
                Grounds[i].Sprite.Texture = Assets.GroundTexture;
                Grounds[i].Sprite.Colour = Color.White;
                Grounds[i].Sprite.Centered = false;
            
                Grounds[i].Transform.Scale = Vector2.One * 2;
                Grounds[i].Transform.Position = new Vector2(Grounds[i].Sprite.Texture.Width * Grounds[i].Transform.Scale.X * i, (GameSettings.ScreenHeight * .5f) + 140);
            }
        }

        public static void Update()
        {
            for (int i = 0; i < Grounds.Length; i++)
            {
                Grounds[i].Transform.Position.X -= Globals.Speed;
                
                if (Grounds[i].Transform.Position.X <= -Grounds[i].Sprite.Texture.Width * Grounds[i].Transform.Scale.X)
                    Grounds[i].Transform.Position = new Vector2(Grounds[i].Sprite.Texture.Width * Grounds[i].Transform.Scale.X, (GameSettings.ScreenHeight * .5f) + 140);
            }
        }

        public static void Draw()
        {
            for (int i = 0; i < Grounds.Length; i++)
            {
                Functions.DrawSprite(ref Grounds[i].Sprite, ref Grounds[i].Transform);
            }
        }
    }
}