﻿using System;
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
            
            Dinosaur.Sprite.Texture = Assets.Dinasaur1Texture;
            Dinosaur.Sprite.Colour = Color.White;
            Dinosaur.Sprite.Centered = true;

            Dinosaur.DiedOnGround = false;
            
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
                    {
                        Dinosaur.IsOnGround = true;

                        if (Globals.GameState == GameStates.Running)
                        {
                            switch (Dinosaur.AnimationTimer)
                            {
                                case 0:
                                    Dinosaur.Sprite.Texture = Assets.Dinasaur1Texture;
                                    break;
                                case 3:
                                    Dinosaur.Sprite.Texture = Assets.Dinasaur2Texture;
                                    break;
                                case 6:
                                    Dinosaur.Sprite.Texture = Assets.Dinasaur1Texture;
                                    break;
                                case 9:
                                    Dinosaur.Sprite.Texture = Assets.Dinasaur3Texture;
                                    break;
                                case 12:
                                    Dinosaur.Sprite.Texture = Assets.Dinasaur1Texture;
                                    break;
                                case 15:
                                    Dinosaur.AnimationTimer = 0;
                                    break;
                            }
                            Dinosaur.AnimationTimer++;
                        }
                    }
                    else
                    {
                        Dinosaur.IsOnGround = false;
                        Dinosaur.Sprite.Texture = Assets.Dinasaur1Texture;
                    }
                    
                    if (Input.IsKeyPressed(Input.KeyMap["player_jump"]) && Dinosaur.IsOnGround)
                        Jump();
                    else if (Input.IsKeyPressed(Input.KeyMap["player_jump"])  && !Dinosaur.IsOnGround)
                    {
                        Dinosaur.HasJumpedBeforeLanding = true;
                        Dinosaur.JumpTick = 0;
                    }
                    
                    if (Input.IsKeyReleased(Input.KeyMap["player_jump"]) && Dinosaur.Velocity.Y < 0)
                        Dinosaur.Velocity.Y *= .5f;

                    if (Dinosaur.HasJumpedBeforeLanding && Dinosaur.JumpTick <= 10 && Dinosaur.IsOnGround)
                    {
                        Jump();
                        Dinosaur.JumpTick = 0;
                        Dinosaur.HasJumpedBeforeLanding = false;
                    }
                    
                    if (Dinosaur.HasJumpedBeforeLanding)
                        Dinosaur.JumpTick++;

                    if (Dinosaur.Transform.Position.Y == Dinosaur.Sprite.Texture.Height * Dinosaur.Transform.Scale.Y * .5f)
                        Dinosaur.Velocity.Y = Dinosaur.Gravity;
            
                    Dinosaur.Velocity.Y += Dinosaur.Gravity;
            
                    Dinosaur.Transform.Position += Dinosaur.Velocity * Time.DeltaTime;

                    Dinosaur.Transform.Position.Y = Math.Clamp(
                        Dinosaur.Transform.Position.Y,
                        (Dinosaur.Sprite.Texture.Height * Dinosaur.Transform.Scale.Y) * .5f,
                        ((GameSettings.ScreenHeight * .5f) + 150) - (Dinosaur.Sprite.Texture.Height * Dinosaur.Transform.Scale.Y) * .5f
                    );

                    Dinosaur.Transform.Position.X =
                        MathHelper.Clamp(Dinosaur.Transform.Position.X, 0, GameSettings.ScreenWidth);
                    break;
                case DinosaurState.Dead:
                    if (Dinosaur.Transform.Position.Y == GameSettings.ScreenHeight * .5f + 150 -
                        Dinosaur.Sprite.Texture.Height * Dinosaur.Transform.Scale.Y * .5f)
                        Dinosaur.IsOnGround = true;
                    else
                        Dinosaur.IsOnGround = false;
                    
                    switch (Dinosaur.DeathTimer)
                    {
                        case 12:
                            Dinosaur.DeathTimer = 0;
                            break;
                        case 6:
                            Dinosaur.Sprite.Colour = Color.Black;
                            break;
                        case 3:
                            Dinosaur.Sprite.Colour = Color.DarkRed;
                            break;
                    }
                    
                    Dinosaur.Transform.Rotation += MathHelper.ToRadians(20) * Dinosaur.Velocity.X;

                    if (Dinosaur.IsOnGround && !Dinosaur.DiedOnGround)
                    {
                        Dinosaur.Velocity.X *= .75f;
                        Dinosaur.Velocity.Y *= -.85f;
                    }
                    else if (Dinosaur.IsOnGround)
                        Dinosaur.Velocity.X *= .98f;
                    
                    Dinosaur.Velocity.Y += Dinosaur.Gravity;
            
                    Dinosaur.Transform.Position += Dinosaur.Velocity * Time.DeltaTime;
                    
                    Dinosaur.Transform.Position.Y = Math.Clamp(
                        Dinosaur.Transform.Position.Y,
                        (Dinosaur.Sprite.Texture.Height * Dinosaur.Transform.Scale.Y) * .5f,
                        ((GameSettings.ScreenHeight * .5f) + 150) - (Dinosaur.Sprite.Texture.Height * Dinosaur.Transform.Scale.Y) * .5f
                    );
                    
                    Dinosaur.DeathTimer++;
                    break;
            }
        }

        public static void Kill()
        {
            Dinosaur.Sprite.Texture = Assets.Dinasaur1Texture;
            Dinosaur.Velocity.X += Functions.Map(Dinosaur.Velocity.X, 0, Constants.MaxSpeed, .5f, 100f);

            if (Dinosaur.IsOnGround)
            {
                Dinosaur.DiedOnGround = true;
                Dinosaur.Velocity.Y *= 1.25f;
            }
        }

        public static void Jump()
        {
            Dinosaur.Velocity.Y = Dinosaur.JumpHeight;
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