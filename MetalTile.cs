// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.MetalTile
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AutonomyTheGame.Autonomy0._1
{
  [Serializable]
  internal class MetalTile : Tile
  {
    public MetalTile(ContentManager Content, float x, float y)
      : base(x, y)
      => this.Instil(Content);

    public MetalTile(
      ContentManager Content,
      float x,
      float y,
      bool borderTop,
      bool borderBottom,
      bool borderLeft,
      bool borderRight)
      : base(x, y)
    {
      this.Instil(Content);
      this.borderBottom = borderBottom;
      this.borderLeft = borderLeft;
      this.borderRight = borderRight;
      this.borderTop = borderTop;
    }

    protected override void Instil(ContentManager Content)
    {
      this.anim = (AnimatedTexture) null;
      this.texture = Content.Load<Texture2D>("Graphics\\Tiles\\MetalTile\\Base");
      this.borderBottomTexture = Content.Load<Texture2D>("Graphics\\Tiles\\MetalTile\\Bottom");
      this.borderTopTexture = Content.Load<Texture2D>("Graphics\\Tiles\\MetalTile\\Top");
      this.borderRightTexture = Content.Load<Texture2D>("Graphics\\Tiles\\MetalTile\\Right");
      this.borderLeftTexture = Content.Load<Texture2D>("Graphics\\Tiles\\MetalTile\\Left");
      this.border = true;
      this.collision = true;
      this.damage = new int?();
      this.speedMod = new float?();
      this.spawn = new Spawns?();
      this.type = TileType.Metal;
      this.absorbValue = 0.200000002980232;
      this.brightness = 0;
      this.activationTile = false;
    }
  }
}
