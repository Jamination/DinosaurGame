using System;
using System.Collections.Generic;
using GoogleDinasaurGame.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GoogleDinasaurGame.Systems
{
    public static class CactusSystem
    {
        public static Cactus[] Cacti = new Cactus[5];
        
        public static void Load()
        {
            for (int i = 0; i < Cacti.Length; i++)
            {
                Cacti[i] = new Cactus();
            
                Cacti[i].Sprite.Texture = Assets.CactusTexture;
                Cacti[i].Sprite.Colour = Color.White;
                Cacti[i].Sprite.Centered = true;
                Cacti[i].Sprite.Effects = Functions.Choose(SpriteEffects.None, SpriteEffects.FlipHorizontally);
                
                Cacti[i].Hitbox.AABB = new Rectangle(0, 0, Cacti[i].Sprite.Texture.Width, Cacti[i].Sprite.Texture.Height);
            
                Cacti[i].Transform.Scale = Vector2.One * 2;
                Cacti[i].Transform.Position = new Vector2(
                    GameSettings.ScreenWidth + (Cacti[i].Sprite.Texture.Width * Cacti[i].Transform.Scale.X) * .5f + Globals.Random.Next(0, GameSettings.ScreenWidth * 4),
                    (GameSettings.ScreenHeight * .5f) + 105
                );
            }
        }

        public static void Update()
        {
            for (int i = 0; i < Cacti.Length; i++)
            {
                Cacti[i].Transform.Position.X -= Globals.Speed;

                if (Cacti[i].Transform.Position.X <=
                    (-Cacti[i].Sprite.Texture.Width * Cacti[i].Transform.Scale.X) * .5f)
                {
                    Cacti[i].Transform.Position = new Vector2(
                        GameSettings.ScreenWidth + (Cacti[i].Sprite.Texture.Width * Cacti[i].Transform.Scale.X) * .5f + Globals.Random.Next(0, GameSettings.ScreenWidth * 4),
                        (GameSettings.ScreenHeight * .5f) + 105
                    );
                }
                
                var cactiAABB = new Rectangle(Cacti[i].Transform.Position.ToPoint(), Cacti[i].Hitbox.AABB.Size * Cacti[i].Transform.Scale.ToPoint());
                var dinasaurAABB = new Rectangle(DinosaurSystem.Dinosaur.Transform.Position.ToPoint(), DinosaurSystem.Dinosaur.Hitbox.AABB.Size);

                if (cactiAABB.Intersects(dinasaurAABB) && Globals.GameState == GameStates.Running)
                {
                    Globals.GameState = GameStates.GameOver;
                    Functions.PlaySound(Sounds.Death);
                    DinosaurSystem.State = DinosaurState.Dead;
                }
            }
        }

        public static void Draw()
        {
            for (int i = 0; i < Cacti.Length; i++)
                Functions.DrawSprite(ref Cacti[i].Sprite, ref Cacti[i].Transform);
        }
    }
}