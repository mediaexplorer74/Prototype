
// Type: GameManager.Core.RestraintMessageScreen
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using Microsoft.Xna.Framework;
using GameManager.Utility;
using System.Collections.Generic;

#nullable disable
namespace GameManager.Core
{
  public class RestraintMessageScreen
  {
    private static readonly Color dimColor = new Color(0.0f, 0.0f, 0.0f) * 0.667f;
    public bool Active;
    public List<ChoiceButton> Buttons = new List<ChoiceButton>();

    public RestraintMessageScreen()
    {
      this.Buttons.Add(new ChoiceButton("You have completed this path.", new Point(60, 60), Color.Green));
      this.Buttons.Add(new ChoiceButton("You have been returned to Level 1.", new Point(60, 160), Color.Green));
      this.Buttons.Add(new ChoiceButton("Thank you for playing Choices.", new Point(60, 260), Color.Green));
    }

    public void Activate() => this.Active = true;

    public bool Update(double timeStep)
    {
      if (!this.Active)
        return false;
      foreach (ChoiceButton button in this.Buttons)
      {
        if (button.Update())
        {
          this.PerformButtonCallback(button.Label);
          break;
        }
      }
      return true;
    }

    private void PerformButtonCallback(string label) => this.Active = false;

    public void Draw(double timeStep)
    {
      if (!this.Active)
        return;
      Game1.DrawRectangle(Point.Zero, Options.RESOLUTION_DEFAULT, RestraintMessageScreen.dimColor);
      
      foreach (ChoiceButton button in this.Buttons)
        button.Draw();
    }
  }
}
