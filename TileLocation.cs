// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.TileLocation
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using System;

namespace AutonomyTheGame.Autonomy0._1
{
  public struct TileLocation
  {
    public int X;
    public int Y;

    public TileLocation(float x, float y)
    {
      this.X = (int) Math.Ceiling((double) x / 256.0) - 1;
      this.Y = (int) Math.Ceiling((double) y / 256.0) - 1;
    }

    public override string ToString() => this.X.ToString() + " " + this.Y.ToString();

    public Vector2 GetVector() => new Vector2((float) (this.X * 256), (float) (this.Y * 256));
  }
}
