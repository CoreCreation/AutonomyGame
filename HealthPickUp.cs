// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.HealthPickUp
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AutonomyTheGame.Autonomy0._1
{
  [Serializable]
  internal class HealthPickUp : Pickup
  {
    public HealthPickUp(ContentManager content, float x, float y)
      : base(x, y)
    {
      this.texture = content.Load<Texture2D>("Graphics\\rotatetrans");
      this.type = PickupType.Health;
    }

    public override void Activate(Entity activator)
    {
      if (this.pickedUp)
        return;
      activator.ApplyHealth(10);
      this.pickedUp = true;
    }
  }
}
