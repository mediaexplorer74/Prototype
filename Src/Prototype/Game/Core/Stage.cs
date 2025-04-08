
// Type: GameManager.Core.Stage
// Assembly: GameManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F9274EB-43B6-4682-B722-53DDCB9FD0AE
// Assembly location: C:\Users\Admin\Desktop\RE\Prototype\GameManager.exe

using Microsoft.Xna.Framework;
using GameManager.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace GameManager.Core
{
  public class Stage
  {
    public const float GRAVITY_BASE = 0.5f;
    public List<Actor> Actors = new List<Actor>();
    public float backgroundtransformation;
    public List<ScrollingBgObject> BgObjects = new List<ScrollingBgObject>();
    public Camera2D Camera;
    public bool ChangingWorld;
    public bool ChoiceMade;
    public LevelData Data;
    public float GravityMod = 1f;
    public string LevelId;
    private double lifeTime;
    public string NextLevelId;
    public List<Platform> Platforms = new List<Platform>();
    public Player Player;
    public Point Spawn;

    public void LoadLevel(string id)
    {
      this.Player = (Player) null;
      this.GravityMod = 1f;
      this.Platforms.Clear();
      this.Actors.Clear();
      this.BgObjects.Clear();
      if (this.Camera == null)
        this.Camera = new Camera2D(Game1.graphics.GraphicsDevice.Viewport);
      this.LevelId = id;
      try
      {
        this.Data = Game1.Resources.LevelDatas[id];
      }
      catch (KeyNotFoundException ex)
      {
        Debug.WriteLine("[i] Stage - Load Lever error: " + ex.Message + 
            ". Trying plan B...");

        // Plan B
        this.Data = Game1.Resources.LevelDatas["Level1"];
        Game1.PlayerChoseBouncy = false;
        Game1.PlayerChoseIcy = false;
        Game1.PlayerHasDash = false;
        Game1.PlayerHasDoubleJump = false;
        Game1.PlayerHasFloat = false;
        Game1.PlayerHasWallJump = false;
        this.ChoiceMade = false;
        Game1.SorryScreen.Activate();
      }
      this.Spawn = new Point(this.Data.PlayerPos.X, Options.RESOLUTION_DEFAULT.Y - this.Data.PlayerPos.Y);
      this.Player = new Player("Player", this.Spawn);
      this.Player.Initialize(this);
      this.GravityMod = this.Data.GravityModifier;
      if (this.Data.ChoiceId != null)
        this.ChoiceMade = false;
      foreach (InstData instData in this.Data.Objects)
      {
        GameObjectData gameObjectData = Game1.Resources.GameObjectDatas[instData.Id];
        if (gameObjectData.Type == GameObjectType.Platform)
        {
          Platform platform1 = new Platform();
          platform1.ObjectId = gameObjectData.Id;
          platform1.Graphic = GraphicData.GenerateGraphic(gameObjectData.GraphicId);
          platform1.Position = new Rectangle(new Point(instData.Pos.X, Options.RESOLUTION_DEFAULT.Y - gameObjectData.Size.Y - instData.Pos.Y), gameObjectData.Size);
          platform1.Type = gameObjectData.PlatformType;
          platform1.StartingPos = instData.Pos;
          platform1.StartingDir = gameObjectData.Direction;
          platform1.Moving = gameObjectData.Moving;
          platform1.MoveRange = gameObjectData.MoveRange;
          platform1.Direction = gameObjectData.Direction;
          Platform platform2 = platform1;
          platform2.owner = this;
          this.Platforms.Add(platform2);
        }
        if (gameObjectData.Type == GameObjectType.Actor)
        {
          Actor actor1 = new Actor();
          actor1.ObjectId = gameObjectData.Id;
          actor1.Graphic = GraphicData.GenerateGraphic(gameObjectData.GraphicId);
          actor1.Position = new Rectangle(instData.Pos, gameObjectData.Size);
          Actor actor2 = actor1;
          actor2.ActualCoords.X = (float) actor2.Position.X;
          actor2.ActualCoords.Y = (float) (actor2.Position.Y - Options.RESOLUTION_DEFAULT.Y);
          actor2.LastPosition = actor2.Position;
          this.Actors.Add(actor2);
        }
      }
      Random random = new Random();
      int num1 = random.Next(6, 8);
      for (int index = 0; index < num1 * 3; ++index)
      {
        int num2 = random.Next(3, 15);
        this.BgObjects.Add(new ScrollingBgObject(Game1.Resources.Sprites["cloud" + (object) random.Next(0, 5)], new Vector2((float) random.Next(0, Options.RESOLUTION_DEFAULT.X), (float) random.Next(-10, Options.RESOLUTION_DEFAULT.Y - 100)), (float) (random.NextDouble() * 0.4) + 0.1f, new Point(num2, num2), 3f + (float) (random.NextDouble() * 15.0)));
      }
      for (int index = 0; index < num1; ++index)
      {
        int num3 = random.Next(20, 30);
        this.BgObjects.Add(new ScrollingBgObject(Game1.Resources.Sprites["hill" + (object) random.Next(0, 3)], new Vector2((float) random.Next(20, Options.RESOLUTION_DEFAULT.X + 20), (float) Options.RESOLUTION_DEFAULT.Y), 0.85f + (float) (random.NextDouble() * 0.15), new Point(num3, num3), 0.2f + (float) (random.NextDouble() * 0.800000011920929)));
      }
      this.NextLevelId = this.Data.NextLevelId;
      Game1.ChoiceMaker.Load(this.Data.ChoiceId);
    }

    public void Update(double timeStep)
    {
      this.ChangingWorld = false;
      foreach (GameObject platform in this.Platforms)
        platform.Update(timeStep);
      this.Player.Update(timeStep);
      foreach (GameObject actor in this.Actors)
        actor.Update(timeStep);
      this.Camera.Update(this.Player);
    }

    public void DrawBackground(double timeStep)
    {
      this.lifeTime += timeStep;
      Game1.Resources.Sprites["sun"].Draw(y: Options.RESOLUTION_DEFAULT.Y, xScale: 22, yScale: 22, rotation: (float) (this.lifeTime * Math.PI / 50.0), opacity: 0.25f, centered: true);
      foreach (ScrollingBgObject bgObject in this.BgObjects)
        bgObject.Draw(timeStep);
    }

    public void DrawForeground(double timeStep)
    {
      foreach (GameObject platform in this.Platforms)
        platform.Draw(timeStep);
      foreach (GameObject actor in this.Actors)
        actor.Draw(timeStep);
      this.Player.Draw(timeStep);
    }

    public void LoadNextLevel()
    {
      string nextLevelId = this.NextLevelId;
      this.ChangingWorld = true;
      this.LoadLevel(nextLevelId);
      try
      {
        Game1.ChoiceMaker.Load(Game1.Resources.LevelDatas[nextLevelId].ChoiceId);
      }
      catch
      {
      }
    }
  }
}
