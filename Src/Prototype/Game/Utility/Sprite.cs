
// Type: GameManager.Utility.Sprite
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#nullable disable
namespace GameManager.Utility
{
  public class Sprite : IDescriptor
  {
    private Rectangle _drawRect;
    private Rectangle _srcRect;

    public Sprite(Texture2D texture)
    {
      this.Texture = texture;
      this._srcRect = texture.Bounds;
      this._drawRect = this._srcRect;
    }

    public Sprite(Texture2D texture, Rectangle srcRect)
    {
      this.Texture = texture;
      this._srcRect = srcRect;
      this._drawRect = srcRect;
    }

    public string Id { get; internal set; }

    public Texture2D Texture { get; }

    public string GetIdentifier() => this.Id;

    public void Draw(
      int x = 0,
      int y = 0,
      int xScale = 1,
      int yScale = 1,
      float rotation = 0.0f,
      float opacity = 1f,
      bool centered = false)
    {
      this._drawRect.X = x * Game1.Options.GetResolutionScaleFactor();
      this._drawRect.Y = y * Game1.Options.GetResolutionScaleFactor();
      this._drawRect.Width = this._srcRect.Width * xScale * Game1.Options.GetResolutionScaleFactor();
      this._drawRect.Height = this._srcRect.Height * yScale * Game1.Options.GetResolutionScaleFactor();
      SpriteBatch mainSpriteBatch = Game1.SpriteBatch;
      Texture2D texture = this.Texture;
      Vector2? position = new Vector2?();
      Rectangle? destinationRectangle = new Rectangle?(this._drawRect);
      Rectangle? sourceRectangle = new Rectangle?(this._srcRect);
      float num = rotation;

      Color? nullable = new Color?(Color.White * opacity);
      Vector2? origin = new Vector2?(centered ? new Vector2((float) this._srcRect.Width / 2f, 
          (float) this._srcRect.Height / 2f) : Vector2.Zero);
      double rotation1 = (double) num;
      Vector2? scale = new Vector2?();

      Color? color = nullable;
      mainSpriteBatch.Draw(texture, position, destinationRectangle, sourceRectangle, origin, 
          (float) rotation1, scale, color);
    }
  }
}
