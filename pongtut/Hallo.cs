using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace Tutorial
{
    public class Hallo : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private SpriteFont textfont;
        private Vector2 textPos;
        private const string TextMsg = "Welcome To My Bouncer";

        private const int ScreenW = 1600;
        private const int ScreenH = 900;

        private Texture2D bg;
        private Rectangle bgRect;

        private Texture2D BallText;
        private Rectangle BallRect;
        private Texture2D PaddleText;
        private Rectangle PaddleRect;
        private KeyboardState kb;
        private const int speed = 7;
        private float vX;
        private float vY;
        private int count;
         
        public Hallo()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = ScreenW;
            graphics.PreferredBackBufferHeight = ScreenH;
            graphics.ApplyChanges();

            Window.AllowUserResizing = false;
            Window.AllowAltF4 = true;
            Window.Title = "By Sofie & Maren!";

            vX = 30f;
            vY = 50f;
            count = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            bg = Content.Load<Texture2D>("Textures/background");
            bgRect = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);

            textfont = Content.Load<SpriteFont>("Fonts/Textfont");
            textPos = new Vector2(0,10);

            BallText = Content.Load<Texture2D>("Textures/ball");
            BallRect = new Rectangle(500, 500, BallText.Width/2, BallText.Height/2);

            PaddleText = Content.Load<Texture2D>("Textures/paddle");
            PaddleRect = new Rectangle(500, 800, PaddleText.Width, PaddleText.Height);

        }

        protected override void Update(GameTime gameTime)
        {

            if (BallRect.Bottom > graphics.GraphicsDevice.Viewport.Height)
            {
                kb = Keyboard.GetState();
                if (kb.IsKeyDown(Keys.Escape))
                {
                    Exit();
                }
                else if (kb.GetPressedKeyCount() > 0 )
                {
                    Initialize();
                }
            }
            else
            {

                if (BallRect.Top <= 0)
                {
                    vY = -vY;
                }
                else if (BallRect.Intersects(PaddleRect) && vY < 0)
                {
                    vY = -vY;
                    count += 1;
                }
                BallRect.Y -= (int)(speed * vY * gameTime.ElapsedGameTime.Milliseconds / 1000f);

                if (BallRect.Left <= 0)
                {
                    vX = -vX;
                }
                else if (BallRect.Right >= graphics.GraphicsDevice.Viewport.Width)
                {
                    vX = -vX;
                }
                BallRect.X -= (int)(speed * vX * gameTime.ElapsedGameTime.Milliseconds / 1000f);

                kb = Keyboard.GetState();

                if (kb.IsKeyDown(Keys.W))
                    PaddleRect.Y -= speed;
                else if (kb.IsKeyDown(Keys.S))
                    PaddleRect.Y += speed;

                else if (kb.IsKeyDown(Keys.D) || kb.IsKeyDown(Keys.Right))
                    PaddleRect.X += speed;
                else if (kb.IsKeyDown(Keys.A) || kb.IsKeyDown(Keys.Left))
                    PaddleRect.X -= speed;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightPink);

            spriteBatch.Begin();
            spriteBatch.Draw(bg, bgRect, Color.White);

            spriteBatch.DrawString(textfont, TextMsg + ": " + count, textPos, Color.White);

            spriteBatch.Draw(BallText, BallRect, Color.White);

            spriteBatch.Draw(PaddleText, PaddleRect, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}