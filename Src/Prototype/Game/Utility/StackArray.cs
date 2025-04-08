
// Type: GameManager.Utility.StackArray`1
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using System;

#nullable disable
namespace GameManager.Utility
{
  public class StackArray<T> : IDisposable
  {
    private T[] _arr;
    private int _head;

    public StackArray(int capacity, int padding = 2)
    {
      this.Length = capacity;
      if (padding < 2)
        padding = 2;
      this._arr = new T[capacity + padding];
    }

    public int Length { get; internal set; }

    public bool Disposed { get; internal set; }

    public T this[int i]
    {
      get
      {
        return this._head + i >= this.Length ? this._arr[this._head + i - this.Length] : this._arr[this._head + i];
      }
      set
      {
        if (i >= this.Length)
          throw new IndexOutOfRangeException();
        if (this._head + i >= this._arr.Length)
          this._arr[this._head + i - this._arr.Length] = value;
        else
          this._arr[this._head + i] = value;
      }
    }

    public void Dispose()
    {
      if (this.Disposed)
        return;
      this._arr = (T[]) null;
      this._head = 0;
      this.Disposed = true;
    }

    public void Insert(T newItem)
    {
      --this._head;
      if (this._head < 0)
        this._head += this.Length;
      this._arr[this._head] = newItem;
    }
  }
}
