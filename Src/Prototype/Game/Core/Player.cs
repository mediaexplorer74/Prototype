
// Type: GameManager.Core.Player
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameManager.Utility;

#nullable disable
namespace GameManager.Core
{
  public class Player(string id, Point position) : Actor(id, position)
  {
    public const float JUMP_VELO = 30f;//10f;
    private const float HORIZONTAL_SPEED = 0.45f;
    private const float MAX_SPEED = 7f;//3.25f;
    public bool bottomconnecting;
    private double dashcooldown;
    private int dashes;
    private int deathcount;
    public RectangleGraphic Fill;
    public int jumpsleft;
    private bool leftButtonHeld;
    public bool OnLeftWall;
    public bool OnRightWall;
    private bool rightButtonHeld;
    public bool topconnecting;
    public double WallCooldown;

    public override void Initialize(Stage stage)
    {
      base.Initialize(stage);
      this.Fill = GraphicData.GenerateGraphic("PlayerGraphicF") as RectangleGraphic;
    }

    public override void Update(double timeStep)
    {
      foreach (KeyboardCommand keyCommand in Game1.InputHandler.KeyCommands)
      {

        if (keyCommand.Button == Keys.Right || keyCommand.Button == Keys.D)
        {
          if (keyCommand.State == InputState.Pressed || keyCommand.State == InputState.Held)
          {
            this.rightButtonHeld = true;
            if (this.CurrentPlatformType != PlatformType.Icy && (double) this.ActualVelocity.X < 3.25)
              this.ActualVelocity.X += 0.45f;
            else if (this.CurrentPlatformType == PlatformType.Icy && (double) this.ActualVelocity.X < 6.5)
              this.ActualVelocity.X += 0.45f;
          }
          if (keyCommand.State == InputState.Released)
            this.rightButtonHeld = false;
        }

        if (keyCommand.Button == Keys.Left || keyCommand.Button == Keys.A)
        {
          if (keyCommand.State == InputState.Pressed || keyCommand.State == InputState.Held)
          {
            this.leftButtonHeld = true;
            if (this.CurrentPlatformType != PlatformType.Icy && (double) this.ActualVelocity.X > -3.25)
              this.ActualVelocity.X -= 0.45f;
            else if (this.CurrentPlatformType == PlatformType.Icy && (double) this.ActualVelocity.X > -6.5)
              this.ActualVelocity.X -= 0.45f;
          }

          if (keyCommand.State == InputState.Released)
            this.leftButtonHeld = false;
        }

        if (keyCommand.Button == Keys.J && keyCommand.State == InputState.Pressed 
                    && keyCommand.State != InputState.Held && this.dashes > 0)
        {
          --this.dashes;
          this.ActualVelocity.X = -10f;
        }

        if (keyCommand.Button == Keys.K 
                    && keyCommand.State == InputState.Pressed 
                    && this.dashes > 0)
        {
          --this.dashes;
          this.ActualVelocity.X = 10f;
        }

        if (keyCommand.Button == Keys.Up || keyCommand.Button == Keys.W)
        {
          if (keyCommand.State == InputState.Pressed || keyCommand.State == InputState.Held)
          {
            if (this.OnGround)
            {
              this.ActualVelocity.Y = -30f;//-10f;
              if (keyCommand.State == InputState.Pressed)
                Game1.Audio.PlaySFX("Jump", 0.333f, 0.8f);
            }
            else if (this.OnRightWall && keyCommand.State == InputState.Pressed)
            {
            this.ActualVelocity.Y = -30f;//-10f;
            this.ActualVelocity.X = -30f;//-10f;

              if (Game1.PlayerHasDoubleJump)
                this.jumpsleft = 1;

              Game1.Audio.PlaySFX("Jump", 0.333f, 0.8f);
            }
            else if (this.OnLeftWall && keyCommand.State == InputState.Pressed)
            {
                this.ActualVelocity.Y = -30f;//-10f;
                this.ActualVelocity.X = 30f;//10f;

              if (Game1.PlayerHasDoubleJump)
                this.jumpsleft = 1;

              Game1.Audio.PlaySFX("Jump", 0.333f, 0.8f);
            }
            else if (this.jumpsleft > 0 && keyCommand.State == InputState.Pressed)
            {
              this.ActualVelocity.Y = -30f;//-10f;
              --this.jumpsleft;
              Game1.Audio.PlaySFX("Jump", 0.333f, 0.8f);
            }
          }
          if (!this.OnGround && keyCommand.State == InputState.Held
                 && Game1.PlayerHasFloat && (double) this.ActualVelocity.Y > /*1.3333333730697632*/4)
            this.ActualVelocity.Y = /*1.33333337f*/4f;
        }
      }
      if (this.CurrentPlatformType != PlatformType.Icy)
      {
        if ((double) this.ActualVelocity.X > 3.25)
          this.ActualVelocity.X -= this.ActualVelocity.X / 30f;
        if ((double) this.ActualVelocity.X < -3.25)
          this.ActualVelocity.X -= this.ActualVelocity.X / 30f;
        if (!this.leftButtonHeld && !this.rightButtonHeld || this.leftButtonHeld && this.rightButtonHeld)
          this.ActualVelocity.X -= this.ActualVelocity.X / 15f;
      }
      else
      {
        if ((double) this.ActualVelocity.X > 6.5)
          this.ActualVelocity.X -= this.ActualVelocity.X / 150f;
        if ((double) this.ActualVelocity.X < -6.5)
          this.ActualVelocity.X -= this.ActualVelocity.X / 150f;
        if (!this.leftButtonHeld && !this.rightButtonHeld || this.leftButtonHeld && this.rightButtonHeld)
          this.ActualVelocity.X -= this.ActualVelocity.X / 75f;
      }
      if (this.dashcooldown > 0.0)
        this.dashcooldown -= timeStep * 1000.0;
      else if (Game1.PlayerHasDash)
      {
        this.dashes = 1;
        this.dashcooldown = 500.0;
      }
      if (this.Stage.ChangingWorld)
        return;

      if (this.OnGround && Game1.PlayerHasDoubleJump)
        this.jumpsleft = 1;

      this.OnGround = false;
      if (this.WallCooldown > 0.0)
      {
        this.WallCooldown -= timeStep * 1000.0;
      }
      else
      {
        this.OnRightWall = false;
        this.OnLeftWall = false;
      }
      foreach (Platform platform in this.Stage.Platforms)
      {
        platform.ConnectedObjects.Clear();
        if (this.CollisionCheck(platform))
          return;
      }
            if (this.topconnecting && this.bottomconnecting)
            {
                this.Die();
            }
            if ((double)this.ActualCoords.Y > (double)Options.RESOLUTION_DEFAULT.Y)
            {
                this.Die();
            }
      this.topconnecting = false;
      this.bottomconnecting = false;
      this.Move(timeStep);
    }

