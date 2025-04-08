
// Type: GameManager.Utility.RectangleGraphic
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using Microsoft.Xna.Framework;

#nullable disable
namespace GameManager.Utility
{
  public class RectangleGraphic : IGraphic
  {
    public Color Color;
    public bool DrawCentered;
    public Point DrawSize;
    public string GraphicId;
    public float Opacity;
    public float Rotation;

    public RectangleGraphic(Point size, Color color, float rot = 0.0f, float opacity = 1f, bool drawCentered = false)
    {
      this.GraphicId = "CUSTOM";
      this.DrawSize = size;
      this.Color = color;
      this.Rotation = rot;
      this.Opacity = opacity;
      this.DrawCentered = drawCentered;
    }

    public RectangleGraphic(string id)
    {
      RectangleGraphicData graphicData = Game1.Resources.GraphicDatas[id] as RectangleGraphicData;
      this.GraphicId = id;
      this.DrawSize = graphicData.DrawSize;
      this.Color = graphicData.Color;
      this.Rotation = graphicData.Rotation;
      this.Opacity = graphicData.Opacity;
      this.DrawCentered = graphicData.DrawCentered;
    }

    public void Draw(int x, int y, double timeStep)
    {
      Game1.DrawRectangle(new Point(x, y), this.DrawSize, this.Color, this.Rotation, this.Opacity, 
          this.DrawCentered ? new Vector2((float) (this.DrawSize.X / 2), (float) (this.DrawSize.Y / 2))
          : Vector2.Zero);
    }
  }
}
