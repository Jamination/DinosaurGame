﻿using Microsoft.Xna.Framework;

namespace GoogleDinasaurGame.Components
{
    public struct Droplet
    {
        public Sprite Sprite;
        public Transform Transform;
        public Vector2 Velocity;
        public Hitbox Hitbox;

        public const float Gravity = .02f;
        public float Speed;
        public bool Splashed;
        public int SplashTimer;
    }
}