    public override void Move(double timeStep)
    {
      if (!this.OnGround)
      {
        if ((this.OnRightWall || this.OnLeftWall) && (double) this.ActualVelocity.Y > 0.0)
          this.ActualVelocity.Y += (float) (0.5 * (double) this.Stage.GravityMod / 4.0);
        else
          this.ActualVelocity.Y += 0.5f * this.Stage.GravityMod;
        this.CurrentPlatformType = PlatformType.None;
      }
      this.LastPosition = this.Position;
      if ((double) this.ActualVelocity.X > 20.0)
        this.ActualVelocity.X = 20f;
      else if ((double) this.ActualVelocity.X < -20.0)
        this.ActualVelocity.X = -20f;
      if ((double) this.ActualVelocity.Y > 20.0)
        this.ActualVelocity.Y = 20f;
      else if ((double) this.ActualVelocity.Y < -20.0)
        this.ActualVelocity.Y = -20f;
      this.ActualCoords.X += this.ActualVelocity.X;
      this.ActualCoords.Y += this.ActualVelocity.Y;
      this.Position.X = (int) this.ActualCoords.X;
      this.Position.Y = (int) this.ActualCoords.Y;
    }

    public override void Draw(double timeStep)
    {
      base.Draw(timeStep);
      this.Fill.Draw(this.Position.X + 2, this.Position.Y + 2, timeStep);
    }

    public void Die()
    {
      foreach (Platform platform in this.Stage.Platforms)
      {
                platform.Position.X = platform.StartingPos.X;
        platform.Direction = platform.StartingDir;
        platform.Position.Y = Options.RESOLUTION_DEFAULT.Y 
                    - platform.Position.Height - platform.StartingPos.Y;
      }
      ++this.deathcount;
      this.ActualVelocity.X = -10f;//0.0f;
      this.ActualVelocity.Y = -30f;//0.0f;
      //this.ActualCoords.X = (float) this.Stage.Spawn.X;
      //this.ActualCoords.Y = (float) this.Stage.Spawn.Y;
    }
  }
}
