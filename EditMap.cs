// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.EditMap
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace AutonomyTheGame.Autonomy0._1
{
  internal class EditMap
  {
    private const TileType fillerTile = TileType.Empty;
    public TileFactory tileFactory;
    public DecorationFactory decorationFactory;
    public PickupFactory pickupFactory;
    private Game1 game;
    private CameraMap camera;
    private Texture2D lineVertical;
    private Texture2D lineHorizontal;
    private int yMinFus;
    private int yMaxFus;
    private int xMinFus;
    private int xMaxFus;
    protected List<List<Tile>> tiles;

    public int TileY => this.tiles.Count - 1;

    public int TileX => this.tiles.ElementAt<List<Tile>>(0).Count - 1;

    public EditMap(Game1 game, ContentManager content, CameraMap camera)
    {
      this.game = game;
      this.camera = camera;
      this.tileFactory = new TileFactory(content);
      this.decorationFactory = new DecorationFactory(content);
      this.pickupFactory = new PickupFactory(content);
      this.tiles = new List<List<Tile>>(300);
      for (int index1 = 0; index1 <= 10; ++index1)
      {
        this.tiles.Add(new List<Tile>(300));
        for (int index2 = 0; index2 <= 10; ++index2)
          this.tiles.ElementAt<List<Tile>>(index1).Add(this.tileFactory.GetTile(TileType.Empty, (float) index2, (float) index1, false, false, false, false));
      }
      this.GetEdge();
    }

    public void Update()
    {
      TileLocation tileLocation1 = new TileLocation(this.camera.Position.X - this.camera.Width / this.camera.Zoom, this.camera.Position.Y - this.camera.Height / this.camera.Zoom);
      TileLocation tileLocation2 = new TileLocation(this.camera.Position.X + this.camera.Width / this.camera.Zoom, this.camera.Position.Y + this.camera.Height / this.camera.Zoom);
      this.yMinFus = tileLocation1.Y;
      this.yMaxFus = tileLocation2.Y;
      this.xMinFus = tileLocation1.X;
      this.xMaxFus = tileLocation2.X;
      for (int yMinFus1 = this.yMinFus; yMinFus1 <= this.yMaxFus; ++yMinFus1)
      {
        for (int yMinFus2 = this.yMinFus; yMinFus2 <= this.xMaxFus; ++yMinFus2)
        {
          if (yMinFus1 >= 0 && yMinFus2 >= 0 && (yMinFus1 <= this.TileY && yMinFus2 <= this.TileX))
            this.tiles.ElementAt<List<Tile>>(yMinFus1).ElementAt<Tile>(yMinFus2).Update();
        }
      }
    }

    public void Draw(SpriteBatch sb)
    {
      sb.Draw(this.lineVertical, new Vector2((float) -this.lineVertical.Width, -1f), Color.White);
      sb.Draw(this.lineVertical, new Vector2((float) ((this.TileX + 1) * 256), -1f), Color.White);
      sb.Draw(this.lineHorizontal, new Vector2(-1f, (float) -this.lineHorizontal.Height), Color.White);
      sb.Draw(this.lineHorizontal, new Vector2(-1f, (float) ((this.TileY + 1) * 256)), Color.White);
      for (int yMinFus1 = this.yMinFus; yMinFus1 <= this.yMaxFus; ++yMinFus1)
      {
        for (int yMinFus2 = this.yMinFus; yMinFus2 <= this.xMaxFus; ++yMinFus2)
        {
          if (yMinFus1 >= 0 && yMinFus2 >= 0 && (yMinFus1 <= this.TileY && yMinFus2 <= this.TileX))
            this.tiles.ElementAt<List<Tile>>(yMinFus1).ElementAt<Tile>(yMinFus2).Draw(sb);
        }
      }
    }

    private void GetEdge()
    {
      this.lineHorizontal = Div.getTexture(this.game.GraphicsDevice, 0, 0, 0, (this.TileX + 1) * 256 + 2, 20);
      this.lineVertical = Div.getTexture(this.game.GraphicsDevice, 0, 0, 0, 20, (this.TileY + 1) * 256 + 2);
    }

    public void Shade()
    {
      for (int index1 = 0; index1 <= this.TileY; ++index1)
      {
        for (int index2 = 0; index2 <= this.TileX; ++index2)
          this.tiles.ElementAt<List<Tile>>(index1).ElementAt<Tile>(index2).SetBrightnessFull(this.tileFactory.GetTile(this.tiles.ElementAt<List<Tile>>(index1).ElementAt<Tile>(index2).TileType, 0.0f, 0.0f, false, false, false, false).Brightness());
      }
      this.LightEffectsDown();
      this.LightEffectsUp();
      this.LightEffectsRight();
      this.LightEffectsLeft();
    }

    public void Light()
    {
      for (int index1 = 0; index1 <= this.TileY; ++index1)
      {
        for (int index2 = 0; index2 <= this.TileX; ++index2)
          this.tiles.ElementAt<List<Tile>>(index1).ElementAt<Tile>(index2).SetBrightnessFull((int) byte.MaxValue);
      }
    }

    private void LightEffectsRight()
    {
      for (int index1 = 0; index1 <= this.TileY; ++index1)
      {
        int num = 0;
        for (int index2 = 0; index2 <= this.TileX; ++index2)
        {
          if (this.tiles.ElementAt<List<Tile>>(index1).ElementAt<Tile>(index2).Brightness() >= num)
          {
            num = this.tiles.ElementAt<List<Tile>>(index1).ElementAt<Tile>(index2).Brightness();
          }
          else
          {
            this.tiles.ElementAt<List<Tile>>(index1).ElementAt<Tile>(index2).SetBrightness(num);
            num = this.tiles.ElementAt<List<Tile>>(index1).ElementAt<Tile>(index2).Brightness();
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
          if (this.tiles.ElementAt<List<Tile>>(index2).ElementAt<Tile>(index1).Brightness() >= num)
          {
            num = this.tiles.ElementAt<List<Tile>>(index2).ElementAt<Tile>(index1).Brightness();
          }
          else
          {
            this.tiles.ElementAt<List<Tile>>(index2).ElementAt<Tile>(index1).SetBrightness(num);
            num = this.tiles.ElementAt<List<Tile>>(index2).ElementAt<Tile>(index1).Brightness();
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
          if (this.tiles.ElementAt<List<Tile>>(tileY).ElementAt<Tile>(tileX).Brightness() >= num)
          {
            num = this.tiles.ElementAt<List<Tile>>(tileY).ElementAt<Tile>(tileX).Brightness();
          }
          else
          {
            this.tiles.ElementAt<List<Tile>>(tileY).ElementAt<Tile>(tileX).SetBrightness(num);
            num = this.tiles.ElementAt<List<Tile>>(tileY).ElementAt<Tile>(tileX).Brightness();
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
          if (this.tiles.ElementAt<List<Tile>>(tileY).ElementAt<Tile>(tileX).Brightness() >= num)
          {
            num = this.tiles.ElementAt<List<Tile>>(tileY).ElementAt<Tile>(tileX).Brightness();
          }
          else
          {
            this.tiles.ElementAt<List<Tile>>(tileY).ElementAt<Tile>(tileX).SetBrightness(num);
            num = this.tiles.ElementAt<List<Tile>>(tileY).ElementAt<Tile>(tileX).Brightness();
          }
        }
      }
    }

    public void AddX()
    {
      int num1 = this.TileX + 1;
      int num2 = 0;
      foreach (List<Tile> tile in this.tiles)
      {
        tile.Add(this.tileFactory.GetTile(TileType.Empty, (float) num1, (float) num2, false, false, false, false));
        ++num2;
      }
      this.GetEdge();
    }

    public void AddXUp()
    {
      foreach (List<Tile> tile in this.tiles)
        tile.Insert(0, this.tileFactory.GetTile(TileType.Empty, 0.0f, 0.0f, false, false, false, false));
      this.RefreshTiles();
      this.camera.Move(256f, 0.0f);
      this.GetEdge();
    }

    public void AddY()
    {
      this.tiles.Add(new List<Tile>());
      for (int index = 0; index <= this.TileX; ++index)
        this.tiles.ElementAt<List<Tile>>(this.TileY).Add(this.tileFactory.GetTile(TileType.Empty, (float) index, (float) this.TileY, false, false, false, false));
      this.GetEdge();
    }

    public void AddYUp()
    {
      List<Tile> tileList = new List<Tile>();
      for (int index = 0; index <= this.TileX; ++index)
        tileList.Add(this.tileFactory.GetTile(TileType.Empty, (float) index, 0.0f, false, false, false, false));
      this.tiles.Insert(0, tileList);
      this.RefreshTiles();
      this.camera.Move(0.0f, 256f);
      this.GetEdge();
    }

    public void RemoveY()
    {
      if (this.tiles.Count <= 5)
        return;
      this.tiles.RemoveAt(this.TileY);
      this.GetEdge();
    }

    public void RemoveX()
    {
      if (this.tiles.ElementAt<List<Tile>>(0).Count <= 5)
        return;
      int tileX = this.TileX;
      foreach (List<Tile> tile in this.tiles)
        tile.RemoveAt(tileX);
      this.GetEdge();
    }

    private void CleanBorders()
    {
      for (int index1 = 0; index1 <= this.TileY; ++index1)
      {
        for (int index2 = 0; index2 <= this.TileX; ++index2)
        {
          Tile tile = this.tiles.ElementAt<List<Tile>>(index1).ElementAt<Tile>(index2);
          bool[] borders = this.GetBorders(index2, index1);
          this.tiles.ElementAt<List<Tile>>(index1).RemoveAt(index2);
          this.tiles.ElementAt<List<Tile>>(index1).Insert(index2, this.tileFactory.GetTile(tile.TileType, (float) index2, (float) index1, borders[0], borders[1], borders[2], borders[3]));
          if (tile.Decoration != null)
            this.tiles.ElementAt<List<Tile>>(index1).ElementAt<Tile>(index2).SetDecoration(this.decorationFactory.GetDecoration(tile.DecorationType, (float) index2, (float) index1, tile.Decoration.BorderTop, tile.Decoration.BorderBottom, tile.Decoration.BorderLeft, tile.Decoration.BorderRight));
          if (tile.Pickup != null)
            this.tiles.ElementAt<List<Tile>>(index1).ElementAt<Tile>(index2).SetPickup(this.pickupFactory.GetPickup(tile.PickUpType, (float) index2, (float) index1));
        }
      }
    }

    public void CleanMap()
    {
      int num1 = 0;
      for (int tileY = this.TileY; tileY >= 0; --tileY)
      {
        int num2 = 0;
        for (int index = 0; index <= this.TileX; ++index)
        {
          if (this.tiles.ElementAt<List<Tile>>(tileY).ElementAt<Tile>(index).TileType != TileType.Empty)
            ++num2;
        }
        if (num2 == 0)
          ++num1;
        else
          break;
      }
      for (; num1 > 0; --num1)
        this.RemoveY();
      int num3 = 0;
      for (int tileX = this.TileX; tileX >= 0; --tileX)
      {
        int num2 = 0;
        for (int index = 0; index <= this.TileY; ++index)
        {
          if (this.tiles.ElementAt<List<Tile>>(index).ElementAt<Tile>(tileX).TileType != TileType.Empty)
            ++num2;
        }
        if (num2 == 0)
          ++num3;
        else
          break;
      }
      for (; num3 > 0; --num3)
        this.RemoveX();
      this.CleanBorders();
    }

    private void RefreshTiles()
    {
      for (int index1 = 0; index1 <= this.TileY; ++index1)
      {
        for (int index2 = 0; index2 <= this.TileX; ++index2)
        {
          Tile tile = this.tiles.ElementAt<List<Tile>>(index1).ElementAt<Tile>(index2);
          this.tiles.ElementAt<List<Tile>>(index1).RemoveAt(index2);
          this.tiles.ElementAt<List<Tile>>(index1).Insert(index2, this.tileFactory.GetTile(tile.TileType, (float) index2, (float) index1, tile.BorderTop, tile.BorderBottom, tile.BorderLeft, tile.BorderRight));
          if (tile.Decoration != null)
            this.tiles.ElementAt<List<Tile>>(index1).ElementAt<Tile>(index2).SetDecoration(this.decorationFactory.GetDecoration(tile.DecorationType, (float) index2, (float) index1, tile.Decoration.BorderTop, tile.Decoration.BorderBottom, tile.Decoration.BorderLeft, tile.Decoration.BorderRight));
          if (tile.Pickup != null)
            this.tiles.ElementAt<List<Tile>>(index1).ElementAt<Tile>(index2).SetPickup(this.pickupFactory.GetPickup(tile.PickUpType, (float) index2, (float) index1));
        }
      }
    }

    private bool SetBorder(TileType thisT, TileType type) => thisT != type;

    public void ChangeTileAt(Vector2 click, Tile currentTile)
    {
      TileLocation tileLocation = new TileLocation(click.X, click.Y);
      if (tileLocation.X <= this.TileX && tileLocation.X >= 0 && (tileLocation.Y <= this.TileY && tileLocation.Y >= 0))
      {
        bool[] borders = this.GetBorders(tileLocation);
        this.tiles.ElementAt<List<Tile>>(tileLocation.Y).RemoveAt(tileLocation.X);
        this.tiles.ElementAt<List<Tile>>(tileLocation.Y).Insert(tileLocation.X, this.tileFactory.GetTile(currentTile.TileType, (float) tileLocation.X, (float) tileLocation.Y, borders[0], borders[1], borders[2], borders[3]));
      }
      else
        this.Expand(tileLocation);
    }

    public void AddDecoration(Vector2 click, Decoration currentDec)
    {
      TileLocation mouseTile = new TileLocation(click.X, click.Y);
      if (mouseTile.X <= this.TileX && mouseTile.X >= 0 && (mouseTile.Y <= this.TileY && mouseTile.Y >= 0))
        this.tiles.ElementAt<List<Tile>>(mouseTile.Y).ElementAt<Tile>(mouseTile.X).SetDecoration(this.decorationFactory.GetDecoration(currentDec.Type, (float) mouseTile.X, (float) mouseTile.Y, false, false, false, false));
      else
        this.Expand(mouseTile);
    }

    public void AddPickup(Vector2 click, Pickup currentPick)
    {
      TileLocation mouseTile = new TileLocation(click.X, click.Y);
      if (mouseTile.X <= this.TileX && mouseTile.X >= 0 && (mouseTile.Y <= this.TileY && mouseTile.Y >= 0))
        this.tiles.ElementAt<List<Tile>>(mouseTile.Y).ElementAt<Tile>(mouseTile.X).SetPickup(this.pickupFactory.GetPickup(currentPick.Type, (float) mouseTile.X, (float) mouseTile.Y));
      else
        this.Expand(mouseTile);
    }

    private void Expand(TileLocation mouseTile)
    {
      if (mouseTile.X > this.TileX)
        this.AddX();
      if (mouseTile.X < 0)
        this.AddXUp();
      if (mouseTile.Y > this.TileY)
        this.AddY();
      if (mouseTile.Y >= 0)
        return;
      this.AddYUp();
    }

    public void OpenMap(ContentManager content, string Path)
    {
      Map map;
      using (FileStream fileStream = File.Open(Path, FileMode.Open))
        map = (Map) new BinaryFormatter().Deserialize((Stream) fileStream);
      map.Instil(this.game, content);
      this.tiles = new List<List<Tile>>();
      for (int index = 0; index <= map.TileY; ++index)
      {
        this.tiles.Add(new List<Tile>());
        for (int x = 0; x <= map.TileX; ++x)
        {
          Tile tile = map.TileAt(index, x);
          this.tiles[index].Add(tile);
        }
      }
      this.GetEdge();
    }

    public void GetMap(string Path)
    {
      Tile[,] array = new Tile[this.TileY + 1, this.TileX + 1];
      int index1 = 0;
      int index2 = 0;
      foreach (List<Tile> tile1 in this.tiles)
      {
        foreach (Tile tile2 in tile1)
        {
          array[index2, index1] = tile2;
          ++index1;
        }
        ++index2;
        index1 = 0;
      }
      Map map = new Map(array);
      map.Flatten();
      using (FileStream fileStream = File.Create(Path))
        new BinaryFormatter().Serialize((Stream) fileStream, (object) map);
    }

    public void NewMap(ContentManager content)
    {
      this.tiles = new List<List<Tile>>();
      for (int index1 = 0; index1 <= 10; ++index1)
      {
        this.tiles.Add(new List<Tile>());
        for (int index2 = 0; index2 <= 10; ++index2)
          this.tiles.ElementAt<List<Tile>>(index1).Add(this.tileFactory.GetTile(TileType.Metal, (float) index2, (float) index1, false, false, false, false));
      }
    }

    private bool XCheckRight(int x) => x + 1 > this.TileX;

    private bool XCheckLeft(int x) => x - 1 < 0;

    private bool YCheckDown(int y) => y + 1 > this.TileY;

    private bool YCheckUp(int y) => y - 1 < 0;

    private bool[] GetBorders(TileLocation Tile) => this.GetBorders(Tile.X, Tile.Y);

    private bool[] GetBorders(int x, int y) => new bool[4]
    {
      this.YCheckUp(y) || this.SetBorder(this.tiles.ElementAt<List<Tile>>(y - 1).ElementAt<Tile>(x).TileType, this.tiles.ElementAt<List<Tile>>(y).ElementAt<Tile>(x).TileType),
      this.YCheckDown(y) || this.SetBorder(this.tiles.ElementAt<List<Tile>>(y + 1).ElementAt<Tile>(x).TileType, this.tiles.ElementAt<List<Tile>>(y).ElementAt<Tile>(x).TileType),
      this.XCheckLeft(x) || this.SetBorder(this.tiles.ElementAt<List<Tile>>(y).ElementAt<Tile>(x - 1).TileType, this.tiles.ElementAt<List<Tile>>(y).ElementAt<Tile>(x).TileType),
      this.XCheckRight(x) || this.SetBorder(this.tiles.ElementAt<List<Tile>>(y).ElementAt<Tile>(x + 1).TileType, this.tiles.ElementAt<List<Tile>>(y).ElementAt<Tile>(x).TileType)
    };
  }
}
