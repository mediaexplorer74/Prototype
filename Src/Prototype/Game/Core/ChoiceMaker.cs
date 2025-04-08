
// Type: GameManager.Core.ChoiceMaker
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using Microsoft.Xna.Framework;
using GameManager.Utility;
using System;
using System.Collections.Generic;

#nullable disable
namespace GameManager.Core
{
  public class ChoiceMaker
  {
    private static readonly Color dimColor = new Color(0.0f, 0.0f, 0.0f) * 0.667f;
    public bool Active;
    public List<ChoiceButton> Buttons = new List<ChoiceButton>();
    public bool Initialized;

    public void Load(string dataId)
    {
      this.Buttons.Clear();
      this.Initialized = false;
      if (dataId == null)
        return;
      ChoiceData choiceMakerData = Game1.Resources.ChoiceMakerDatas[dataId];
      this.Buttons.Add(new ChoiceButton(choiceMakerData.ChoiceA, new Point(60, 60), choiceMakerData.ColorA));
      this.Buttons.Add(new ChoiceButton(choiceMakerData.ChoiceB, new Point(60, 160), choiceMakerData.ColorB));
      if (choiceMakerData.Happens == ChoiceMakerHappens.AtStart)
        this.Active = true;
      this.Initialized = true;
    }

    public bool Update(double timeStep)
    {
      if (!this.Active || !this.Initialized)
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

    private void PerformButtonCallback(string label)
    {
      this.Active = false;
      Game1.CurrentStage.ChoiceMade = true;
      switch (label)
      {
        case "Double Jump":
          Game1.PlayerHasDoubleJump = true;
          Game1.CurrentStage.LoadLevel("Level2a1");
          break;
        case "Dash (Use J/K)":
          Game1.PlayerHasDash = true;
          Game1.CurrentStage.LoadLevel("Level2b1");
          break;
        case "Bouncy Platforms":
          Game1 .PlayerChoseBouncy = true;
          if(Game1.PlayerHasDoubleJump)
          {
            Game1.CurrentStage.LoadLevel("Level3aa1");
            break;
          }
          if (!Game1.PlayerHasDash)
            break;
          Game1.CurrentStage.LoadLevel("Level3ba1");
          break;
        case "Icy Platforms":
          Game1.PlayerChoseIcy = true;
          if (Game1.PlayerHasDoubleJump)
          {
            Game1.CurrentStage.LoadLevel("Level3ab1");
            break;
          }
          if (!Game1.PlayerHasDash)
            break;
          Game1.CurrentStage.LoadLevel("Level3bb1");
          break;
        case "Wall Jump":
          if (Game1.PlayerHasDoubleJump && Game1.PlayerChoseBouncy)
          {
            Game1.CurrentStage.LoadLevel("Level4aaa1");
            Game1.PlayerHasWallJump = true;
            break;
          }
          if (Game1.PlayerHasDoubleJump && Game1.PlayerChoseIcy)
          {
            Game1.CurrentStage.LoadLevel("Level4aba1");
            Game1.PlayerHasWallJump = true;
            break;
          }
          if (Game1.PlayerHasDash && Game1.PlayerChoseBouncy)
          {
            Game1.CurrentStage.LoadLevel("Level4baa1");
            Game1.PlayerHasWallJump = true;
            break;
          }
          if (!Game1.PlayerHasDash || !Game1.PlayerChoseIcy)
            break;
          Game1.CurrentStage.LoadLevel("Level4bba1");
          Game1.PlayerHasWallJump = true;
          break;
        case "Float (Hold Jump)":
          if (Game1.PlayerHasDoubleJump && Game1.PlayerChoseBouncy)
          {
            Game1.CurrentStage.LoadLevel("Level4aab1");
            Game1.PlayerHasFloat = true;
            break;
          }
          if (Game1.PlayerHasDoubleJump && Game1.PlayerChoseIcy)
          {
            Game1.CurrentStage.LoadLevel("Level4abb1");
            Game1.PlayerHasFloat = true;
            break;
          }
          if (Game1.PlayerHasDash && Game1.PlayerChoseBouncy)
          {
            Game1.CurrentStage.LoadLevel("Level4bab1");
            Game1.PlayerHasFloat = true;
            break;
          }
          if (!Game1.PlayerHasDash || !Game1.PlayerChoseIcy)
            break;
          Game1.CurrentStage.LoadLevel("Level4bbb1");
          Game1.PlayerHasFloat = true;
          break;
        default:
          throw new NotImplementedException(string.Format("\"{0}\" was not recognized as a button callback!"));
      }
    }

    public void Draw(double timeStep)
    {
      if (!this.Active || !this.Initialized)
        return;
      Game1.DrawRectangle(Point.Zero, Options.RESOLUTION_DEFAULT, ChoiceMaker.dimColor);
      foreach (ChoiceButton button in this.Buttons)
        button.Draw();
    }
  }
}
