
// Type: GameManager.Core.LevelData
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using Microsoft.Xna.Framework;
using GameManager.Utility;

#nullable disable
namespace GameManager.Core
{
  public class LevelData : IDescriptor
  {
    public string ChoiceId;
    public float GravityModifier = 1f;
    public string LevelId;
    public string NextLevelId;
    public InstData[] Objects;
    public Point PlayerPos;

    public LevelData(
      string id,
      string choiceId,
      Point playerPos,
      string nextLevelId,
      float gravity = 1f,
      params InstData[] objects)
    {
      this.LevelId = id;
      this.ChoiceId = choiceId;
      this.PlayerPos = playerPos;
      this.NextLevelId = nextLevelId;
      this.GravityModifier = gravity;
      this.Objects = objects;
    }

    public LevelData(
      string id,
      string choiceId,
      Point playerPos,
      string nextLevelId,
      InstData[] objects,
      float gravity = 1f)
    {
      this.LevelId = id;
      this.ChoiceId = choiceId;
      this.PlayerPos = playerPos;
      this.NextLevelId = nextLevelId;
      this.GravityModifier = gravity;
      this.Objects = objects;
    }

    public string GetIdentifier() => this.LevelId;
  }
}
