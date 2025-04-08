
// Type: GameManager.Utility.AudioManager
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using Microsoft.Xna.Framework.Media;

#nullable disable
namespace GameManager.Utility
{
  public class AudioManager
  {
    private string lastMusicTrack;

    public MediaState BGMState => MediaPlayer.State;

    public void PlayMusic(string track = null)
    {
      if (track != null && this.lastMusicTrack != track)
      {
        MediaPlayer.IsRepeating = true;
        MediaPlayer.Volume = Game1.Options.BgmVolume;
        MediaPlayer.Play(Game1.Resources.Songs[track]);
        this.lastMusicTrack = track;
      }
      if (MediaPlayer.State != MediaState.Paused)
        return;
      MediaPlayer.Resume();
    }

    public void PauseMusic()
    {
      if (MediaPlayer.State != MediaState.Playing)
        return;
      MediaPlayer.Pause();
    }

    public void PlaySFX(string track, float volumeMod = 1f, float pitch = 1f)
    {
      Game1.Resources.SoundEffects[track].Play(Game1.Options.SfxVolume * volumeMod, pitch, 0.0f);
    }
  }
}
