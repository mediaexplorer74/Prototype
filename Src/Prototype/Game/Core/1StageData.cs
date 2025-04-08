
// Type: GameManager.Core.InstData
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using Microsoft.Xna.Framework;

#nullable disable
namespace GameManager.Core
{
  public class InstData
  {
    public string Id;
    public Point Pos;

    public InstData(string id, Point pos)
    {
      this.Id = id;
      this.Pos = pos;
    }
  }
}
