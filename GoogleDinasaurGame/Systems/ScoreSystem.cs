using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GoogleDinasaurGame.Systems
{
    public static class ScoreSystem
    {
        public static float Score = 0f;
        public static float LastScore = 0f;
        public static float TextScale = 1f;

        public static Color TextColour = Color.White;

        public static void Update()
        {
            Score += .1f;

            TextScale = MathHelper.Lerp(TextScale, 1f, .1f);
            TextColour = Color.Lerp(TextColour, Color.White, .1f);
            
            if ((int)Score == Math.Floor(Score / 50) * 50 && Score > 5 && Score != LastScore)
            {
                LastScore = Score;
                TextScale = 1.5f;
                TextColour = Color.Gold;
                Functions.PlaySound(Sounds.ScoreBonus);
            }
        }

        public static void Draw()
        {
            Globals.SpriteBatch.DrawString(
                Assets.ScoreFont,
                ((int)Score).ToString(),
                new Vector2(GameSettings.ScreenWidth * .5f, GameSettings.ScreenHeight * .05f),
                TextColour,
                0f,
                new Vector2(Assets.ScoreFont.MeasureString(((int)Score).ToString()).X * .5f, Assets.ScoreFont.MeasureString(((int)Score).ToString()).Y * .5f),
                new Vector2(TextScale), 
                SpriteEffects.None,
                0f
            );
        }
    }
}