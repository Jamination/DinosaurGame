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
            
            IsMouseVisible = false;
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
            Window.Title = "Google Dinosaur Game By James Heasman";
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
                    GroundSystem.Update();
                    ScoreSystem.Update();
                    Globals.Speed = MathHelper.Lerp(Globals.Speed, Constants.MaxSpeed, .0001f);
                    break;
                case GameStates.GameOver:
                    IsMouseVisible = true;
                    CloudSystem.Update();
                    DinosaurSystem.Update();
                    ReplayButtonSystem.Update();

                    if (Input.IsKeyPressed(Input.KeyMap["restart"]))
                        Functions.RestartGame();
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(0f, .05f, .1f, 1f));
            
            Globals.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            
            CloudSystem.Draw();
            GroundSystem.Draw();
            CactusSystem.Draw();
            DinosaurSystem.Draw();
            ScoreSystem.Draw();
            
            if (Globals.GameState == GameStates.GameOver)
                ReplayButtonSystem.Draw();

            Globals.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}