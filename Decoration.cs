// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.Decoration
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AutonomyTheGame.Autonomy0._1
{
  [Serializable]
  public abstract class Decoration : Placeable
  {
    protected DecorationType type;
    [NonSerialized]
    protected Texture2D texture;
    protected bool border;
    protected bool borderTop;
    protected bool borderBottom;
    protected bool borderLeft;
    protected bool borderRight;
    [NonSerialized]
    protected Texture2D borderTopTexture;
    [NonSerialized]
    protected Texture2D borderBottomTexture;
    [NonSerialized]
    protected Texture2D borderLeftTexture;
    [NonSerialized]
    protected Texture2D borderRightTexture;
    [NonSerialized]
    protected AnimatedTexture anim;
    [NonSerialized]
    protected Particle particle;
    [NonSerialized]
    protected Vector2 location;
    [NonSerialized]
    protected Spawns? spawn;
    [NonSerialized]
    protected int? damage;
    protected int brightness;

    public DecorationType Type => this.type;

    public bool BorderTop => this.borderTop;

    public bool BorderBottom => this.borderBottom;

    public bool BorderLeft => this.borderLeft;

    public bool BorderRight => this.borderRight;

    public AnimatedTexture Anim => this.anim;

    public Particle Particle => this.particle;

    public Vector2 Location => this.location;

    public Spawns? Spawn => this.spawn;

    public int? Damage => this.damage;

    public int Brightness => this.brightness;

    public Decoration(float x, float y) => this.location = new Vector2(256f * x, 256f * y);

    public void Update()
    {
      if (this.anim == null)
        return;
      this.anim.Animate();
    }

    public void Draw(SpriteBatch sb, Color shade)
    {
      if (this.texture != null)
        sb.Draw(this.texture, this.location, shade);
      if (this.anim == null)
        return;
      this.anim.Draw(sb, this.location, shade);
    }

    public Texture2D GetTexture() => this.texture;

    public AnimatedTexture GetAnimatedTexture() => this.anim;

    protected abstract void Instil(ContentManager Content);
  }
}
