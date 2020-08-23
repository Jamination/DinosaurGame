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

            Globals.Graphics.PreferredBackBufferWidth = GameSettings.ScreenWidth;
            Globals.Graphics.PreferredBackBufferHeight = GameSettings.ScreenHeight;
            Globals.Graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Globals.Graphics.PreferMultiSampling = true;
            Globals.Graphics.SynchronizeWithVerticalRetrace = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            Window.Title = "Google Dinosaur Game By James Heasman";
            Mouse.SetCursor(MouseCursor.FromTexture2D(Assets.CursorTexture, 0, 0));
            
            IsMouseVisible = false;
            IsFixedTimeStep = true;
            
            Globals.Graphics.PreferredBackBufferWidth = GameSettings.ScreenWidth;
            Globals.Graphics.PreferredBackBufferHeight = GameSettings.ScreenHeight;
            Globals.Graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Globals.Graphics.PreferMultiSampling = true;
            Globals.Graphics.SynchronizeWithVerticalRetrace = true;
            
            Globals.Graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);
            
            Assets.Load();
            CactusSystem.Load();
            DinosaurSystem.Load();
            GroundSystem.Load();
            CloudSystem.Load();
            ReplayButtonSystem.Load();
            RainSystem.Load();
            GoreSystem.Load();
            LightningSystem.Load();
            
            Functions.PlaySound(Sounds.BackgroundMusic);
            Functions.PlaySound(Sounds.Rain);
        }

        protected override void Update(GameTime gameTime)
        {
            Input.UpdateState();
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Input.IsKeyPressed(Input.KeyMap["quit"]))
                Exit();

            Time.GameTime = gameTime;
            Time.DeltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            switch (Globals.GameState)
            {
                case GameStates.BeforeStart:
                    IsMouseVisible = false;
                    DinosaurSystem.Update();
                    CloudSystem.Update();
                    break;
                case GameStates.Running:
                    IsMouseVisible = false;
                    DinosaurSystem.Update();
                    CloudSystem.Update();
                    CactusSystem.Update();
                    RainSystem.Update();
                    GroundSystem.Update();
                    ScoreSystem.Update();
                    LightningSystem.Update();
                    Globals.Speed = MathHelper.Lerp(Globals.Speed, Constants.MaxSpeed, .0001f);
                    break;
                case GameStates.GameOver:
                    IsMouseVisible = true;
                    CloudSystem.Update();
                    DinosaurSystem.Update();
                    RainSystem.Update();
                    ReplayButtonSystem.Update();
                    GoreSystem.Update();
                    LightningSystem.Update();

                    if (Input.IsKeyPressed(Input.KeyMap["restart"]))
                        Functions.RestartGame();
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (LightningSystem.State != LightningState.Flash)
            {
                GraphicsDevice.Clear(new Color(0f, .05f, .1f, 1f));
                
                            Globals.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            switch (Globals.GameState)
            {
                case GameStates.BeforeStart:
                    CloudSystem.Draw();
                    GroundSystem.Draw();
                    CactusSystem.Draw();
                    DinosaurSystem.Draw();
                    ScoreSystem.Draw();
                    
                    Globals.SpriteBatch.DrawString(
                        Assets.ScoreFont,
                        "Jump to start",
                        new Vector2(GameSettings.ScreenWidth * .5f, GameSettings.ScreenHeight * .5f),
                        Color.White,
                        0f,
                        new Vector2(Assets.ScoreFont.MeasureString(("Jump to start").ToString()).X * .5f, Assets.ScoreFont.MeasureString(("Jump to start").ToString()).Y * .5f),
                        new Vector2((float)Math.Sin(Globals.TextTick), (float)Math.Sin(Globals.TextTick)) * .2f + (Vector2.One * 2), 
                        SpriteEffects.None,
                        0f
                    );
                    Globals.TextTick += .01f;
                    break;
                case GameStates.Running:
                    CloudSystem.Draw();
                    GroundSystem.Draw();
                    CactusSystem.Draw();
                    LightningSystem.Draw();
                    DinosaurSystem.Draw();
                    RainSystem.Draw();
                    ScoreSystem.Draw(); 
                    break;
                case GameStates.GameOver:
                    CloudSystem.Draw();
                    GroundSystem.Draw();
                    CactusSystem.Draw();
                    GoreSystem.Draw();
                    LightningSystem.Draw();
                    DinosaurSystem.Draw();
                    RainSystem.Draw();
                    ScoreSystem.Draw();
                    ReplayButtonSystem.Draw();
                    break;
            }
            
            Globals.SpriteBatch.End();
            }
            else
                Globals.Graphics.GraphicsDevice.Clear(Color.White);
            
            base.Draw(gameTime);
        }
    }
}