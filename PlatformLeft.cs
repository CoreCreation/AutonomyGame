﻿// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.PlatformLeft
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AutonomyTheGame.Autonomy0._1
{
  [Serializable]
  internal class PlatformLeft : Tile
  {
    public PlatformLeft(ContentManager Content, float x, float y)
      : base(x, y)
      => this.Instil(Content);

    protected override void Instil(ContentManager Content)
    {
      this.anim = (AnimatedTexture) null;
      this.texture = Content.Load<Texture2D>("Graphics\\Tiles\\Platforms\\Platform_Left");
      this.collision = true;
      this.damage = new int?();
      this.speedMod = new float?();
      this.spawn = new Spawns?();
      this.type = TileType.PlatformLeft;
      this.absorbValue = 0.0;
      this.brightness = (int) byte.MaxValue;
      this.activationTile = false;
    }
  }
}
