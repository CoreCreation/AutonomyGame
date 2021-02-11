// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.Tile
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
  public abstract class Tile : Placeable
  {
    [NonSerialized]
    public const int TileSize = 256;
    protected TileType type;
    [NonSerialized]
    protected bool collision;
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
    protected bool corners;
    [NonSerialized]
    protected Texture2D CBL;
    [NonSerialized]
    protected Texture2D CBR;
    [NonSerialized]
    protected Texture2D CTL;
    [NonSerialized]
    protected Texture2D CTR;
    [NonSerialized]
    protected bool CBLB;
    [NonSerialized]
    protected bool CBRB;
    [NonSerialized]
    protected bool CTLB;
    [NonSerialized]
    protected bool CTRB;
    [NonSerialized]
    protected AnimatedTexture anim;
    [NonSerialized]
    protected Particle particle;
    protected PickupType pickUpType;
    protected Pickup pickup;
    protected DecorationType decorationType;
    protected Decoration decoration;
    [NonSerialized]
    protected Vector2 location;
    [NonSerialized]
    protected Spawns? spawn;
    [NonSerialized]
    protected float? speedMod;
    [NonSerialized]
    protected int? damage;
    [NonSerialized]
    protected int brightness;
    [NonSerialized]
    protected double absorbValue;
    [NonSerialized]
    protected bool activationTile;

    public TileType TileType => this.type;

    public bool Collision => this.collision;

    public bool BorderTop => this.borderTop;

    public bool BorderBottom => this.borderBottom;

    public bool BorderLeft => this.borderLeft;

    public bool BorderRight => this.borderRight;

    public AnimatedTexture Anim => this.anim;

    public Particle Particle => this.particle;

    public PickupType PickUpType => this.pickUpType;

    public Pickup Pickup => this.pickup;

    public DecorationType DecorationType => this.decorationType;

    public Decoration Decoration => this.decoration;

    public Vector2 Location => this.location;

    public Spawns? Spawn => this.spawn;

    public float? SpeedMod => this.speedMod;

    public int? Damage => this.damage;

    public int Brightness()
    {
      if (this.decoration == null || this.decoration.Brightness <= this.brightness)
        return this.brightness;
      this.brightness = this.decoration.Brightness;
      return this.decoration.Brightness;
    }

    public double AbsorbValue => this.absorbValue;

    public bool ActivationTile => this.activationTile;

    public Rectangle Rec => new Rectangle((int) this.location.X, (int) this.location.Y, 256, 256);

    public float RightBound => this.location.X + 256f;

    public float LeftBound => this.location.X;

    public float TopBound => this.location.Y;

    public float BotBound => this.location.Y + 256f;

    public Tile(float x, float y) => this.location = new Vector2(256f * x, 256f * y);

    public void Update()
    {
      if (this.anim != null)
        this.anim.Animate();
      if (this.decoration != null)
        this.decoration.Update();
      if (this.pickup == null)
        return;
      this.pickup.Update();
    }

    public void Draw(SpriteBatch sb)
    {
      if (this.texture != null)
        sb.Draw(this.texture, this.location, new Color(this.brightness, this.brightness, this.brightness));
      if (this.border)
      {
        if (this.borderTop)
          sb.Draw(this.borderTopTexture, this.location, new Color(this.brightness, this.brightness, this.brightness));
        if (this.borderBottom)
          sb.Draw(this.borderBottomTexture, this.location, new Color(this.brightness, this.brightness, this.brightness));
        if (this.borderRight)
          sb.Draw(this.borderRightTexture, this.location, new Color(this.brightness, this.brightness, this.brightness));
        if (this.borderLeft)
          sb.Draw(this.borderLeftTexture, this.location, new Color(this.brightness, this.brightness, this.brightness));
      }
      if (this.corners)
      {
        if (this.CTLB)
          sb.Draw(this.CTL, this.location, new Color(this.brightness, this.brightness, this.brightness));
        if (this.CTRB)
          sb.Draw(this.CTR, this.location, new Color(this.brightness, this.brightness, this.brightness));
        if (this.CBRB)
          sb.Draw(this.CBR, this.location, new Color(this.brightness, this.brightness, this.brightness));
        if (this.CBLB)
          sb.Draw(this.CBL, this.location, new Color(this.brightness, this.brightness, this.brightness));
      }
      if (this.anim != null)
        this.anim.Draw(sb, this.location, new Color(this.brightness, this.brightness, this.brightness));
      if (this.decoration != null)
        this.decoration.Draw(sb, new Color(this.brightness, this.brightness, this.brightness));
      if (this.pickup == null)
        return;
      this.pickup.Draw(sb, new Color(this.brightness, this.brightness, this.brightness));
    }

    protected abstract void Instil(ContentManager Content);

    public virtual void Activate(Game1 game, Entity activator)
    {
    }

    public Texture2D GetTexture() => this.texture;

    public AnimatedTexture GetAnimatedTexture() => this.anim;

    public void SetDecoration(Decoration decoration)
    {
      this.decoration = decoration;
      this.decorationType = decoration.Type;
    }

    public void SetPickup(Pickup pickup)
    {
      this.pickup = pickup;
      this.pickUpType = pickup.Type;
    }

    protected void SetCorners()
    {
      if (!this.corners)
        return;
      if (this.borderBottom && this.borderLeft)
        this.CBLB = true;
      if (this.borderBottom && this.borderRight)
        this.CBRB = true;
      if (this.borderTop && this.borderLeft)
        this.CTLB = true;
      if (!this.borderTop || !this.borderRight)
        return;
      this.CTRB = true;
    }

    public void SetBrightness(int value) => this.brightness = (int) ((double) value * (1.0 - this.absorbValue));

    public void SetBrightnessFull(int value) => this.brightness = value;
  }
}
