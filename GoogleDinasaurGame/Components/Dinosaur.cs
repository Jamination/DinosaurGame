namespace GoogleDinasaurGame.Components
{
    public struct Dinosaur
    {
        public Transform Transform;
        public Sprite Sprite;
        public Hitbox Hitbox;
        
        public float VelY;
        
        public int DeathTimer;

        public bool IsOnGround;

        public const float Gravity = .04f;
        public const float JumpHeight = -1f;
    }
}