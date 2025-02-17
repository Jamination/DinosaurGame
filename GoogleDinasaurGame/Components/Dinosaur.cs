﻿using Microsoft.Xna.Framework;

namespace GoogleDinasaurGame.Components
{
    public struct Dinosaur
    {
        public Transform Transform;
        public Sprite Sprite;
        public Hitbox Hitbox;

        public Vector2 Velocity;

        public int JumpTick;
        public bool HasJumpedBeforeLanding;
        
        public int AnimationTimer;

        public bool IsOnGround, DiedOnGround;
        
        public const float Gravity = .04f;
        public const float JumpHeight = -1f;

        public Color ColourToLerpTo;
    }
}