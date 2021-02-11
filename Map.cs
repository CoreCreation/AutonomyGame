// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.Map
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace AutonomyTheGame.Autonomy0._1
{
  [Serializable]
  public class Map
  {
    private Tile[] tileArray;
    protected Tile[,] tiles;
    public int TileY;
    public int TileX;
    private List<Tile> spawns;
    [NonSerialized]
    private ContentManager content;
    [NonSerialized]
    private Game1 game;
    [NonSerialized]
    private TileFactory tileFactory;
    [NonSerialized]
    private DecorationFactory decorationFactory;
    [NonSerialized]
    private PickupFactory pickupFactory;
    [NonSerialized]
    private Tile boundaryTile;
    [NonSerialized]
    private int yMinFus;
    [NonSerialized]
    private int yMaxFus;
    [NonSerialized]
    private int xMinFus;
    [NonSerialized]
    private int xMaxFus;
    [NonSerialized]
    private Vector2 playerSpawn;
    [NonSerialized]
    private List<Tile> particleTiles;

    public List<Tile> Spawn => this.spawns;

    public float xMax => this.tiles[this.TileY, this.TileX].LeftBound;

    public float xMin => this.tiles[0, 0].RightBound;

    public Vector2 PlayerSpawn => this.playerSpawn;

    public Map(Tile[,] array)
    {
      this.tiles = array;
      this.TileY = this.tiles.GetUpperBound(0);
      this.TileX = this.tiles.GetUpperBound(1);
    }

    public void Instil(Game1 game, ContentManager content)
    {
      this.game = game;
      this.content = content;
      this.tileFactory = new TileFactory(content);
      this.decorationFactory = new DecorationFactory(content);
      this.pickupFactory = new PickupFactory(content);
      this.boundaryTile = this.tileFactory.GetTile(TileType.Metal, 0.0f, 0.0f, false, false, false, false);
      this.Fluff();
      this.particleTiles = new List<Tile>();
      for (int index1 = 0; index1 <= this.TileY; ++index1)
      {
        for (int index2 = 0; index2 <= this.TileX; ++index2)
        {
          Tile tile = this.tiles[index1, index2];
          this.tiles[index1, index2] = this.tileFactory.GetTile(tile.TileType, (float) index2, (float) index1, tile.BorderTop, tile.BorderBottom, tile.BorderLeft, tile.BorderRight);
          if (tile.Decoration != null)
            this.tiles[index1, index2].SetDecoration(this.decorationFactory.GetDecoration(tile.DecorationType, (float) index2, (float) index1, tile.Decoration.BorderTop, tile.Decoration.BorderBottom, tile.Decoration.BorderLeft, tile.Decoration.BorderRight));
          if (tile.Pickup != null)
            this.tiles[index1, index2].SetPickup(this.pickupFactory.GetPickup(tile.PickUpType, (float) index2, (float) index1));
          if (this.tiles[index1, index2].Particle != null)
            this.particleTiles.Add(this.tiles[index1, index2]);
        }
      }
      this.LightEffectsDown();
      this.LightEffectsUp();
      this.LightEffectsRight();
      this.LightEffectsLeft();
      this.GetSpawns();
    }

    public void Update(Camera camera)
    {
      TileLocation tileLocation1 = new TileLocation(camera.Position.X - camera.Width * camera.Zoom, camera.Position.Y - camera.Height * camera.Zoom);
      TileLocation tileLocation2 = new TileLocation(camera.Position.X + camera.Width / camera.Zoom, camera.Position.Y + camera.Height / camera.Zoom);
      this.yMinFus = tileLocation1.Y;
      this.yMaxFus = tileLocation2.Y;
      this.xMinFus = tileLocation1.X;
      this.xMaxFus = tileLocation2.X;
      for (int yMinFus = this.yMinFus; yMinFus <= this.yMaxFus; ++yMinFus)
      {
        for (int xMinFus = this.xMinFus; xMinFus <= this.xMaxFus; ++xMinFus)
        {
          if (yMinFus >= 0 && xMinFus >= 0 && (yMinFus <= this.TileY && xMinFus <= this.TileX))
          {
            this.tiles[yMinFus, xMinFus].Update();
            if (this.tiles[yMinFus, xMinFus].Particle != null)
              this.tiles[yMinFus, xMinFus].Particle.Update();
          }
        }
      }
    }

    public void Draw(SpriteBatch sb)
    {
      for (int yMinFus = this.yMinFus; yMinFus <= this.yMaxFus; ++yMinFus)
      {
        for (int xMinFus = this.xMinFus; xMinFus <= this.xMaxFus; ++xMinFus)
        {
          if (yMinFus >= 0 && xMinFus >= 0 && (yMinFus <= this.TileY && xMinFus <= this.TileX))
            this.tiles[yMinFus, xMinFus].Draw(sb);
        }
      }
      foreach (Tile particleTile in this.particleTiles)
        particleTile.Particle.Draw(sb);
    }

    private void GetSpawns()
    {
      this.spawns = new List<Tile>();
      for (int index1 = 0; index1 <= this.TileY; ++index1)
      {
        for (int index2 = 0; index2 <= this.TileX; ++index2)
        {
          if (this.tiles[index1, index2].Spawn.HasValue)
          {
            Spawns? spawn = this.tiles[index1, index2].Spawn;
            if ((spawn.GetValueOrDefault() != Spawns.PlayerSpawn ? 0 : (spawn.HasValue ? 1 : 0)) != 0)
              this.playerSpawn = this.tiles[index1, index2].Location;
            else
              this.spawns.Add(this.tiles[index1, index2]);
          }
        }
      }
    }

    private void LightEffectsRight()
    {
      for (int index1 = 0; index1 <= this.TileY; ++index1)
      {
        int num = 0;
        for (int index2 = 0; index2 <= this.TileX; ++index2)
        {
          if (this.tiles[index1, index2].Brightness() >= num)
          {
            num = this.tiles[index1, index2].Brightness();
          }
          else
          {
            this.tiles[index1, index2].SetBrightness(num);
            num = this.tiles[index1, index2].Brightness();
          }
        }
      }
    }

    private void LightEffectsDown()
    {
      for (int index1 = 0; index1 <= this.TileX; ++index1)
      {
        int num = 0;
        for (int index2 = 0; index2 <= this.TileY; ++index2)
        {
          if (this.tiles[index2, index1].Brightness() >= num)
          {
            num = this.tiles[index2, index1].Brightness();
          }
          else
          {
            this.tiles[index2, index1].SetBrightness(num);
            num = this.tiles[index2, index1].Brightness();
          }
        }
      }
    }

    private void LightEffectsUp()
    {
      for (int tileX = this.TileX; tileX >= 0; --tileX)
      {
        int num = 0;
        for (int tileY = this.TileY; tileY >= 0; --tileY)
        {
          if (this.tiles[tileY, tileX].Brightness() >= num)
          {
            num = this.tiles[tileY, tileX].Brightness();
          }
          else
          {
            this.tiles[tileY, tileX].SetBrightness(num);
            num = this.tiles[tileY, tileX].Brightness();
          }
        }
      }
    }

    private void LightEffectsLeft()
    {
      for (int tileY = this.TileY; tileY >= 0; --tileY)
      {
        int num = 0;
        for (int tileX = this.TileX; tileX >= 0; --tileX)
        {
          if (this.tiles[tileY, tileX].Brightness() >= num)
          {
            num = this.tiles[tileY, tileX].Brightness();
          }
          else
          {
            this.tiles[tileY, tileX].SetBrightness(num);
            num = this.tiles[tileY, tileX].Brightness();
          }
        }
      }
    }

    public void ActivatePickUp(TileLocation location, Entity activator) => this.TileAt(location).Pickup.Activate(activator);

    public void ActivateTile(TileLocation location, Entity activator) => this.TileAt(location).Activate(this.game, activator);

    public void SetSpawn(Tile tile) => this.playerSpawn = tile.Location;

    public void Flatten()
    {
      int length = (this.TileY + 1) * (this.TileX + 1);
      int index1 = 0;
      this.tileArray = new Tile[length];
      for (int index2 = 0; index2 <= this.TileY; ++index2)
      {
        for (int index3 = 0; index3 <= this.TileX; ++index3)
        {
          this.tileArray[index1] = this.tiles[index2, index3];
          ++index1;
        }
      }
      this.tiles = (Tile[,]) null;
    }

    public void Fluff()
    {
      this.tiles = new Tile[this.TileY + 1, this.TileX + 1];
      int index1 = 0;
      for (int index2 = 0; index2 <= this.TileY; ++index2)
      {
        for (int index3 = 0; index3 <= this.TileX; ++index3)
        {
          this.tiles[index2, index3] = this.tileArray[index1];
          ++index1;
        }
      }
      this.tileArray = (Tile[]) null;
    }

    public TileType LocationCheck(TileLocation entityLocation)
    {
      try
      {
        return this.tiles[entityLocation.Y, entityLocation.X].TileType;
      }
      catch (IndexOutOfRangeException ex)
      {
        return TileType.Metal;
      }
    }

    public TileType LocationCheck(TileLocation entityLocation, YDirection yDirection)
    {
      try
      {
        switch (yDirection)
        {
          case YDirection.Up:
            return this.tiles[entityLocation.Y - 1, entityLocation.X].TileType;
          case YDirection.Down:
            return this.tiles[entityLocation.Y + 1, entityLocation.X].TileType;
          default:
            return TileType.Empty;
        }
      }
      catch (IndexOutOfRangeException ex)
      {
        return TileType.Empty;
      }
    }

    public TileType LocationCheck(TileLocation entityLocation, XDirection xDirection)
    {
      try
      {
        switch (xDirection)
        {
          case XDirection.Right:
            return this.tiles[entityLocation.Y, entityLocation.X + 1].TileType;
          case XDirection.Left:
            return this.tiles[entityLocation.Y, entityLocation.X - 1].TileType;
          default:
            return TileType.Empty;
        }
      }
      catch (IndexOutOfRangeException ex)
      {
        return TileType.Empty;
      }
    }

    public Tile TileAt(TileLocation entityLocation, XDirection direction)
    {
      switch (direction)
      {
        case XDirection.Right:
          return this.tiles[entityLocation.Y, entityLocation.X + 1];
        case XDirection.Left:
          return this.tiles[entityLocation.Y, entityLocation.X - 1];
        default:
          throw new Exception("Invaild use of TileAt Function. Check Argument");
      }
    }

    public Tile TileAt(int y, int x) => this.tiles[y, x];

    public Tile TileAt(TileLocation entityLocation) => entityLocation.X < 0 || entityLocation.X > this.TileX || (entityLocation.Y < 0 || entityLocation.Y > this.TileY) ? this.boundaryTile : this.tiles[entityLocation.Y, entityLocation.X];

    private bool XCheckRight(int x) => x + 1 > this.TileX;

    private bool XCheckLeft(int x) => x - 1 < 0;

    private bool YCheckDown(int y) => y + 1 > this.TileY;

    private bool YCheckUp(int y) => y - 1 < 0;

    public float? YObstructionCheck(YDirection direction, TileLocation[] vectors)
    {
      float? nullable1 = new float?();
      switch (direction)
      {
        case YDirection.Up:
          float num1 = 0.0f;
          for (int index = 0; index < vectors.Length; ++index)
          {
            if (this.YCheckUp(vectors[index].Y))
              return new float?(num1);
          }
          for (int index = 0; index < vectors.Length; ++index)
          {
            nullable1 = !this.tiles[vectors[index].Y - 1, vectors[index].X].Collision ? new float?() : new float?(this.tiles[vectors[index].Y - 1, vectors[index].X].BotBound);
            if (nullable1.HasValue)
            {
              float? nullable2 = nullable1;
              float num2 = num1;
              if (((double) nullable2.GetValueOrDefault() <= (double) num2 ? 0 : (nullable2.HasValue ? 1 : 0)) != 0)
                num1 = nullable1.Value;
            }
          }
          return new float?(num1);
        case YDirection.Down:
          float botBound = this.tiles[this.TileY, this.TileX].BotBound;
          for (int index = 0; index < vectors.Length; ++index)
          {
            if (this.YCheckDown(vectors[index].Y))
              return new float?(botBound);
          }
          for (int index = 0; index < vectors.Length; ++index)
          {
            nullable1 = !this.tiles[vectors[index].Y + 1, vectors[index].X].Collision ? new float?() : new float?(this.tiles[vectors[index].Y + 1, vectors[index].X].TopBound);
            if (nullable1.HasValue)
            {
              float? nullable2 = nullable1;
              float num2 = botBound;
              if (((double) nullable2.GetValueOrDefault() >= (double) num2 ? 0 : (nullable2.HasValue ? 1 : 0)) != 0)
                botBound = nullable1.Value;
            }
          }
          return new float?(botBound);
        default:
          throw new Exception("Fool!");
      }
    }

    public float? XObstructionCheck(XDirection direction, TileLocation[] vectors)
    {
      float? nullable1 = new float?();
      switch (direction)
      {
        case XDirection.Right:
          float rightBound = this.tiles[this.TileY, this.TileX].RightBound;
          for (int index = 0; index < vectors.Length; ++index)
          {
            if (this.XCheckRight(vectors[index].X))
              return new float?(rightBound);
          }
          for (int index = 0; index < vectors.Length; ++index)
          {
            nullable1 = !this.tiles[vectors[index].Y, vectors[index].X + 1].Collision ? new float?() : new float?(this.tiles[vectors[index].Y, vectors[index].X + 1].LeftBound);
            if (nullable1.HasValue)
            {
              float? nullable2 = nullable1;
              float num = rightBound;
              if (((double) nullable2.GetValueOrDefault() >= (double) num ? 0 : (nullable2.HasValue ? 1 : 0)) != 0)
                rightBound = nullable1.Value;
            }
          }
          return new float?(rightBound);
        case XDirection.Left:
          float num1 = 0.0f;
          for (int index = 0; index < vectors.Length; ++index)
          {
            if (this.XCheckLeft(vectors[index].X))
              return new float?(num1);
          }
          for (int index = 0; index < vectors.Length; ++index)
          {
            nullable1 = !this.tiles[vectors[index].Y, vectors[index].X - 1].Collision ? new float?() : new float?(this.tiles[vectors[index].Y, vectors[index].X - 1].RightBound);
            if (nullable1.HasValue)
            {
              float? nullable2 = nullable1;
              float num2 = num1;
              if (((double) nullable2.GetValueOrDefault() <= (double) num2 ? 0 : (nullable2.HasValue ? 1 : 0)) != 0)
                num1 = nullable1.Value;
            }
          }
          return new float?(num1);
        default:
          throw new Exception("Fool!");
      }
    }
  }
}
