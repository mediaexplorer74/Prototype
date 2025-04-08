
// Type: GameManager.Utility.RectangleGraphicData
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using Microsoft.Xna.Framework;

#nullable disable
namespace GameManager.Utility
{
  public class RectangleGraphicData : IDescriptor
  {
    public string Id { get; internal set; }

    public Point DrawSize { get; internal set; }

    public Color Color { get; internal set; }

    public float Rotation { get; internal set; }

    public float Opacity { get; internal set; }

    public bool DrawCentered { get; internal set; }

    public string GetIdentifier() => this.Id;
  }
}
