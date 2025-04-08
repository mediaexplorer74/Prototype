
// Type: GameManager.Core.Camera2D
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace GameManager.Core
{
  public class Camera2D
  {
    public readonly Viewport Viewport;
    public Vector2 Location;

    public Camera2D(Viewport viewport)
    {
      this.Viewport = viewport;
      this.Rotation = 0.0f;
      this.Zoom = 1f;
      this.Origin = new Vector2((float) viewport.Width / 2f, (float) viewport.Height / 2f);
      this.Location = Vector2.Zero;
    }

    public float Rotation { get; set; }

    public float Zoom { get; set; }

    public Vector2 Origin { get; set; }

    public Matrix GetViewMatrix()
    {
      return Matrix.CreateTranslation(new Vector3(-this.Location, 0.0f)) * Matrix.CreateTranslation(new Vector3(-this.Origin, 0.0f)) * Matrix.CreateRotationZ(this.Rotation) * Matrix.CreateScale(this.Zoom, this.Zoom, 1f) * Matrix.CreateTranslation(new Vector3(this.Origin, 0.0f));
    }

    public void Update(Player player)
    {
      this.Location.X = player.Position.X - this.Viewport.Width / 2 <= 0 ? 0.0f : (float) ((player.Position.X - this.Viewport.Width / 2) * Game1.Options.GetResolutionScaleFactor());
      if (player.Position.Y < this.Viewport.Height / 2)
        this.Location.Y = (float) ((player.Position.Y - this.Viewport.Height / 2) 
                    * Game1.Options.GetResolutionScaleFactor());
      else
        this.Location.Y = 0.0f;
    }
  }
}
