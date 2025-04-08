
// Type: GameManager.Core.ChoiceButton
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using Microsoft.Xna.Framework;
using GameManager.Utility;

#nullable disable
namespace GameManager.Core
{
  public class ChoiceButton
  {
    private static Color buttonRectColor = new Color(45, 45, 45);
    private readonly Color _hoverColor;
    private readonly Color _pressColor;
    private bool _hovering;
    private bool _pressing;
    private Color textColor;

    public ChoiceButton(string label, Point pos, Color hoverColor)
    {
      this.Label = label;
      Vector2 vector2 = Game1.Resources.SpriteFonts["DefaultFont"].MeasureString(label);
      this.Position = new Rectangle(pos, new Point((int) vector2.X, (int) vector2.Y));
      this._hoverColor = hoverColor;
      this._pressColor = new Color((int) hoverColor.R + 25, (int) hoverColor.G + 25, (int) hoverColor.B + 25);
    }

    public Rectangle Position { get; }

    public string Label { get; }

    public bool Update()
    {
      this._hovering = false;
      this._pressing = false;
      if (this.Position.Contains(Game1.InputHandler.MousePosition))
      {
        this._hovering = true;
        foreach (MouseCommand mouseCommand in Game1.InputHandler.MouseCommands)
        {
          if (mouseCommand.Button == MouseKeys.LeftButton)
          {
            switch (mouseCommand.State)
            {
              case InputState.Pressed:
              case InputState.Held:
                this._pressing = true;
                continue;
              case InputState.Released:
                return true;
              default:
                continue;
            }
          }
        }
      }
      return false;
    }

    public void Draw()
    {
      this.textColor = !this._pressing ? (!this._hovering ? Color.White : this._hoverColor) : this._pressColor;
      Game1.SpriteBatch.DrawString(Game1.Resources.SpriteFonts["DefaultFont"], 
          this.Label, new Vector2((float) this.Position.X, (float) this.Position.Y), this.textColor);
    }
  }
}
