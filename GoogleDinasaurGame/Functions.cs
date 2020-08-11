using System.Linq;
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
        
        public static float Map(float value, float fromLow, float fromHigh, float toLow, float toHigh) => (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
        
        public static T Choose<T>(params T[] list) => list[Globals.Random.Next(0, list.ToArray().Length)];

        public static void RestartGame()
        {
            Functions.PlaySound(Sounds.Restart);
            Globals.Speed = Constants.MinSpeed;
            ScoreSystem.Score = 0;
            Globals.GameState = GameStates.BeforeStart;
            DinosaurSystem.State = DinosaurState.Alive;
            RainSystem.DropletAmount = 0;
            DinosaurSystem.Load();
            GroundSystem.Load();
            CloudSystem.Load();
            CactusSystem.Load();
            RainSystem.Load();
        }

        public static void PlaySound(Sounds sound)
        {
            switch (sound)
            {
                case Sounds.Jump:
                    Assets.JumpSound.Pitch = (float)Globals.Random.NextDouble() - .5f;
                    Assets.JumpSound.Play();
                    break;
                case Sounds.Death:
                    Assets.DeathSound.Play();
                    break;
                case Sounds.Restart:
                    Assets.RestartSound.Play();
                    break;
                case Sounds.ScoreBonus:
                    Assets.ScoreBonusSound.Play();
                    break;
                case Sounds.ButtonHover:
                    Assets.ButtonHoverSound.Play();
                    break;
                case Sounds.BackgroundMusic:
                    Assets.BackgroundMusic.Play();
                    break;
                case Sounds.Rain:
                    Assets.RainSounds.Play();
                    break;
            }
        }
    }
}