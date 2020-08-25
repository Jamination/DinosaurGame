using GoogleDinasaurGame.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GoogleDinasaurGame.Systems
{
    public static class LightningSystem
    {
        public static LightningStrike Lightning;
        public static LightningState State = LightningState.Docile;
        
        public static void Load()
        {
            NewLightning(ref Lightning);
        }

        public static void Update()
        {
            switch (State)
            {
                case LightningState.Docile:
                    if (Lightning.Timer >= Lightning.StrikeTime)
                        Strike();
                    
                    Lightning.Timer++;
                    break;
                case LightningState.Active:
                    if (Lightning.LifeTime >= 4)
                        State = LightningState.Flash;
                    Lightning.LifeTime++;
                    if (Globals.GameState == GameStates.Running)
                        Lightning.Transform.Position.X -= Globals.Speed;
                    break;
                case LightningState.Flash:
                    if (Lightning.FlashTime >= 4)
                    {
                        State = LightningState.Docile;
                        NewLightning(ref Lightning);
                    }
                    Lightning.FlashTime++;
                    break;
            }
        }

        public static void Draw()
        {
            if (State == LightningState.Active)
                Functions.Draw(ref Lightning.Sprite, ref Lightning.Transform);
        }

        public static void Strike()
        {
            State = LightningState.Active;
            Functions.PlaySound(Sounds.Lightning);
        }

        public static void NewLightning(ref LightningStrike lightningStrike)
        {
            lightningStrike.Sprite.Texture = Functions.Choose(Assets.Lightning1Texture, Assets.Lightning2Texture);
            lightningStrike.Sprite.Effects = Functions.Choose(SpriteEffects.None, SpriteEffects.FlipHorizontally);
            lightningStrike.Sprite.Colour = Color.White;
            lightningStrike.Transform.Scale = Vector2.One * 4f;
            if (Globals.GameState == GameStates.GameOver)
                lightningStrike.Transform.Position = new Vector2(Globals.Random.Next(lightningStrike.Sprite.Texture.Width * 4, GameSettings.ScreenWidth - lightningStrike.Sprite.Texture.Width * 4), 0f);
            else
                lightningStrike.Transform.Position = new Vector2(Globals.Random.Next((lightningStrike.Sprite.Texture.Width * 4) + 100, GameSettings.ScreenWidth - lightningStrike.Sprite.Texture.Width * 4), 0f);

            lightningStrike.Timer = 0;
            lightningStrike.StrikeTime = Globals.Random.Next(960, 2048);
        }
    }
}