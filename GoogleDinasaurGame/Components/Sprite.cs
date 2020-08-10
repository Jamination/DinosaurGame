using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GoogleDinasaurGame.Components
{
    public struct Sprite
    {
        public Texture2D Texture;
        public Rectangle? SourceRect;
        public Color Colour;
        public Vector2 Origin;
        public SpriteEffects Effects;
        public float LayerDepth;
        public bool Centered;
    }
}