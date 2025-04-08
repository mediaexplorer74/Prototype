
// Type: GameManager.Utility.SimpleAnimGraphic
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using Microsoft.Xna.Framework;
using System;

#nullable disable
namespace GameManager.Utility
{
  public class SimpleAnimGraphic : IGraphic
  {
    private readonly int _drawsPerFrame;
    private readonly Sprite[] _sprites;
    private readonly long drawCount;
    public Point DrawScale;
    public string GraphicId;
    public float Opacity;
    public float Rotation;

    public SimpleAnimGraphic(
      Sprite[] sprites,
      Point scale = default (Point),
      float rot = 0.0f,
      float opacity = 1f,
      int drawsPerFrame = 3)
    {
      this.GraphicId = "CUSTOM";
      this._sprites = sprites;
      this.DrawScale = !(scale == new Point()) ? scale : new Point(1, 1);
      this.Rotation = rot;
      this.Opacity = opacity;
      this._drawsPerFrame = drawsPerFrame;
    }

    public SimpleAnimGraphic(string id)
    {
      SimpleAnimGraphicData graphicData = Game1.Resources.GraphicDatas[id] as SimpleAnimGraphicData;
      this.GraphicId = id;
      this._sprites = new Sprite[graphicData.SpriteIds.Length];
      for (short index = 0; (int) index < this._sprites.Length; ++index)
        this._sprites[(int) index] = Game1.Resources.Sprites[graphicData.SpriteIds[(int) index]];
      this.DrawScale = graphicData.DrawScale;
      this.Rotation = graphicData.Rotation;
      this.Opacity = graphicData.Opacity;
      this._drawsPerFrame = graphicData.DrawsPerFrame;
    }

    public void Draw(int x, int y, double timeStep)
    {
      this._sprites[(int) Math.Floor((double) (this.drawCount % (long) (this._sprites.Length 
          * this._drawsPerFrame)) / (double) this._drawsPerFrame)].Draw(x,
          y, this.DrawScale.X, this.DrawScale.Y, this.Rotation, this.Opacity);
    }
  }
}
