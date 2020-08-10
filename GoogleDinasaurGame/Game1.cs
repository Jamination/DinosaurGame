using System;
using GoogleDinasaurGame.Components;
using GoogleDinasaurGame.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GoogleDinasaurGame
{
    public class Game1 : Game
    {
        public Game1()
        {
            Globals.Window = Window;
            Globals.Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Globals.Content = Content;
            
            IsMouseVisible = true;
            IsFixedTimeStep = true;

            Globals.Graphics.PreferredBackBufferWidth = GameSettings.ScreenWidth;
            Globals.Graphics.PreferredBackBufferHeight = GameSettings.ScreenHeight;
            Globals.Graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Globals.Graphics.PreferMultiSampling = true;
            Globals.Graphics.SynchronizeWithVerticalRetrace = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            Window.Title = "Google Dinasaur Game By James Heasman";
        }

        protected override void LoadContent()
        {
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);
            
            Assets.Load();
            CactusSystem.Load();
            DinasaurSystem.Load();
            GroundSystem.Load();
            CloudSystem.Load();
        }

        protected override void Update(GameTime gameTime)
        {
            Input.UpdateState();
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Input.IsKeyPressed(Input.KeyMap["quit"]))
                Exit();

            Time.GameTime = gameTime;
            Time.DeltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            
            DinasaurSystem.Update();
            CloudSystem.Update();
            
            if (Globals.GameStarted)
            {
                CactusSystem.Update();
                GroundSystem.Update();
                Globals.Speed = MathHelper.Lerp(Globals.Speed, Constants.MaxSpeed, .0001f);
            }

            if (Globals.GameStarted)
                Globals.Score += .5f;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(0f, .05f, .1f, 1f));
            
            Globals.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            
            CloudSystem.Draw();
            GroundSystem.Draw();
            CactusSystem.Draw();
            DinasaurSystem.Draw();
            
            Globals.SpriteBatch.DrawString(
                Assets.ScoreFont,
                ((int)Globals.Score).ToString(),
                new Vector2(GameSettings.ScreenWidth * .5f, GameSettings.ScreenHeight * .025f),
                Color.White,
                0f,
                new Vector2(Assets.ScoreFont.MeasureString(((int)Globals.Score).ToString()).X * .5f, 0),
                Vector2.One,
                SpriteEffects.None,
                0f
            );
            
            Globals.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}