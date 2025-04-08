
// Type: GameManager.Utility.BasicGraphic
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using Microsoft.Xna.Framework;

#nullable disable
namespace GameManager.Utility
{
  public class BasicGraphic : IGraphic
  {
    private readonly Sprite _sprite;
    public Point DrawScale;
    public string GraphicId;
    public float Opacity;
    public float Rotation;

    public BasicGraphic(Sprite sprite, Point scale = default (Point), float rot = 0.0f, float opacity = 1f)
    {
      this.GraphicId = "CUSTOM";
      this._sprite = sprite;
      this.DrawScale = !(scale == new Point()) ? scale : new Point(1, 1);
      this.Rotation = rot;
      this.Opacity = opacity;
    }

    public BasicGraphic(string id)
    {
      BasicGraphicData graphicData = Game1.Resources.GraphicDatas[id] as BasicGraphicData;
      this.GraphicId = id;
      this._sprite = Game1.Resources.Sprites[graphicData.SpriteId];
      this.DrawScale = graphicData.DrawScale;
      this.Rotation = graphicData.Rotation;
      this.Opacity = graphicData.Opacity;
    }

    public void Draw(int x, int y, double timeStep)
    {
      this._sprite.Draw(x, y, this.DrawScale.X, this.DrawScale.Y, this.Rotation, this.Opacity);
    }
  }
}
