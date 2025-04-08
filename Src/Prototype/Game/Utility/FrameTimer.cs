
// Type: GameManager.Utility.FrameTimer
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using System.Diagnostics;

#nullable disable
namespace GameManager.Utility
{
  public class FrameTimer
  {
    private readonly Stopwatch _stopwatch;
    private readonly int _updateMs;
    private long _accumulator;
    private long _totalElapsedMs;
    private int _updateCount;
    public long LastMsDelta = -1;
    public double UpdatesPerSecond = -1.0;

    public FrameTimer(int updateMs = 2000)
    {
      this._stopwatch = new Stopwatch();
      this._updateMs = updateMs;
    }

    public void Start()
    {
      this._stopwatch.Start();
      this._totalElapsedMs = this._stopwatch.ElapsedMilliseconds;
    }

    public void Stop()
    {
      this._stopwatch.Stop();
      this._totalElapsedMs = this._stopwatch.ElapsedMilliseconds;
    }

    public void Update()
    {
      ++this._updateCount;
      this.LastMsDelta = this._stopwatch.ElapsedMilliseconds - this._totalElapsedMs;
      this._totalElapsedMs += this.LastMsDelta;
      this._accumulator += this.LastMsDelta;
      if (this._accumulator <= (long) this._updateMs)
        return;
      this.UpdatesPerSecond = (double) this._updateCount / (double) this._accumulator;
      this._accumulator = 0L;
      this._updateCount = 0;
    }
  }
}
