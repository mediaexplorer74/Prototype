
// Type: GameManager.Utility.KeyboardCommand
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using Microsoft.Xna.Framework.Input;

#nullable disable
namespace GameManager.Utility
{
  public class KeyboardCommand : InputCommand
  {
    public Keys Button;

    public KeyboardCommand(Keys btn, InputState state)
    {
      this.Button = btn;
      this.State = state;
    }
  }
}
