
// Type: GameManager.Core.ScrollingBgObject
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using Microsoft.Xna.Framework;
using GameManager.Utility;

#nullable disable
namespace GameManager.Core
{
  public class ScrollingBgObject
  {
    public float Opacity;
    public Vector2 Position;
    public Point Scale;
    public float ScrollSpeed;
    public Sprite Sprite;

    public ScrollingBgObject(
      Sprite sprite,
      Vector2 pos,
      float opacity,
      Point scale,
      float scrollSpeed)
    {
      this.Sprite = sprite;
      this.Position = pos;
      this.Opacity = opacity;
      this.Scale = scale;
      this.ScrollSpeed = scrollSpeed;
    }

    public void Draw(double timeStep)
    {
      this.Position.X -= this.ScrollSpeed * (float) timeStep;
      if ((double) this.Position.X < (double) -(this.Sprite.Texture.Width * this.Scale.X))
        this.Position.X = (float) (Options.RESOLUTION_DEFAULT.X + this.Sprite.Texture.Width * this.Scale.X);
      this.Sprite.Draw((int) this.Position.X, (int) this.Position.Y 
          - (int) ((double) Game1.CurrentStage.Camera.Location.Y / 10.0), this.Scale.X, this.Scale.Y, opacity: this.Opacity, centered: true);
    }
  }
}
