// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.TileFactory
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework.Content;

namespace AutonomyTheGame.Autonomy0._1
{
  internal class TileFactory
  {
    private ContentManager Content;

    public TileFactory(ContentManager Content) => this.Content = Content;

    public Tile GetTile(
      TileType type,
      float x,
      float y,
      bool borderTop,
      bool borderBottom,
      bool borderLeft,
      bool borderRight)
    {
      switch (type)
      {
        case TileType.Empty:
          return (Tile) new EmptyTile(this.Content, x, y);
        case TileType.Metal:
          return (Tile) new MetalTile(this.Content, x, y, borderTop, borderBottom, borderLeft, borderRight);
        case TileType.InteriorBase:
          return (Tile) new InteriorBase(this.Content, x, y, borderTop, borderBottom, borderLeft, borderRight);
        case TileType.PlayerSpawn:
          return (Tile) new PlayerSpawn(this.Content, x, y);
        case TileType.Exit:
          return (Tile) new Exit(this.Content, x, y);
        case TileType.CheckPoint:
          return (Tile) new CheckPoint(this.Content, x, y);
        case TileType.PlatformLeft:
          return (Tile) new PlatformLeft(this.Content, x, y);
        case TileType.PlatformRight:
          return (Tile) new PlatformRight(this.Content, x, y);
        case TileType.InteriorNC:
          return (Tile) new InteriorNC(this.Content, x, y);
        case TileType.DroneSpawn:
          return (Tile) new DroneSpawn(this.Content, x, y);
        case TileType.Crate:
          return (Tile) new Crate(this.Content, x, y);
        default:
          return (Tile) new EmptyTile(this.Content, x, y);
      }
    }
  }
}
