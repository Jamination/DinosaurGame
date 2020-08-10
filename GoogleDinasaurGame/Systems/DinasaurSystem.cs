using System;
using GoogleDinasaurGame.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GoogleDinasaurGame.Systems
{
    public static class DinasaurSystem
    {
        public static Dinosaur Dinosaur;

        public static void Load()
        {
            Dinosaur = new Dinosaur();
            
            Dinosaur.Sprite.Texture = Assets.DinasaurTexture;
            Dinosaur.Sprite.Colour = Color.White;
            Dinosaur.Sprite.Centered = true;
            
            Dinosaur.Hitbox.AABB = new Rectangle(0, 0, Dinosaur.Sprite.Texture.Width, Dinosaur.Sprite.Texture.Height);
            
            Dinosaur.Transform.Position = new Vector2(Dinosaur.Sprite.Texture.Width * 3, ((GameSettings.ScreenHeight * .5f) + 150) - (Dinosaur.Sprite.Texture.Height * Dinosaur.Transform.Scale.Y) * .5f);
            Dinosaur.Transform.Scale = Vector2.One * 2;
        }

        public static void Update()
        {
            if (Input.IsKeyPressed(Input.KeyMap["player_jump"]) && Dinosaur.Transform.Position.Y ==
                ((GameSettings.ScreenHeight * .5f) + 150) -
                (Dinosaur.Sprite.Texture.Height * Dinosaur.Transform.Scale.Y) * .5f)
            {
                Dinosaur.VelY = -Dinosaur.JumpHeight;
                if (!Globals.GameStarted)
                    Globals.GameStarted = true;
            }
            else if (Input.IsKeyReleased(Input.KeyMap["player_jump"]) && Dinosaur.VelY < 0)
                Dinosaur.VelY *= .5f;

            if (Dinosaur.Transform.Position.Y == GameSettings.ScreenHeight * .5f + 150 -
                Dinosaur.Sprite.Texture.Height * Dinosaur.Transform.Scale.Y * .5f)
                Dinosaur.IsOnGround = true;
            else
                Dinosaur.IsOnGround = false;

            if (Dinosaur.Transform.Position.Y == Dinosaur.Sprite.Texture.Height * Dinosaur.Transform.Scale.Y * .5f)
                Dinosaur.VelY = Dinosaur.Gravity;
            
            Dinosaur.VelY += Dinosaur.Gravity;
            
            Dinosaur.Transform.Position.Y += Dinosaur.VelY * Time.DeltaTime;

            Dinosaur.Transform.Position.Y = Math.Clamp(
                Dinosaur.Transform.Position.Y,
                (Dinosaur.Sprite.Texture.Height * Dinosaur.Transform.Scale.Y) * .5f,
                ((GameSettings.ScreenHeight * .5f) + 150) - (Dinosaur.Sprite.Texture.Height * Dinosaur.Transform.Scale.Y) * .5f
            );
        }

        public static void Draw()
        {
            Functions.DrawSprite(ref Dinosaur.Sprite, ref Dinosaur.Transform);
        }
    }
}