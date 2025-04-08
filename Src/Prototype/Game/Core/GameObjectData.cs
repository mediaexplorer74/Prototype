
// Type: GameManager.Core.GameObjectData
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using Microsoft.Xna.Framework;
using GameManager.Utility;

#nullable disable
namespace GameManager.Core
{
  public class GameObjectData : IDescriptor
  {
    public GameObjectType Type { get; internal set; }

    public PlatformType PlatformType { get; internal set; } = PlatformType.Default;

    public string Id { get; internal set; }

    public string GraphicId { get; internal set; }

    public Point Size { get; internal set; }

    public bool Moving { get; internal set; }

    public float MoveRange { get; internal set; }

    public Direction Direction { get; internal set; }

    public string GetIdentifier() => this.Id;
  }
}
