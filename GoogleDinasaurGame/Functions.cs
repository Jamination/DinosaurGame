﻿using System.Linq;
using GoogleDinasaurGame.Components;
using GoogleDinasaurGame.Systems;
using Microsoft.Xna.Framework;

namespace GoogleDinasaurGame
{
    public static class Functions
    {
        public static void DrawSprite(ref Sprite sprite, ref Transform transform)
        {
            var centerOrigin = Vector2.Zero;
            
            if (sprite.Centered)
                centerOrigin = sprite.Texture.Bounds.Size.ToVector2() * .5f;
            
            Globals.SpriteBatch.Draw(
                sprite.Texture,
                new Vector2((int)transform.Position.X, (int)transform.Position.Y),
                sprite.SourceRect,
                sprite.Colour,
                transform.Rotation,
                centerOrigin + sprite.Origin,
                transform.Scale,
                sprite.Effects,
                sprite.LayerDepth
            );
        }
        
        public static T Choose<T>(params T[] list) => list[Globals.Random.Next(0, list.ToArray().Length)];

        public static void RestartGame()
        {
            DinasaurSystem.Load();
            GroundSystem.Load();
            CloudSystem.Load();
            CactusSystem.Load();
            Globals.Speed = Constants.MinSpeed;
            Globals.GameStarted = false;
            Globals.Score = 0;
        }
    }
}