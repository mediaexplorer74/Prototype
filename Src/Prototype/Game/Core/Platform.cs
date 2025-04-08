
// Type: GameManager.Core.Platform
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using Microsoft.Xna.Framework;
using GameManager.Utility;
using System.Collections.Generic;

#nullable disable
namespace GameManager.Core
{
  public class Platform : GameObject
  {
    private static readonly Color DefaultPlatformColor = new Color(50, 50, 50);
    public List<Actor> ConnectedObjects = new List<Actor>();
    public Direction Direction;
    public float MoveRange;
    public bool Moving;
    public Stage owner;
    public Direction StartingDir;
    public Point StartingPos;
    public PlatformType Type;

    public Platform()
    {
    }

    public Platform(string id, Point position, PlatformType type = PlatformType.Default)
      : base(id, position)
    {
      this.Type = type;
    }

    public bool OnCollide(Actor a, PlatformSide side)
    {
      switch (this.Type)
      {
        case PlatformType.Default:
        case PlatformType.Icy:
          this.RespondNormal(a, side);
          break;
        case PlatformType.Bouncy:
          this.RespondBouncy(a, side);
          break;
        case PlatformType.Win:
          this.RespondWin(a, side);
          return true;
      }
      return false;
    }

    private void RespondNormal(Actor a, PlatformSide side)
    {
      switch (side)
      {
        case PlatformSide.Top:
          if ((double) a.ActualVelocity.Y > 0.0)
            a.ActualVelocity.Y = 0.0f;
          a.ActualCoords.Y = (float) (this.Position.Top - a.Position.Height);
          a.CurrentPlatformType = this.Type;
          a.OnGround = true;
          this.ConnectedObjects.Add(a);
          break;
        case PlatformSide.Bottom:
          if ((double) a.ActualVelocity.Y < 0.0)
            a.ActualVelocity.Y = 0.0f;
          a.ActualCoords.Y = (float) this.Position.Bottom;
          this.ConnectedObjects.Add(a);
          break;
        case PlatformSide.Right:
          if ((double) a.ActualVelocity.X < 0.0)
            a.ActualVelocity.X = 0.0f;
          a.ActualCoords.X = (float) this.Position.Right;
          if (Game1.PlayerHasWallJump)
          {
            (a as Player).OnLeftWall = true;
            (a as Player).WallCooldown = 100.0;
          }
          this.ConnectedObjects.Add(a);
          break;
        case PlatformSide.Left:
          if ((double) a.ActualVelocity.X > 0.0)
            a.ActualVelocity.X = 0.0f;
          a.ActualCoords.X = (float) (this.Position.Left - a.Position.Width);
          if (Game1.PlayerHasWallJump)
          {
            (a as Player).OnRightWall = true;
            (a as Player).WallCooldown = 100.0;
          }
          this.ConnectedObjects.Add(a);
          break;
      }
    }

    private void RespondBouncy(Actor a, PlatformSide side)
    {
      switch (side)
      {
        case PlatformSide.Top:
          if ((double) a.ActualCoords.Y == (double) (this.Position.Top - a.Position.Height))
            break;
          a.ActualCoords.Y = (float) (this.Position.Top - a.Position.Height);
          a.ActualVelocity.Y = -(double) a.ActualVelocity.Y * 0.89999997615814209 < -10.0 ? (float) (-(double) a.ActualVelocity.Y * 0.89999997615814209) : -10f;
          a.CurrentPlatformType = this.Type;
          break;
        case PlatformSide.Bottom:
          if ((double) a.ActualCoords.Y != (double) this.Position.Bottom)
            a.ActualCoords.Y = (float) this.Position.Bottom;
          a.ActualVelocity.Y = -a.ActualVelocity.Y;
          break;
        case PlatformSide.Right:
          if ((double) a.ActualVelocity.X > 0.0)
          {
            a.ActualVelocity.X *= 2f;
            break;
          }
          a.ActualVelocity.X = (float) (-(double) a.ActualVelocity.X * 2.0);
          break;
        case PlatformSide.Left:
          if ((double) a.ActualVelocity.X > 0.0)
          {
            a.ActualVelocity.X = (float) (-(double) a.ActualVelocity.X * 2.0);
            break;
          }
          a.ActualVelocity.X *= 2f;
          break;
      }
    }

    private void RespondWin(Actor a, PlatformSide side)
    {
      if (!(a is Player))
        return;
      if (!a.Stage.ChoiceMade && Game1.ChoiceMaker.Initialized)
        Game1.ChoiceMaker.Active = true;
      else
        a.Stage.LoadNextLevel();
    }

    public override void Update(double timeStep)
    {
      base.Update(timeStep);
      if (!this.Moving)
        return;
      this.Move((Actor) this.owner.Player);
    }

    public void Move(Actor player)
    {
      switch (this.Direction)
      {
        case Direction.Up:
          if ((double) (Options.RESOLUTION_DEFAULT.Y - this.Position.Height - this.Position.Y - this.StartingPos.Y) < (double) this.MoveRange)
          {
            --this.Position.Y;
            using (List<Actor>.Enumerator enumerator = this.ConnectedObjects.GetEnumerator())
            {
              while (enumerator.MoveNext())
                --enumerator.Current.ActualCoords.Y;
              break;
            }
          }
          else
          {
            this.Direction = Direction.Down;
            break;
          }
        case Direction.Down:
          if ((double) (Options.RESOLUTION_DEFAULT.Y - this.Position.Height - this.Position.Y - this.StartingPos.Y) > -(double) this.MoveRange)
          {
            ++this.Position.Y;
            using (List<Actor>.Enumerator enumerator = this.ConnectedObjects.GetEnumerator())
            {
              while (enumerator.MoveNext())
                ++enumerator.Current.ActualCoords.Y;
              break;
            }
          }
          else
          {
            this.Direction = Direction.Up;
            break;
          }
        case Direction.Left:
          if ((double) (this.Position.X - this.StartingPos.X) > -(double) this.MoveRange)
          {
            --this.Position.X;
            using (List<Actor>.Enumerator enumerator = this.ConnectedObjects.GetEnumerator())
            {
              while (enumerator.MoveNext())
                --enumerator.Current.ActualCoords.X;
              break;
            }
          }
          else
          {
            this.Direction = Direction.Right;
            break;
          }
        case Direction.Right:
          if ((double) (this.Position.X - this.StartingPos.X) < (double) this.MoveRange)
          {
            ++this.Position.X;
            using (List<Actor>.Enumerator enumerator = this.ConnectedObjects.GetEnumerator())
            {
              while (enumerator.MoveNext())
                ++enumerator.Current.ActualCoords.X;
              break;
            }
          }
          else
          {
            this.Direction = Direction.Left;
            break;
          }
      }
    }

    public override void Draw(double timeStep) => base.Draw(timeStep);

    public static Color GetPlatformTypeColor(PlatformType type)
    {
      switch (type)
      {
        case PlatformType.Bouncy:
          return Color.Orange;
        case PlatformType.Icy:
          return Color.LightBlue;
        case PlatformType.Win:
          return Color.Green;
        default:
          return Platform.DefaultPlatformColor;
      }
    }
  }
}
