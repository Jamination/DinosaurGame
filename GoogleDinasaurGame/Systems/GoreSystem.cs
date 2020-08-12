using System;
using GoogleDinasaurGame.Components;
using Microsoft.Xna.Framework;

namespace GoogleDinasaurGame.Systems
{
    public static class GoreSystem
    {
        public static Blood[] Gore = new Blood[10000];

        public static float BloodAmount = 0f;

        public static void Load()
        {
            for (int i = 0; i < Gore.Length; i++)
            {
                NewBlood(ref Gore[i]);
            }
        }

        public static void Update()
        {
            for (int i = 0; i < (int)BloodAmount; i++)
            {
                if (!Gore[i].PositionSet)
                {
                    Gore[i].Transform.Position = DinosaurSystem.Dinosaur.Transform.Position;
                    Gore[i].PositionSet = true;
                }
                
                Gore[i].Velocity.Y += Blood.Gravity;

                if (Gore[i].Transform.Position.Y >= GameSettings.ScreenHeight * .5f + 140 + Gore[i].YMargin)
                {
                    Gore[i].Velocity = new Vector2(Gore[i].Velocity.X * .75f, -Gore[i].Velocity.Y * .25f);
                }
                
                Gore[i].Transform.Position += Gore[i].Velocity * Time.DeltaTime;

                Gore[i].LifeTime++;

                if (Gore[i].LifeTime >= Globals.Random.Next(120, 240))
                    Gore[i].Disappearing = true;

                if (Gore[i].Disappearing)
                {
                    Gore[i].Sprite.Colour = Color.Lerp(Gore[i].Sprite.Colour, new Color(1f, 1f, 1f, 0f), .1f);
                    if (Gore[i].Sprite.Colour.A < .01f)
                        NewBlood(ref Gore[i]);
                }
            }

            BloodAmount += 1f;
            BloodAmount = MathHelper.Clamp(BloodAmount, 0, Gore.Length);
        }

        public static void Draw()
        {
            for (int i = 0; i < (int)BloodAmount; i++)
                Functions.DrawSprite(ref Gore[i].Sprite, ref Gore[i].Transform);
        }

        public static void NewBlood(ref Blood blood)
        {
            blood = new Blood();
            
            blood.Sprite.Texture = Assets.BloodTexture;
            float lightness = (float) Globals.Random.NextDouble() + .5f;
            blood.Sprite.Colour = new Color(lightness, lightness, lightness, 1f);
            blood.Sprite.Centered = true;
            
            blood.PositionSet = false;
            
            blood.Transform.Scale = Vector2.One * 2;
            blood.Transform.Position = DinosaurSystem.Dinosaur.Transform.Position;
            
            blood.Velocity = new Vector2((float)Globals.Random.NextDouble() - .75f, -(float)Globals.Random.NextDouble() * .75f);
            blood.YMargin = Globals.Random.Next(0, 32);
        }
    }
}