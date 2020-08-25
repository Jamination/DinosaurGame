using System;
using GoogleDinasaurGame.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GoogleDinasaurGame.Systems
{
    public static class RainSystem
    {
        public static Droplet[] RainDroplets = new Droplet[20000];

        public static float DropletAmount = 0f;

        public static void Load()
        {
            for (int i = 0; i < RainDroplets.Length; i++)
            {
                NewDroplet(ref RainDroplets[i]);
            }
        }

        public static void Update()
        {
            for (int i = 0; i < (int)DropletAmount; i++)
            {
                if (RainDroplets[i].Splashed)
                {
                    RainDroplets[i].SplashTimer++;
                    
                    if (RainDroplets[i].SplashTimer >= 10)
                        NewDroplet(ref RainDroplets[i]);

                    if (Globals.GameState == GameStates.Running)
                        RainDroplets[i].Transform.Position.X -= Globals.Speed;
                }
                else
                {
                    RainDroplets[i].Velocity.X -= Droplet.Gravity * RainDroplets[i].Speed;
                    RainDroplets[i].Velocity.Y += Droplet.Gravity * RainDroplets[i].Speed;
                    RainDroplets[i].Transform.Rotation = MathHelper.ToRadians(45f);
                    RainDroplets[i].Transform.Position += RainDroplets[i].Velocity * Time.DeltaTime;
                    
                    var dropletAABB = new Rectangle(RainDroplets[i].Transform.Position.ToPoint(), RainDroplets[i].Hitbox.AABB.Size * RainDroplets[i].Transform.Scale.ToPoint());
                    var dinasaurAABB = new Rectangle(DinosaurSystem.Dinosaur.Transform.Position.ToPoint(), DinosaurSystem.Dinosaur.Sprite.Texture.Bounds.Size * DinosaurSystem.Dinosaur.Transform.Scale.ToPoint());

                    for (int j = 0; j < CactusSystem.Cacti.Length; j++)
                    {
                        if (!CactusSystem.Cacti[j].Active)
                            continue;
                        
                        var cactusAABB = new Rectangle(CactusSystem.Cacti[j].Transform.Position.ToPoint(), CactusSystem.Cacti[j].Sprite.Texture.Bounds.Size * CactusSystem.Cacti[j].Transform.Scale.ToPoint());

                        if (dropletAABB.Intersects(cactusAABB))
                            Splash(ref RainDroplets[i]);
                    }

                    if (RainDroplets[i].Transform.Position.Y >= GameSettings.ScreenHeight * .5f + 140 || dropletAABB.Intersects(dinasaurAABB))
                    {
                        Splash(ref RainDroplets[i]);
                    }
                }
            }

            DropletAmount = MathHelper.Lerp(DropletAmount, RainDroplets.Length, .0002f);
            Assets.RainSounds.Volume = MathHelper.Lerp(Assets.RainSounds.Volume, .15f, .0002f);
        }

        public static void Draw()
        {
            for (int i = 0; i < DropletAmount; i++)
            {
                Functions.Draw(ref RainDroplets[i].Sprite, ref RainDroplets[i].Transform);
            }
        }

        public static void NewDroplet(ref Droplet droplet)
        {
            droplet.Sprite.Texture = Assets.DropletTexture;
            droplet.Sprite.Colour = new Color(1f, 1f, 1f, (float)Globals.Random.NextDouble() * .15f);
            droplet.Splashed = false;
            droplet.SplashTimer = 0;
            droplet.Velocity = Vector2.Zero;
            
            droplet.Hitbox.AABB = new Rectangle(0, 0, 12, 12);
                
            droplet.Transform.Scale = Vector2.One;
            droplet.Transform.Position = new Vector2(Globals.Random.Next((int)(GameSettings.ScreenWidth), (int)(GameSettings.ScreenWidth * 2)), Globals.Random.Next(-GameSettings.ScreenHeight * 8, -droplet.Sprite.Texture.Height));
            droplet.Speed = (float)Globals.Random.NextDouble() + .5f;
        }

        public static void Splash(ref Droplet droplet)
        {
            droplet.Splashed = true;
            droplet.Sprite.Texture = Assets.SplashTexture;
            droplet.Sprite.Effects = Functions.Choose(SpriteEffects.None, SpriteEffects.FlipHorizontally);
            droplet.Sprite.Colour = new Color(1f, 1f, 1f, (float)Globals.Random.NextDouble() * .7f);
            droplet.Velocity = Vector2.Zero;
            droplet.Sprite.Colour = Color.Lerp(droplet.Sprite.Colour,
                new Color(1f, 1f, 1f, 0f), .75f);
        }
    }
}