using Microsoft.Xna.Framework;

namespace GoogleDinasaurGame.Components
{
    public struct Droplet
    {
        public Sprite Sprite;
        public Transform Transform;
        public Vector2 Velocity;

        public const float Gravity = .02f;
    }
}