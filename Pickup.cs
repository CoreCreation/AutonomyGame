// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.Pickup
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AutonomyTheGame.Autonomy0._1
{
  [Serializable]
  public abstract class Pickup : Placeable
  {
    private Vector2 location;
    [NonSerialized]
    protected Texture2D texture;
    [NonSerialized]
    protected AnimatedTexture animTexture;
    protected bool pickedUp;
    protected PickupType type;

    public PickupType Type => this.type;

    public Pickup(float x, float y)
    {
      this.location = new Vector2(256f * x, 256f * y);
      this.pickedUp = false;
    }

    public void Update()
    {
      if (this.animTexture == null)
        return;
      this.animTexture.Animate();
    }

    public void Draw(SpriteBatch sb, Color shade)
    {
      if (this.pickedUp)
        return;
      if (this.texture != null)
        sb.Draw(this.texture, this.location, shade);
      if (this.animTexture == null)
        return;
      this.animTexture.Draw(sb, this.location, shade);
    }

    public Texture2D GetTexture() => this.texture;

    public AnimatedTexture GetAnimatedTexture() => this.animTexture;

    public abstract void Activate(Entity activator);
  }
}
