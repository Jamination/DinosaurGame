using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace GoogleDinasaurGame
{
    public static class Assets
    {
        public static Texture2D DinasaurTexture, CactusTexture, GroundTexture, CloudTexture, ReplayButtonTexture, GameOverTextTexture, DropletTexture;
        
        public static SpriteFont ScoreFont;

        public static SoundEffectInstance JumpSound, DeathSound, RestartSound, ScoreBonusSound, ButtonHover;

        public static void Load()
        {
            DinasaurTexture = Globals.Content.Load<Texture2D>("Sprites/Dinasaur");
            CactusTexture = Globals.Content.Load<Texture2D>("Sprites/Cactus");
            GroundTexture = Globals.Content.Load<Texture2D>("Sprites/Ground");
            CloudTexture = Globals.Content.Load<Texture2D>("Sprites/Cloud");
            ReplayButtonTexture = Globals.Content.Load<Texture2D>("Sprites/Replay Button");
            GameOverTextTexture = Globals.Content.Load<Texture2D>("Sprites/GameOver Text");
            DropletTexture = Globals.Content.Load<Texture2D>("Sprites/Droplet");

            ScoreFont = Globals.Content.Load<SpriteFont>("Fonts/ScoreFont");

            JumpSound = Globals.Content.Load<SoundEffect>("Sounds/Jump").CreateInstance();
            JumpSound.Volume = .1f;
            
            DeathSound = Globals.Content.Load<SoundEffect>("Sounds/Death").CreateInstance();
            DeathSound.Volume = .5f;
            
            RestartSound = Globals.Content.Load<SoundEffect>("Sounds/Restart").CreateInstance();
            RestartSound.Volume = .5f;
            
            ScoreBonusSound = Globals.Content.Load<SoundEffect>("Sounds/ScoreBonus").CreateInstance();
            ScoreBonusSound.Volume = .5f;
            
            ButtonHover = Globals.Content.Load<SoundEffect>("Sounds/ButtonHover").CreateInstance();
            ButtonHover.Volume = .5f;
        }
    }
}