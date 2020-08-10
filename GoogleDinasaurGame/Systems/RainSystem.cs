using GoogleDinasaurGame.Components;
using Microsoft.Xna.Framework;

namespace GoogleDinasaurGame.Systems
{
    public static class RainSystem
    {
        public static Droplet[] RainDroplets = new Droplet[1000];

        public static void Load()
        {
            for (int i = 0; i < RainDroplets.Length; i++)
            {
                RainDroplets[i] = NewDroplet();
            }
        }

        public static void Update()
        {
            for (int i = 0; i < RainDroplets.Length; i++)
            {
                RainDroplets[i].Velocity.X -= Droplet.Gravity;
                RainDroplets[i].Velocity.Y += Droplet.Gravity;
                RainDroplets[i].Transform.Rotation = MathHelper.ToRadians(45f);
                RainDroplets[i].Transform.Position += RainDroplets[i].Velocity * Time.DeltaTime;

                if (RainDroplets[i].Transform.Position.Y >= GameSettings.ScreenHeight * .5f + 140)
                {
                    RainDroplets[i] = NewDroplet();
                }
            }
        }

        public static void Draw()
        {
            for (int i = 0; i < RainDroplets.Length; i++)
            {
                Functions.DrawSprite(ref RainDroplets[i].Sprite, ref RainDroplets[i].Transform);
            }
        }

        public static Droplet NewDroplet()
        {
            var droplet = new Droplet();
            droplet.Sprite.Texture = Assets.DropletTexture;
            droplet.Sprite.Colour = Color.White;
                
            droplet.Transform.Scale = Vector2.One;
            droplet.Transform.Position = new Vector2(Globals.Random.Next((int)(GameSettings.ScreenWidth * .75f), (int)(GameSettings.ScreenWidth * 2.25f)), Globals.Random.Next(-GameSettings.ScreenHeight * 8, -droplet.Sprite.Texture.Height));
            return droplet;
        }
    }
}