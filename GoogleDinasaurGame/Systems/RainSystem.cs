using GoogleDinasaurGame.Components;
using Microsoft.Xna.Framework;

namespace GoogleDinasaurGame.Systems
{
    public static class RainSystem
    {
        public static Droplet[] RainDroplets = new Droplet[100];

        public static void Load()
        {
            for (int i = 0; i < RainDroplets.Length; i++)
            {
                RainDroplets[i] = NewDroplet();
            }
        }

        public static void Update()
        {
        }

        public static void Draw()
        {
            
        }

        public static Droplet NewDroplet()
        {
            var droplet = new Droplet();
            droplet.Sprite.Texture = Assets.DropletTexture;
            droplet.Sprite.Colour = Color.White;
                
            droplet.Transform.Scale = Vector2.One;
            droplet.Transform.Position = new Vector2(Globals.Random.Next(0, GameSettings.ScreenWidth), -droplet.Sprite.Texture.Height);
            return droplet;
        }
    }
}