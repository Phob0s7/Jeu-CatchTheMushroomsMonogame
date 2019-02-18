using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CatchTheMushrooms
{
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Texture2D hand_Sprite;
		Texture2D mushroom_Sprite;
		Texture2D background_Sprite;

		SpriteFont gameFont;

		MouseState mState;

		Vector2 mushroomPosition = new Vector2(150, 150);

		bool mReleased = true;
		int nberOfMushrooms= 0;
		const int MUSHROOM_RADIUS = 20;
		float timer = 15F;
		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = false;
		}

		protected override void Initialize()
		{
			base.Initialize();
		}

		
		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			hand_Sprite = Content.Load<Texture2D>("hand");
			mushroom_Sprite = Content.Load<Texture2D>("mushroom");
			background_Sprite = Content.Load<Texture2D>("background");
			gameFont = Content.Load<SpriteFont>("gameFont");
		}

		
		protected override void UnloadContent()
		{
		}
		
		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

			Random rand = new Random();

			mState = Mouse.GetState();

			float mouseDistanceMushroom = Vector2.Distance(mushroomPosition, new Vector2(mState.X, mState.Y));

			if (timer <= 0)
			{
				timer = 0;
			}

			if (mState.LeftButton == ButtonState.Pressed && mReleased == true && timer > 0)
			{
				if (mouseDistanceMushroom < MUSHROOM_RADIUS)
				{
					nberOfMushrooms++;
					mushroomPosition.X = rand.Next(MUSHROOM_RADIUS, graphics.PreferredBackBufferWidth - MUSHROOM_RADIUS + 1);
					mushroomPosition.Y = rand.Next(MUSHROOM_RADIUS, graphics.PreferredBackBufferHeight - MUSHROOM_RADIUS + 1);
				}
				mReleased = false;
			}

			else if (mState.LeftButton == ButtonState.Released)
			{
				mReleased = true;
			}
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			spriteBatch.Draw(background_Sprite, new Vector2(0, 0), Color.White);
			if (timer > 0)
			{
				spriteBatch.Draw(mushroom_Sprite, new Vector2(mushroomPosition.X - MUSHROOM_RADIUS, mushroomPosition.Y - MUSHROOM_RADIUS), Color.White);

			}
			spriteBatch.Draw(hand_Sprite, new Vector2(mState.X - MUSHROOM_RADIUS, mState.Y - MUSHROOM_RADIUS), Color.White);
			spriteBatch.DrawString(gameFont, "Number of mushrooms : " + nberOfMushrooms, new Vector2(10,10), Color.Green);
			spriteBatch.DrawString(gameFont, "Time : " + Math.Ceiling(timer), new Vector2(10,40), Color.Green);

			spriteBatch.End();
		
			base.Draw(gameTime);
		}
	}
}
