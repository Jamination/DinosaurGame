using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace GoogleDinasaurGame
{
    public static class Assets
    {
        public static Texture2D DinasaurTexture, CactusTexture, GroundTexture, CloudTexture;
        
        public static SpriteFont ScoreFont;

        public static SoundEffectInstance JumpSound, DeathSound, RestartSound, ScoreBonusSound;

        public static void Load()
        {
            DinasaurTexture = Globals.Content.Load<Texture2D>("Sprites/Dinasaur");
            CactusTexture = Globals.Content.Load<Texture2D>("Sprites/Cactus");
            GroundTexture = Globals.Content.Load<Texture2D>("Sprites/Ground");
            CloudTexture = Globals.Content.Load<Texture2D>("Sprites/Cloud");

            ScoreFont = Globals.Content.Load<SpriteFont>("Fonts/ScoreFont");

            JumpSound = Globals.Content.Load<SoundEffect>("Sounds/Jump").CreateInstance();
            JumpSound.Volume = .1f;
            
            DeathSound = Globals.Content.Load<SoundEffect>("Sounds/Death").CreateInstance();
            DeathSound.Volume = .5f;
            
            RestartSound = Globals.Content.Load<SoundEffect>("Sounds/Restart").CreateInstance();
            RestartSound.Volume = .5f;
            
            ScoreBonusSound = Globals.Content.Load<SoundEffect>("Sounds/ScoreBonus").CreateInstance();
            ScoreBonusSound.Volume = .5f;
        }
    }
}