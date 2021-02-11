// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.Exit
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AutonomyTheGame.Autonomy0._1
{
  [Serializable]
  internal class Exit : Tile
  {
    public Exit(ContentManager Content, float x, float y)
      : base(x, y)
      => this.Instil(Content);

    protected override void Instil(ContentManager Content)
    {
      this.texture = Content.Load<Texture2D>("Graphics\\Tiles\\Exit\\Exit");
      this.border = false;
      this.anim = (AnimatedTexture) null;
      this.collision = false;
      this.damage = new int?();
      this.speedMod = new float?();
      this.spawn = new Spawns?();
      this.type = TileType.Exit;
      this.brightness = (int) byte.MaxValue;
      this.absorbValue = 0.0;
      this.activationTile = true;
    }

    public override void Activate(Game1 game, Entity activator)
    {
      game.PauseGame();
      game.SwitchToMenuGame((object) null, (EventArgs) null);
    }
  }
}
