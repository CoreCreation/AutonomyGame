// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.PickupFactory
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework.Content;

namespace AutonomyTheGame.Autonomy0._1
{
  internal class PickupFactory
  {
    private ContentManager Content;

    public PickupFactory(ContentManager Content) => this.Content = Content;

    public Pickup GetPickup(PickupType type, float x, float y) => type == PickupType.Health ? (Pickup) new HealthPickUp(this.Content, x, y) : (Pickup) null;
  }
}
