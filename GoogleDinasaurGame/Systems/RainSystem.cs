using GoogleDinasaurGame.Components;
using Microsoft.Xna.Framework;

namespace GoogleDinasaurGame.Systems
{
    public static class RainSystem
    {
        public static Droplet[] RainDroplets = new Droplet[10000];

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
                if (RainDroplets[i].Splashed)
                {
                    RainDroplets[i].SplashTimer++;
                    
                    if (RainDroplets[i].SplashTimer >= 5)
                        RainDroplets[i] = NewDroplet();

                    if (Globals.GameState == GameStates.Running)
                        RainDroplets[i].Transform.Position.X -= Globals.Speed;
                }
                else
                {
                    RainDroplets[i].Velocity.X -= Droplet.Gravity * RainDroplets[i].Speed;
                    RainDroplets[i].Velocity.Y += Droplet.Gravity * RainDroplets[i].Speed;
                    RainDroplets[i].Transform.Rotation = MathHelper.ToRadians(45f);
                    RainDroplets[i].Transform.Position += RainDroplets[i].Velocity * Time.DeltaTime;

                    if (RainDroplets[i].Transform.Position.Y >= GameSettings.ScreenHeight * .5f + 140)
                    {
                        RainDroplets[i].Splashed = true;
                        RainDroplets[i].Sprite.Texture = Assets.SplashTexture;
                        RainDroplets[i].Transform.Rotation = 0f;
                        RainDroplets[i].Sprite.Colour = new Color(1f, 1f, 1f, (float)Globals.Random.NextDouble() * .5f);
                        RainDroplets[i].Transform.Scale = Vector2.One * 1.5f;
                    }
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
            droplet.Sprite.Colour = new Color(1f, 1f, 1f, (float)Globals.Random.NextDouble() * .1f);
            droplet.Splashed = false;
                
            droplet.Transform.Scale = Vector2.One;
            droplet.Transform.Position = new Vector2(Globals.Random.Next((int)(GameSettings.ScreenWidth), (int)(GameSettings.ScreenWidth * 2)), Globals.Random.Next(-GameSettings.ScreenHeight * 8, -droplet.Sprite.Texture.Height));
            droplet.Speed = (float)Globals.Random.NextDouble() + .5f;
            
            return droplet;
        }
    }
}