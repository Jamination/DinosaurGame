using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GoogleDinasaurGame
{
    public static class Globals
    {
        public static GraphicsDeviceManager Graphics;
        public static SpriteBatch SpriteBatch;
        public static ContentManager Content;
        public static GameWindow Window;

        public static float Speed = Constants.MinSpeed;
        public static bool GameStarted = false;

        public static float Score = 0f;
        
        public static Random Random = new Random();
    }
}