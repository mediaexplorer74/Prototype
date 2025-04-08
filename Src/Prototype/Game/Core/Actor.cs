
// Type: GameManager.Core.Actor
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace GameManager.Core
{
  public class Actor : GameObject
  {
    private bool _init;
    public Vector2 ActualCoords;
    public Vector2 ActualVelocity = new Vector2(0.0f, 0.0f);
    public PlatformType CurrentPlatformType;
    public Rectangle LastPosition;
    public bool OnGround;
    public Stage Stage;

    public Actor()
    {
    }

    public Actor(string id, Point position)
      : base(id, position)
    {
      this.ActualCoords.X = (float) position.X;
      this.ActualCoords.Y = (float) position.Y;
      this.LastPosition = this.Position;
    }

    public virtual void Initialize(Stage stage)
    {
      this.Stage = stage;
      this._init = true;
    }

    public override void Update(double timeStep)
    {
      if (!this._init)
        throw new InvalidOperationException("Actor called Update() before initialization!");
      if (this.Stage.ChangingWorld)
        return;
      this.OnGround = false;
      foreach (Platform platform in this.Stage.Platforms)
      {
        if (this.CollisionCheck(platform))
          return;
      }
      this.Move(timeStep);
    }

    public virtual void Move(double timeStep)
    {
      if (!this.OnGround)
      {
        this.ActualVelocity.Y += 0.5f * this.Stage.GravityMod;
        this.CurrentPlatformType = PlatformType.None;
      }
      this.LastPosition = this.Position;
      this.ActualCoords.X += this.ActualVelocity.X;
      this.ActualCoords.Y += this.ActualVelocity.Y;
      this.Position.X = (int) this.ActualCoords.X;
      this.Position.Y = (int) this.ActualCoords.Y;
    }

    public override void Draw(double timeStep) => base.Draw(timeStep);

    public virtual bool CollisionCheck(Platform collider)
    {
      Rectangle rectangle = new Rectangle(collider.Position.X - 1, collider.Position.Y - 1, collider.Position.Width + 2, collider.Position.Height + 2);
      if (!this.Position.Intersects(rectangle) && !rectangle.Contains(this.Position))
        return false;
      bool flag1 = this.Position.Intersects(new Rectangle(collider.Position.X - 1, collider.Position.Y - 1, collider.Position.Width + 2, collider.Position.Height / 2 + 2));
      bool flag2 = this.Position.Intersects(new Rectangle(collider.Position.X - 1, collider.Position.Center.Y - 1, collider.Position.Width + 2, collider.Position.Height / 2 + 2));
      bool flag3 = this.Position.Intersects(new Rectangle(collider.Position.Center.X - 1, collider.Position.Y - 1, collider.Position.Width / 2 + 2, collider.Position.Height + 2));
      bool flag4 = this.Position.Intersects(new Rectangle(collider.Position.X - 1, collider.Position.Y - 1, collider.Position.Width / 2 + 2, collider.Position.Height + 2));
      if (flag1)
        (this as Player).topconnecting = true;
      if (flag2 && this.Position.Top >= collider.Position.Bottom && collider.Direction == Direction.Down)
        (this as Player).bottomconnecting = true;
      if (flag1)
      {
        if (flag3)
          return this.Position.Right <= collider.Position.Right || this.Position.Bottom < collider.Position.Bottom && this.Position.Bottom - collider.Position.Top <= collider.Position.Right - this.Position.Left ? collider.OnCollide(this, PlatformSide.Top) : collider.OnCollide(this, PlatformSide.Right);
        if (flag4)
          return this.Position.Left >= collider.Position.Left || this.Position.Bottom < collider.Position.Bottom && this.Position.Bottom - collider.Position.Top <= this.Position.Right - collider.Position.Left ? collider.OnCollide(this, PlatformSide.Top) : collider.OnCollide(this, PlatformSide.Left);
      }
      else
      {
        if (flag3)
          return this.Position.Bottom <= collider.Position.Bottom || this.Position.Right > collider.Position.Right && this.Position.Left - collider.Position.Right <= this.Position.Top - collider.Position.Bottom ? collider.OnCollide(this, PlatformSide.Right) : collider.OnCollide(this, PlatformSide.Bottom);
        if (flag4)
          return this.Position.Bottom <= collider.Position.Bottom || this.Position.Left < collider.Position.Left && this.Position.Right - collider.Position.Left <= this.Position.Top - collider.Position.Bottom ? collider.OnCollide(this, PlatformSide.Left) : collider.OnCollide(this, PlatformSide.Bottom);
      }
      return false;
    }
  }
}
