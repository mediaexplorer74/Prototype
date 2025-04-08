
// Type: GameManager.Utility.MouseCommand
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using Microsoft.Xna.Framework;

#nullable disable
namespace GameManager.Utility
{
  public class MouseCommand : InputCommand
  {
    public MouseKeys Button;
    public Point Position;

    public MouseCommand(MouseKeys btn, InputState state, Point pos)
    {
      this.Button = btn;
      this.State = state;
      this.Position = pos;
    }
  }
}
