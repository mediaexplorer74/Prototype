
// Type: GameManager.Core.GameObject
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using Microsoft.Xna.Framework;
using GameManager.Utility;
using System;

#nullable disable
namespace GameManager.Core
{
  public class GameObject
  {
    public IGraphic Graphic;
    public Rectangle Position;

    public GameObject()
    {
    }

    public GameObject(IGraphic graphic, Rectangle position)
    {
      this.ObjectId = "CUSTOM";
      this.Graphic = graphic;
      this.Position = position;
    }

    public GameObject(string id, Point position)
    {
      GameObjectData gameObjectData = Game1.Resources.GameObjectDatas[id];
      this.Graphic = GraphicData.GenerateGraphic(gameObjectData.GraphicId);
      this.Position = new Rectangle(position, gameObjectData.Size);
    }

    public string ObjectId { get; internal set; }

    public Point Center
    {
      get => this.Position.Center;
      set
      {
        this.Position.X += value.X - this.Position.Center.X;
        this.Position.Y += value.Y - this.Position.Center.Y;
      }
    }

    public static GameObject GenerateObject(string id, Point position)
    {
      GameObjectData gameObjectData = Game1.Resources.GameObjectDatas[id];
      if (gameObjectData.Type == GameObjectType.Platform)
      {
        Platform platform = new Platform();
        platform.ObjectId = gameObjectData.Id;
        platform.Graphic = GraphicData.GenerateGraphic(gameObjectData.GraphicId);
        platform.Position = new Rectangle(position, gameObjectData.Size);
        return (GameObject) platform;
      }
      if (gameObjectData.Type != GameObjectType.Actor)
        throw new Exception("Could not parse the object!");
      Actor actor = new Actor();
      actor.ObjectId = gameObjectData.Id;
      actor.Graphic = GraphicData.GenerateGraphic(gameObjectData.GraphicId);
      actor.Position = new Rectangle(position, gameObjectData.Size);
      GameObject gameObject = (GameObject) actor;
      (gameObject as Actor).ActualCoords.X = (float) position.X;
      (gameObject as Actor).ActualCoords.Y = (float) position.Y;
      (gameObject as Actor).LastPosition = gameObject.Position;
      return gameObject;
    }

    public virtual void Update(double timeStep)
    {
    }

    public virtual void Draw(double timeStep)
    {
      this.Graphic.Draw(this.Position.X, this.Position.Y, timeStep);
    }
  }
}
