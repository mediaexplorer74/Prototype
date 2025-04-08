
// Type: GameManager.Utility.GraphicData
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using System;

#nullable disable
namespace GameManager.Utility
{
  public static class GraphicData
  {
    public static IGraphic GenerateGraphic(string graphicId)
    {
      switch (Game1.Resources.GraphicDatas[graphicId])
      {
        case BasicGraphicData _:
          return (IGraphic) new BasicGraphic(graphicId);
        case SimpleAnimGraphic _:
          return (IGraphic) new SimpleAnimGraphic(graphicId);
        case RectangleGraphicData _:
          return (IGraphic) new RectangleGraphic(graphicId);
        default:
          throw new NotImplementedException(string.Format("Graphic ID \"{0}\" was not supported by GenerateGraphic(string)!", (object) graphicId));
      }
    }
  }
}
