using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace GoogleDinasaurGame
{
    public static class Assets
    {
        public static Texture2D 
            Dinasaur1Texture,
            Dinasaur2Texture,
            Dinasaur3Texture,
            CactusTexture,
            GroundTexture,
            CloudTexture,
            ReplayButtonTexture,
            GameOverTextTexture,
            DropletTexture,
            SplashTexture,
            BloodTexture,
            CursorTexture,
            Lightning1Texture,
            Lightning2Texture;
        
        public static SpriteFont ScoreFont;

        public static SoundEffectInstance JumpSound, DeathSound, RestartSound, ScoreBonusSound, ButtonHoverSound, BackgroundMusic, RainSounds, LightningStrikeSound;

        public static void Load()
        {
            Dinasaur1Texture = Globals.Content.Load<Texture2D>("Sprites/Dinasaur");
            Dinasaur2Texture = Globals.Content.Load<Texture2D>("Sprites/Dinasaur2");
            Dinasaur3Texture = Globals.Content.Load<Texture2D>("Sprites/Dinasaur3");
            CactusTexture = Globals.Content.Load<Texture2D>("Sprites/Cactus");
            GroundTexture = Globals.Content.Load<Texture2D>("Sprites/Ground");
            CloudTexture = Globals.Content.Load<Texture2D>("Sprites/Cloud");
            ReplayButtonTexture = Globals.Content.Load<Texture2D>("Sprites/Replay Button");
            GameOverTextTexture = Globals.Content.Load<Texture2D>("Sprites/GameOver Text");
            DropletTexture = Globals.Content.Load<Texture2D>("Sprites/Droplet");
            SplashTexture = Globals.Content.Load<Texture2D>("Sprites/Splash");
            BloodTexture = Globals.Content.Load<Texture2D>("Sprites/Blood");
            CursorTexture = Globals.Content.Load<Texture2D>("Sprites/Cursor");
            Lightning1Texture = Globals.Content.Load<Texture2D>("Sprites/Lightning1");
            Lightning2Texture = Globals.Content.Load<Texture2D>("Sprites/Lightning2");

            ScoreFont = Globals.Content.Load<SpriteFont>("Fonts/ScoreFont");

            JumpSound = Globals.Content.Load<SoundEffect>("Sounds/Jump").CreateInstance();
            JumpSound.Volume = .1f;
            
            DeathSound = Globals.Content.Load<SoundEffect>("Sounds/Death").CreateInstance();
            DeathSound.Volume = .5f;
            
            RestartSound = Globals.Content.Load<SoundEffect>("Sounds/Restart").CreateInstance();
            RestartSound.Volume = .5f;
            
            ScoreBonusSound = Globals.Content.Load<SoundEffect>("Sounds/ScoreBonus").CreateInstance();
            ScoreBonusSound.Volume = .5f;
            
            ButtonHoverSound = Globals.Content.Load<SoundEffect>("Sounds/ButtonHover").CreateInstance();
            ButtonHoverSound.Volume = .5f;
            
            LightningStrikeSound = Globals.Content.Load<SoundEffect>("Sounds/LightningStrike").CreateInstance();
            LightningStrikeSound.Volume = .5f;
            
            BackgroundMusic = Globals.Content.Load<SoundEffect>("Sounds/BackgroundMusic1").CreateInstance();
            BackgroundMusic.Volume = .3f;
            BackgroundMusic.IsLooped = true;
            
            RainSounds = Globals.Content.Load<SoundEffect>("Sounds/RainSounds").CreateInstance();
            RainSounds.Volume = 0f;
            RainSounds.IsLooped = true;
        }
    }
}