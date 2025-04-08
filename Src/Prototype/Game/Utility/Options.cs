
// Type: GameManager.Utility.Options
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using Microsoft.Xna.Framework;

#nullable disable
namespace GameManager.Utility
{
  public class Options
  {
    public static Point RESOLUTION_DEFAULT = new Point(960, 540);
    public static Point RESOLUTION_LARGE = new Point(1920, 1080);
    public float BgmVolume = 0.65f;
    public bool CanHandleLargeReso = true;
    public bool LargeResolution;
    public bool PlayBgm = true;
    public bool PlaySfx = true;
    public float SfxVolume = 0.4f;

    public int GetResolutionScaleFactor() => this.LargeResolution ? 2 : 1;

    public int GetBackBufferWidth()
    {
      return this.LargeResolution ? Options.RESOLUTION_LARGE.X : Options.RESOLUTION_DEFAULT.X;
    }

    public int GetBackBufferHeight()
    {
      return this.LargeResolution ? Options.RESOLUTION_LARGE.Y : Options.RESOLUTION_DEFAULT.Y;
    }
  }
}
