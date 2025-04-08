
// Type: GameManager.Game1
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameManager.Core;
using GameManager.Utility;

#nullable disable
namespace GameManager
{
  public class Game1 : Game
  {
    

    // *********************************************************************
    Vector2 baseScreenSize = new Vector2(1280, 720);
    private Matrix globalTransformation;
    int backbufferWidth, backbufferHeight;
    public static bool FirstResize = true;
    public static Vector3 screenScale;
    // *********************************************************************

    private readonly Color backgroundColor = new Color(9, 4, 17);
    private bool _isLargeReso;
    private static Texture2D _whiteRect;
    public static ChoiceMaker ChoiceMaker;
    public static Stage CurrentStage;
    private double drawTimeStep;
    public string InitialLevel = "Level4bab";
    public static bool PlayerChoseBouncy = true;
    public static bool PlayerChoseIcy;
    public static bool PlayerHasDash = true;
    public static bool PlayerHasDoubleJump; // !
    public static bool PlayerHasFloat = true;
    public static bool PlayerHasWallJump;
    public static RestraintMessageScreen SorryScreen;
    private static double updateTimeStep;

    public static GraphicsDeviceManager graphics;

    public static SpriteBatch SpriteBatch;

    public static InputHandler InputHandler { get; internal set; }

    public static ResourceManager Resources { get; internal set; }

    public static Options Options { get; set; }

        public static AudioManager Audio { get; internal set; }

        public Game1()
    {
      Game1.graphics = new GraphicsDeviceManager((Microsoft.Xna.Framework.Game) this);
      this.Content.RootDirectory = "Content";
      Game1.Options = new Options();
      Game1.graphics.PreferredBackBufferWidth = Options.RESOLUTION_DEFAULT.X;
      Game1.graphics.PreferredBackBufferHeight = Options.RESOLUTION_DEFAULT.Y;
      Game1.graphics.IsFullScreen = false;
      this._isLargeReso = false;
      Game1.graphics.GraphicsProfile = GraphicsProfile.Reach;
      this.IsMouseVisible = true;
      this.Window.Title = "Choices";
      this.InitialLevel = "Level1";
      Game1.PlayerHasDoubleJump = false;
      Game1.PlayerHasDash = false;
      Game1.PlayerHasWallJump = false;
      Game1.PlayerHasFloat = false;
    }



    protected override void Initialize()
    {
      Game1.InputHandler = new InputHandler();
      Game1.InputHandler.RegisterKeyboardListener(Keys.W);
      Game1.InputHandler.RegisterKeyboardListener(Keys.A);
      Game1.InputHandler.RegisterKeyboardListener(Keys.D);
      Game1.InputHandler.RegisterKeyboardListener(Keys.Left);
      Game1.InputHandler.RegisterKeyboardListener(Keys.Right);
      Game1.InputHandler.RegisterKeyboardListener(Keys.Up);
      Game1.InputHandler.RegisterKeyboardListener(Keys.J);
      Game1.InputHandler.RegisterKeyboardListener(Keys.K);
      Game1.InputHandler.RegisterMouseListener(MouseKeys.LeftButton);
      Game1.InputHandler.RegisterKeyboardListener(Keys.Escape);
      Game1.InputHandler.RegisterKeyboardListener(Keys.F4);
      Game1.Audio = new AudioManager();
      this.Window.AllowUserResizing = false;
      this.Window.AllowAltF4 = true;
      base.Initialize();
    }

    protected override void LoadContent()
    {
      Game1.SpriteBatch = new SpriteBatch(this.GraphicsDevice); 

      Game1.Resources = new ResourceManager();
      Game1.Resources.Initialize(this.Content);
      Game1._whiteRect = new Texture2D(this.GraphicsDevice, 1, 1);
      Game1._whiteRect.SetData<Color>(new Color[1]
      {
        Color.White
      });
      this.InitializeGameState();
    }

    private void InitializeGameState()
    {
      Game1.SorryScreen = new RestraintMessageScreen();
      Game1.ChoiceMaker = new ChoiceMaker();
      Game1.CurrentStage = new Stage();
      Game1.CurrentStage.LoadLevel(this.InitialLevel);
      Game1.ChoiceMaker.Load(Game1.CurrentStage.Data.ChoiceId);
      Game1.Audio.PlayMusic("Balcony");
    }

    public new void Dispose()
    {
      Game1.InputHandler.Dispose();
      base.Dispose();
    }

