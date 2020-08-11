using System;
using GoogleDinasaurGame.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GoogleDinasaurGame.Systems
{
    public static class DinosaurSystem
    {
        public static Dinosaur Dinosaur;
        public static DinosaurState State = DinosaurState.Alive;

        public static void Load()
        {
            Dinosaur = new Dinosaur();
            
            Dinosaur.Sprite.Texture = Assets.DinasaurTexture;
            Dinosaur.Sprite.Colour = Color.White;
            Dinosaur.Sprite.Centered = true;
            
            Dinosaur.Hitbox.AABB = new Rectangle(0, 0, Dinosaur.Sprite.Texture.Width, Dinosaur.Sprite.Texture.Height);
            
            Dinosaur.Transform.Position = new Vector2(Dinosaur.Sprite.Texture.Width * 4, ((GameSettings.ScreenHeight * .5f) + 150) - (Dinosaur.Sprite.Texture.Height * Dinosaur.Transform.Scale.Y) * .5f);
            Dinosaur.Transform.Scale = Vector2.One * 2;
        }

        public static void Update()
        {
            switch (State)
            {
                case DinosaurState.Alive:
                    if (Dinosaur.Transform.Position.Y == GameSettings.ScreenHeight * .5f + 150 -
                        Dinosaur.Sprite.Texture.Height * Dinosaur.Transform.Scale.Y * .5f)
                        Dinosaur.IsOnGround = true;
                    else
                        Dinosaur.IsOnGround = false;
                    
                    if (Input.IsKeyPressed(Input.KeyMap["player_jump"]) && Dinosaur.IsOnGround)
                        Jump();
                    else if (Input.IsKeyPressed(Input.KeyMap["player_jump"])  && !Dinosaur.IsOnGround)
                    {
                        Dinosaur.HasJumpedBeforeLanding = true;
                        Dinosaur.JumpTick = 0;
                    }
                    
                    if (Input.IsKeyReleased(Input.KeyMap["player_jump"]) && Dinosaur.VelY < 0)
                        Dinosaur.VelY *= .5f;

                    if (Dinosaur.HasJumpedBeforeLanding && Dinosaur.JumpTick <= 10 && Dinosaur.IsOnGround)
                    {
                        Jump();
                        Dinosaur.JumpTick = 0;
                        Dinosaur.HasJumpedBeforeLanding = false;
                    }
                    
                    if (Dinosaur.HasJumpedBeforeLanding)
                        Dinosaur.JumpTick++;

                    if (Dinosaur.Transform.Position.Y == Dinosaur.Sprite.Texture.Height * Dinosaur.Transform.Scale.Y * .5f)
                        Dinosaur.VelY = Dinosaur.Gravity;
            
                    Dinosaur.VelY += Dinosaur.Gravity;
            
                    Dinosaur.Transform.Position.Y += Dinosaur.VelY * Time.DeltaTime;

                    Dinosaur.Transform.Position.Y = Math.Clamp(
                        Dinosaur.Transform.Position.Y,
                        (Dinosaur.Sprite.Texture.Height * Dinosaur.Transform.Scale.Y) * .5f,
                        ((GameSettings.ScreenHeight * .5f) + 150) - (Dinosaur.Sprite.Texture.Height * Dinosaur.Transform.Scale.Y) * .5f
                    );
                    break;
                case DinosaurState.Dead:
                    switch (Dinosaur.DeathTimer)
                    {
                        case 12:
                            Dinosaur.Transform.Rotation += MathHelper.ToRadians(90);
                            Dinosaur.DeathTimer = 0;
                            break;
                        case 6:
                            Dinosaur.Sprite.Colour = Color.Black;
                            break;
                        case 3:
                            Dinosaur.Sprite.Colour = Color.Red;
                            break;
                    }
                    Dinosaur.DeathTimer++;
                    break;
            }
        }

        public static void Jump()
        {
            Dinosaur.VelY = Dinosaur.JumpHeight;
            if (Globals.GameState == GameStates.BeforeStart)
                Globals.GameState = GameStates.Running;
            Functions.PlaySound(Sounds.Jump);
        }

        public static void Draw()
        {
            Functions.DrawSprite(ref Dinosaur.Sprite, ref Dinosaur.Transform);
        }
    }
}