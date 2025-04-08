
// Type: GameManager.Core.ChoiceData
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using Microsoft.Xna.Framework;
using GameManager.Utility;

#nullable disable
namespace GameManager.Core
{
  public class ChoiceData : IDescriptor
  {
    public string ChoiceA;
    public string ChoiceB;
    public Color ColorA;
    public Color ColorB;
    public ChoiceMakerHappens Happens;
    public string Id;

    public ChoiceData(
      string id,
      string a,
      string b,
      Color cA,
      Color cB,
      ChoiceMakerHappens happens = ChoiceMakerHappens.AtStart)
    {
      this.Id = id;
      this.ChoiceA = a;
      this.ColorA = cA;
      this.ChoiceB = b;
      this.ColorB = cB;
      this.Happens = happens;
    }

    public string GetIdentifier() => this.Id;
  }
}
