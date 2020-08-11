using Microsoft.Xna.Framework;

namespace GoogleDinasaurGame.Components
{
    public struct Blood
    {
        public Sprite Sprite;
        public Transform Transform;
        public Vector2 Velocity;

        public int LifeTime;
        public int YMargin;

        public const float Gravity = .02f;
        
        public bool PositionSet;
        public bool Disappearing;
    }
}