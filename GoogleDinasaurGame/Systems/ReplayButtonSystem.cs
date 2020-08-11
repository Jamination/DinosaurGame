using System;
using GoogleDinasaurGame.Components;
using Microsoft.Xna.Framework;

namespace GoogleDinasaurGame.Systems
{
    public static class ReplayButtonSystem
    {
        public static ReplayButton ReplayButton;

        public static bool HoveringOverButton = false;

        public static void Load()
        {
            ReplayButton = new ReplayButton();
            
            ReplayButton.ReplaySprite.Texture = Assets.ReplayButtonTexture;
            ReplayButton.ReplaySprite.Colour = Color.Gray;
            ReplayButton.ReplaySprite.Centered = true;

            ReplayButton.ReplayTransform.Position = new Vector2(GameSettings.ScreenWidth * .5f, GameSettings.ScreenHeight * .5f + 50);
            ReplayButton.ReplayTransform.Scale = Vector2.One * 4;
            
            ReplayButton.ReplayHitbox.AABB = new Rectangle(0, 0,
                ReplayButton.ReplaySprite.Texture.Width * (int)ReplayButton.ReplayTransform.Scale.X,
                ReplayButton.ReplaySprite.Texture.Height * (int)ReplayButton.ReplayTransform.Scale.Y
            );
            
            ReplayButton.GameOverSprite.Texture = Assets.GameOverTextTexture;
            ReplayButton.GameOverSprite.Colour = Color.White;
            ReplayButton.GameOverSprite.Centered = true;
            
            ReplayButton.GameOverTransform.Scale = Vector2.One;
            ReplayButton.GameOverTransform.Position = new Vector2(GameSettings.ScreenWidth * .5f, GameSettings.ScreenHeight * .5f - 100);
            ReplayButton.GameOverTransform.Scale = Vector2.One * 4;
        }

        public static void Update()
        {
            ReplayButton.ReplayTransform.Scale = Vector2.Lerp(ReplayButton.ReplayTransform.Scale, Vector2.One * 4, .25f);
            if (new Rectangle(
                ReplayButton.ReplayTransform.Position.ToPoint() -
                (ReplayButton.ReplayHitbox.AABB.Size.ToVector2() * .5f).ToPoint(),
                ReplayButton.ReplayHitbox.AABB.Size).Contains(Input.CurrentMouseState.Position))
            {
                if (!HoveringOverButton)
                {
                    HoveringOverButton = true;
                    Functions.PlaySound(Sounds.ButtonHover);
                }
                ReplayButton.ReplaySprite.Colour = Color.Lerp(ReplayButton.ReplaySprite.Colour, Color.White, .25f);
                ReplayButton.ReplayTransform.Scale = Vector2.Lerp(ReplayButton.ReplayTransform.Scale, Vector2.One * 4.5f, .25f);
                if (Input.IsLeftMouseDown())
                {
                    ReplayButton.ReplayTransform.Scale = Vector2.Lerp(ReplayButton.ReplayTransform.Scale, Vector2.One * 3, .25f);
                    ReplayButton.ReplaySprite.Colour = Color.Lerp(ReplayButton.ReplaySprite.Colour, Color.Blue, .25f);
                }
                else if (Input.IsLeftMouseReleased())
                {
                    Functions.RestartGame();
                    ReplayButton.ReplayTransform.Scale = Vector2.One * 4f;
                }
            }
            else
            {
                ReplayButton.ReplaySprite.Colour = Color.Lerp(ReplayButton.ReplaySprite.Colour, Color.Gray, .25f);
                HoveringOverButton = false;
            }

            ReplayButton.GameOverTransform.Rotation = (float)Math.Sin(ReplayButton.AnimationTick) * .1f;
            ReplayButton.GameOverTransform.Scale = new Vector2(((float)Math.Cos(ReplayButton.AnimationTick)) * .5f) + new Vector2(4f);
            ReplayButton.AnimationTick += .025f;
        }

        public static void Draw()
        {
            Functions.DrawSprite(ref ReplayButton.ReplaySprite, ref ReplayButton.ReplayTransform);
            Functions.DrawSprite(ref ReplayButton.GameOverSprite, ref ReplayButton.GameOverTransform);
        }
    }
}