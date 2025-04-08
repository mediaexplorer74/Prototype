
// Type: GameManager.Utility.SimpleAnimGraphicData
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using Microsoft.Xna.Framework;

#nullable disable
namespace GameManager.Utility
{
  public class SimpleAnimGraphicData : IDescriptor
  {
    public string Id { get; internal set; }

    public string[] SpriteIds { get; internal set; }

    public Point DrawScale { get; internal set; }

    public float Rotation { get; internal set; }

    public float Opacity { get; internal set; }

    public int DrawsPerFrame { get; internal set; }

    public string GetIdentifier() => this.Id;
  }
}