    protected override void UnloadContent()
    {
      Game1.Resources.Unload();
      this.Content.Unload();
    }

    protected override void Update(GameTime gameTime)
    {
      Game1.updateTimeStep = gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0;
      Game1.InputHandler.Update(Mouse.GetState(), Keyboard.GetState());
      Game1.InputHandler.GenerateCommands();
      foreach (KeyboardCommand keyCommand in Game1.InputHandler.KeyCommands)
      {
        switch (keyCommand.Button)
        {
          case Keys.Escape:
            this.Exit();
            return;
          case Keys.F4:
            if (keyCommand.State == InputState.Pressed)
            {
              Game1.Options.LargeResolution = !Game1.Options.LargeResolution;
              continue;
            }
            continue;
          default:
            continue;
        }
      }
      if (this._isLargeReso != Game1.Options.LargeResolution)
      {
        if (Game1.graphics.GraphicsDevice.DisplayMode.Width < Options.RESOLUTION_LARGE.X 
                    || Game1.graphics.GraphicsDevice.DisplayMode.Height < Options.RESOLUTION_LARGE.Y)
          Game1.Options.CanHandleLargeReso = false;

        if (Game1.Options.LargeResolution && !Game1.Options.CanHandleLargeReso)
          Game1.Options.LargeResolution = false;

        Game1.graphics.PreferredBackBufferWidth = Game1.Options.GetBackBufferWidth();
        Game1.graphics.PreferredBackBufferHeight = Game1.Options.GetBackBufferHeight();
        Game1.graphics.IsFullScreen = Game1.Options.LargeResolution;
        Game1.graphics.ApplyChanges();
        this._isLargeReso = Game1.Options.LargeResolution;
      }
      if (!Game1.SorryScreen.Update(Game1.updateTimeStep) && !Game1.ChoiceMaker.Update(Game1.updateTimeStep))
        Game1.CurrentStage.Update(Game1.updateTimeStep);
      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      this.drawTimeStep = gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0;
      this.GraphicsDevice.Clear(this.backgroundColor);
      Game1.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
      Game1.CurrentStage.DrawBackground(Game1.ChoiceMaker.Active ? this.drawTimeStep / 4.0 : this.drawTimeStep);
      Game1.SpriteBatch.End();
      Game1.SpriteBatch.Begin(transformMatrix: new Matrix?(Game1.CurrentStage.Camera.GetViewMatrix()));
      Game1.CurrentStage.DrawForeground(this.drawTimeStep);
      Game1.SpriteBatch.End();
      SpriteBatch mainSpriteBatch = Game1.SpriteBatch;
      SamplerState anisotropicWrap = SamplerState.AnisotropicWrap;
      BlendState alphaBlend = BlendState.AlphaBlend;
      SamplerState samplerState = anisotropicWrap;

      Matrix? transformMatrix = new Matrix?();
      mainSpriteBatch.Begin(blendState: alphaBlend, samplerState: samplerState,
          transformMatrix: transformMatrix);

      if (Game1.SorryScreen.Active)
        Game1.SorryScreen.Draw(this.drawTimeStep);
      else
        Game1.ChoiceMaker.Draw(this.drawTimeStep);

      Game1.SpriteBatch.End();
      base.Draw(gameTime);
    }

    public static void DrawRectangle(
      Vector2 pos,
      Vector2 size,
      Color color,
      float rotation = 0.0f,
      float opacity = 1f,
      Vector2 origin = default (Vector2))
    {
      Game1.SpriteBatch.Draw(Game1._whiteRect, pos * (float) Game1.Options.GetResolutionScaleFactor(), 
          new Rectangle?(), color * opacity, rotation, origin,
          size * (float) Game1.Options.GetResolutionScaleFactor(), SpriteEffects.None, 0.0f);
    }

    public static void DrawRectangle(
      Point pos,
      Point size,
      Color color,
      float rotation = 0.0f,
      float opacity = 1f,
      Vector2 origin = default (Vector2))
    {
      Game1.SpriteBatch.Draw(Game1._whiteRect, new Rectangle(pos.X * Game1.Options.GetResolutionScaleFactor(),
          pos.Y * Game1.Options.GetResolutionScaleFactor(), size.X * Game1.Options.GetResolutionScaleFactor(), 
          size.Y * Game1.Options.GetResolutionScaleFactor()), new Rectangle?(), color * opacity, rotation,
          origin, SpriteEffects.None, 0.0f);
    }
  }
}
