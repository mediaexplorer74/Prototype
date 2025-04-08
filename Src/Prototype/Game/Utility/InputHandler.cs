
// Type: GameManager.Utility.InputHandler
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

#nullable disable
namespace GameManager.Utility
{
  public class InputHandler : IDisposable
  {
    public StackArray<KeyboardState> KeyboardHistory;
    public List<Keys> KeyboardListeners;
    public StackArray<MouseState> MouseHistory;
    public List<MouseKeys> MouseListeners;

    public InputHandler(int framesToKeep = 3)
    {
      this.MouseHistory = new StackArray<MouseState>(framesToKeep, (int) ((double) framesToKeep * 0.05));
      this.KeyboardHistory = new StackArray<KeyboardState>(framesToKeep, (int) ((double) framesToKeep * 0.05));
      this.MouseListeners = new List<MouseKeys>(Enum.GetValues(typeof (MouseKeys)).Length);
      this.KeyboardListeners = new List<Keys>(Enum.GetValues(typeof (Keys)).Length);
    }

    public bool Disposed { get; internal set; }

    public Point MousePosition { get; internal set; }

    public List<MouseCommand> MouseCommands { get; internal set; } = new List<MouseCommand>();

    public List<KeyboardCommand> KeyCommands { get; internal set; } = new List<KeyboardCommand>();

    public void Dispose()
    {
      if (this.Disposed)
        return;
      this.MouseHistory.Dispose();
      this.KeyboardHistory.Dispose();
      this.MouseListeners.Clear();
      this.KeyboardListeners.Clear();
      this.MouseCommands.Clear();
      this.KeyCommands.Clear();
      this.Disposed = true;
    }

    public void RegisterMouseListener(MouseKeys key)
    {
      if (this.Disposed)
        throw new ObjectDisposedException("Attempted to access InputHandler after object is disposed!");
      if (this.MouseListeners.Contains(key))
        return;
      this.MouseListeners.Add(key);
    }

    public void UnregisterMouseListener(MouseKeys key)
    {
      if (this.Disposed)
        throw new ObjectDisposedException("Attempted to access InputHandler after object is disposed!");
      this.MouseListeners.Remove(key);
    }

    public void RegisterKeyboardListener(Keys key)
    {
      if (this.Disposed)
        throw new ObjectDisposedException("Attempted to access InputHandler after object is disposed!");
      if (this.KeyboardListeners.Contains(key))
        return;
      this.KeyboardListeners.Add(key);
    }

    public void UnregisterKeyboardListener(Keys key)
    {
      if (this.Disposed)
        throw new ObjectDisposedException("Attempted to access InputHandler after object is disposed!");
      this.KeyboardListeners.Remove(key);
    }

    public void Update(MouseState mState, KeyboardState kState)
    {
      if (this.Disposed)
        throw new ObjectDisposedException("Attempted to access InputHandler after object is disposed!");
      this.MouseHistory.Insert(mState);
      this.KeyboardHistory.Insert(kState);
    }

    public void GenerateCommands()
    {
      if (this.Disposed)
        throw new ObjectDisposedException("Attempted to access InputHandler after object is disposed!");
      this.MouseCommands.Clear();
      this.KeyCommands.Clear();
      this.MousePosition = this.MouseHistory[0].Position;
      for (byte index = 0; (int) index < this.MouseListeners.Count; ++index)
      {
        switch (this.MouseListeners[(int) index])
        {
          case MouseKeys.LeftButton:
            this.HandleLeftMouseButton();
            break;
          case MouseKeys.RightButton:
            this.HandleRightMouseButton();
            break;
          case MouseKeys.MiddleButton:
            this.HandleMiddleMouseButton();
            break;
        }
      }
      for (short index = 0; (int) index < this.KeyboardListeners.Count; ++index)
        this.HandleKeyboardButton(this.KeyboardListeners[(int) index]);
    }

    private void HandleLeftMouseButton()
    {
      if (this.MouseHistory[0].LeftButton == ButtonState.Pressed)
      {
        if (this.MouseHistory[1].LeftButton == ButtonState.Pressed)
          this.MouseCommands.Add(new MouseCommand(MouseKeys.LeftButton, InputState.Held, this.MouseHistory[0].Position));
        else
          this.MouseCommands.Add(new MouseCommand(MouseKeys.LeftButton, InputState.Pressed, this.MouseHistory[0].Position));
      }
      else
      {
        if (this.MouseHistory[1].LeftButton != ButtonState.Pressed)
          return;
        this.MouseCommands.Add(new MouseCommand(MouseKeys.LeftButton, InputState.Released, this.MouseHistory[0].Position));
      }
    }

    private void HandleRightMouseButton()
    {
      if (this.MouseHistory[0].RightButton == ButtonState.Pressed)
      {
        if (this.MouseHistory[1].RightButton == ButtonState.Pressed)
          this.MouseCommands.Add(new MouseCommand(MouseKeys.RightButton, InputState.Held, this.MouseHistory[0].Position));
        else
          this.MouseCommands.Add(new MouseCommand(MouseKeys.RightButton, InputState.Pressed, this.MouseHistory[0].Position));
      }
      else
      {
        if (this.MouseHistory[1].RightButton != ButtonState.Pressed)
          return;
        this.MouseCommands.Add(new MouseCommand(MouseKeys.RightButton, InputState.Released, this.MouseHistory[0].Position));
      }
    }

    private void HandleMiddleMouseButton()
    {
      if (this.MouseHistory[0].MiddleButton == ButtonState.Pressed)
      {
        if (this.MouseHistory[1].MiddleButton == ButtonState.Pressed)
          this.MouseCommands.Add(new MouseCommand(MouseKeys.MiddleButton, InputState.Held, this.MouseHistory[0].Position));
        else
          this.MouseCommands.Add(new MouseCommand(MouseKeys.MiddleButton, InputState.Pressed, this.MouseHistory[0].Position));
      }
      else
      {
        if (this.MouseHistory[1].MiddleButton != ButtonState.Pressed)
          return;
        this.MouseCommands.Add(new MouseCommand(MouseKeys.MiddleButton, InputState.Released, this.MouseHistory[0].Position));
      }
    }

    private void HandleKeyboardButton(Keys key)
    {
      if (this.KeyboardHistory[0].IsKeyDown(key))
      {
        if (this.KeyboardHistory[1].IsKeyDown(key))
          this.KeyCommands.Add(new KeyboardCommand(key, InputState.Held));
        else
          this.KeyCommands.Add(new KeyboardCommand(key, InputState.Pressed));
      }
      else
      {
        if (!this.KeyboardHistory[1].IsKeyDown(key))
          return;
        this.KeyCommands.Add(new KeyboardCommand(key, InputState.Released));
      }
    }
  }
}
