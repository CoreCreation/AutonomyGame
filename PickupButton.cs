// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.PickupButton
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AutonomyTheGame.Autonomy0._1
{
  internal class PickupButton : Button
  {
    private Pickup pickUp;

    public Pickup PickUp => this.pickUp;

    public PickupButton(int x, int y, Pickup pickup)
      : base(x, y, 256, 256)
      => this.pickUp = pickup;

    public override void Draw(SpriteBatch sb) => this.pickUp.Draw(sb, Color.White);
  }
}
