using ld39.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ld39
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameMain : Game
    {
        public enum GAMESTATE { Winning, Playing }
        public static GAMESTATE State;
        public static int CurrentLevel;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 960;
            graphics.PreferredBackBufferHeight = 640;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            IsMouseVisible = true;
            State = GAMESTATE.Playing;
            CurrentLevel = 1;

            LevelManager.LoadLevel(CurrentLevel);
            EntityManager.InitializeIndicator();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Art.Load(Content);
        }

        protected override void UnloadContent()
        {
            // Unload any non ContentManager content here
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            InputManager.Update();
            EntityManager.Update(deltaTime);
            Cursor.Instance.Update(deltaTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            DrawCenteredAlignedString(Art.FontLarge, "POWER THE HOUSE!", 50);
            DrawCenteredAlignedString(Art.FontMedium, "Level " + CurrentLevel, 100);
            spriteBatch.DrawString(Art.FontSmall, Hint.HintText[CurrentLevel], new Vector2(25, 150), Color.White);

            if (State.Equals(GAMESTATE.Winning))
            {
                if (CurrentLevel < 9)
                    DrawStringToNext();
                else
                    DrawStringToEnd();
            }

            EntityManager.Draw(spriteBatch);
            Cursor.Instance.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawCenteredAlignedString(SpriteFont font, string text, float y)
        {
            var textWidth = font.MeasureString(text).X;
            spriteBatch.DrawString(font, text, new Vector2((320 - textWidth) / 2, y), Color.White);
        }

        private void DrawStringToNext()
        {
            string text1 = "Press ENTER to continue";
            var text1Height = Art.FontMedium.MeasureString(text1).Y;
            DrawCenteredAlignedString(Art.FontMedium, text1, 640 - text1Height - 50);

            string text2 = "LEVEL COMPLETE!";
            var text2Height = Art.FontMedium.MeasureString(text2).Y;
            DrawCenteredAlignedString(Art.FontMedium, text2, 640 - text1Height - text2Height - 70);
        }

        private void DrawStringToEnd()
        {
            string text1 = "THANK YOU FOR PLAYING";
            var text1Height = Art.FontMedium.MeasureString(text1).Y;
            DrawCenteredAlignedString(Art.FontMedium, text1, 640 - text1Height - 50);

            string text2 = "That's all!";
            var text2Height = Art.FontMedium.MeasureString(text2).Y;
            DrawCenteredAlignedString(Art.FontMedium, text2, 640 - text1Height - text2Height - 70);
        }
    }
}
