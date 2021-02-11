// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.Entity
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace AutonomyTheGame.Autonomy0._1
{
  public abstract class Entity
  {
    protected const float maxGravity = 40f;
    protected const int maxStamina = 120;
    protected const int staminaCost = 60;
    protected const float accel = 2f;
    protected const float MaxSpeed = 28f;
    protected const float JumpSpeed = 40f;
    public EventArgs e;
    protected Entity previousAttacker;
    protected int damageCoolDown;
    protected int health;
    protected int attackRange = 254;
    protected int damage = 5;
    private float height = 254f;
    private float width = 100f;
    protected int stamina;
    protected int ID;
    protected bool dead;
    protected int attackingDraw;
    protected bool CanJump = true;
    protected bool WallJump = true;
    protected float friction = 0.9f;
    protected Entity.XMoveStates xState;
    protected Entity.YMoveStates yState;
    protected Entity.TorsoState prevState;
    protected Entity.TorsoState actionState;
    protected Entity.LegState prevLegState;
    protected Entity.LegState actionLegState;
    protected bool brace;
    protected Entity.Directions prevDirections;
    protected Entity.Directions currentDirections;
    protected TimeSpan frameTime;
    protected float totalGameTime;
    protected float speedMod = 1f;
    protected int prevBrightness;
    protected int brightness;
    protected int attackTime;
    protected int attackTimer;
    protected int stun;
    protected XDirection xDirection;
    protected YDirection yDirection;
    protected XDirection textureDirection;
    protected float gravity;
    protected Dictionary<Entity.TorsoState, AnimatedTexture> torsoTextures;
    protected Dictionary<Entity.LegState, AnimatedTexture> legTextures;
    protected Dictionary<Entity.TorsoState, AnimatedTexture> bodyTextures;
    protected AnimatedTexture torsoTexture;
    protected AnimatedTexture legTexture;
    protected Vector2 position;
    protected Vector2 torsoTexturePosition;
    protected Vector2 legTexturePosition;
    protected Vector2 textureOffSet;
    protected Vector2 texturelegOffSet;
    protected float? xBound;
    protected float? yBound;
    protected float Xmom;
    protected float Ymom;
    protected Map map;

    public event Entity.AttackHandler attack;

    public int Health => this.health;

    public int AttackRange => this.attackRange;

    public int Damage => this.damage;

    public float Height => this.height;

    public float Width => this.width;

    public int Stamina => this.stamina;

    public bool Dead => this.dead;

    public XDirection XDir => this.xDirection;

    public XDirection TextureDirection => this.textureDirection;

    public float XSpeed => this.Xmom;

    public float YSpeed => this.Ymom;

    public Vector2 Location => new Vector2(this.position.X + this.Width / 2f, this.position.Y + this.Height / 2f);

    public Vector2 ACorner => new Vector2(this.position.X, this.position.Y);

    public Vector2 BCorner => new Vector2(this.position.X + this.Width, this.position.Y);

    public Vector2 CCorner => new Vector2(this.position.X, this.position.Y + this.Height);

    public Vector2 DCorner => new Vector2(this.position.X + this.Width, this.position.Y + this.Height);

    public Vector2 BottomMid => new Vector2(this.position.X + this.Width / 2f, this.position.Y + (float) ((double) this.Height / 6.0 * 5.0));

    public Vector2 RightMid => new Vector2(this.position.X + (float) ((double) this.Width / 20.0 * 19.0), this.position.Y + this.Height / 2f);

    public Vector2 LeftMid => new Vector2(this.position.X + this.Width / 20f, this.position.Y + this.Height / 2f);

    public Vector2 VectorAbove => new Vector2(this.position.X + this.Width / 2f, this.position.Y - this.Height / 6f);

    public Vector2 VectorBelow => new Vector2(this.position.X + this.Width / 2f, this.position.Y + (float) ((double) this.Height / 6.0 * 7.0));

    public Vector2 VectorBelowSlight => new Vector2(this.position.X + this.Width / 2f, (float) ((double) this.position.Y + (double) this.Height + 2.0));

    public Vector2 VectorRight => new Vector2(this.position.X + (float) ((double) this.Width / 6.0 * 7.0), this.position.Y + this.Height / 2f);

    public Vector2 VectorLeft => new Vector2(this.position.X - this.Width / 6f, this.position.Y + this.Height / 2f);

    public Vector2 VectorSlightRight => new Vector2((float) ((double) this.position.X + (double) this.Width + 5.0), this.position.Y + this.Height / 2f);

    public Vector2 VectorSlightLeft => new Vector2((float) ((double) this.position.X - (double) this.Width + 5.0), this.position.Y + this.Height / 2f);

    protected TileLocation ATile => new TileLocation(this.ACorner.X, this.ACorner.Y);

    protected TileLocation BTile => new TileLocation(this.BCorner.X, this.BCorner.Y);

    protected TileLocation CTile => new TileLocation(this.CCorner.X, this.CCorner.Y);

    protected TileLocation DTile => new TileLocation(this.DCorner.X, this.DCorner.Y);

    protected TileLocation ASubTile => new TileLocation(this.ACorner.X + 1f, this.ACorner.Y + 1f);

    protected TileLocation BSubTile => new TileLocation(this.BCorner.X - 1f, this.BCorner.Y + 1f);

    protected TileLocation CSubTile => new TileLocation(this.CCorner.X + 1f, this.CCorner.Y - 1f);

    protected TileLocation DSubTile => new TileLocation(this.DCorner.X - 1f, this.DCorner.Y - 1f);

    protected TileLocation MidTile => new TileLocation(this.Location.X, this.Location.Y);

    protected TileLocation BottomMidTile => new TileLocation(this.BottomMid.X, this.BottomMid.Y);

    protected TileLocation RightMidTile => new TileLocation(this.RightMid.X, this.RightMid.Y);

    protected TileLocation LeftMidTile => new TileLocation(this.LeftMid.X, this.LeftMid.Y);

    protected TileLocation TileBot => new TileLocation(this.VectorBelow.X, this.VectorBelow.Y);

    protected TileLocation TileBotSlight => new TileLocation(this.VectorBelowSlight.X, this.VectorBelowSlight.Y);

    protected TileLocation TileTop => new TileLocation(this.VectorAbove.X, this.VectorAbove.Y);

    protected TileLocation TileLeft => new TileLocation(this.VectorLeft.X, this.VectorLeft.Y);

    protected TileLocation TileRight => new TileLocation(this.VectorRight.X, this.VectorRight.Y);

    protected TileLocation TileSlightLeft => new TileLocation(this.VectorSlightLeft.X, this.VectorSlightLeft.Y);

    protected TileLocation TileSlightRight => new TileLocation(this.VectorSlightRight.X, this.VectorSlightRight.Y);

    public virtual void Instil(ContentManager Content, Map map)
    {
      this.health = 100;
      this.stamina = 0;
      this.frameTime = new TimeSpan(0, 0, 0, 0, 500);
      this.map = map;
      this.textureOffSet = new Vector2(0.0f, 0.0f);
      this.texturelegOffSet = new Vector2(0.0f, 0.0f);
      this.attackTime = 60;
      this.GetHealthState(this.xDirection, this.yDirection);
      this.damageCoolDown = 0;
      this.stun = 0;
    }

    public virtual void Update(TimeSpan gameTime)
    {
      this.prevDirections = this.currentDirections;
      this.GetDirections();
      this.totalGameTime += (float) gameTime.TotalMilliseconds;
      this.xDirection = this.FindXDirection();
      this.yDirection = this.FindYDirection();
      this.textureDirection = this.FindTextureSDirection();
      this.GetBounds();
      this.GetMoveChanges();
      this.xMove();
      this.yMove();
      this.ECC();
      this.GetTileDamage();
      this.GetActionState();
      this.GotoState();
      if (this.torsoTexture != null)
        this.torsoTexture.Animate();
      if (this.legTexture != null)
        this.legTexture.Animate();
      this.GetShade();
      this.GetTextureChange();
      if (this.attackTimer > 0)
        --this.attackTimer;
      if (this.stun > 0)
        --this.stun;
      if (this.damageCoolDown > 0)
        --this.damageCoolDown;
      else
        this.previousAttacker = (Entity) null;
    }

    public virtual void Draw(SpriteBatch sb)
    {
      if (this.legTexture != null)
        this.legTexture.Draw(sb, this.legTexturePosition, new Color(this.brightness, this.brightness, this.brightness));
      if (this.torsoTexture == null)
        return;
      this.torsoTexture.Draw(sb, this.torsoTexturePosition, new Color(this.brightness, this.brightness, this.brightness));
    }

    protected void GetTextureChange()
    {
      this.torsoTexturePosition = new Vector2(this.position.X - this.textureOffSet.X, this.position.Y - this.textureOffSet.Y);
      this.legTexturePosition = new Vector2(this.position.X - this.texturelegOffSet.X, this.position.Y - this.texturelegOffSet.Y);
    }

    protected void GetShade()
    {
      if (this.attackTimer > 0)
      {
        this.brightness = (int) byte.MaxValue;
      }
      else
      {
        this.prevBrightness = this.brightness;
        this.brightness = (this.prevBrightness + (this.map.TileAt(this.TileRight).Brightness() + this.map.TileAt(this.TileLeft).Brightness() + this.map.TileAt(this.TileTop).Brightness() + this.map.TileAt(this.TileBot).Brightness() + this.map.TileAt(this.MidTile).Brightness()) / 5) / 2;
      }
    }

    protected void GetMoveChanges()
    {
      this.friction = !this.map.TileAt(this.TileBot).Collision ? 1f : 0.9f;
      this.RechargeStamina();
      this.gravity = 1f;
      this.SetSpeedMod();
    }

    protected abstract void GetDirections();

    private void RechargeStamina()
    {
      if (this.stamina >= 120)
        return;
      ++this.stamina;
    }

    protected void GetTileDamage()
    {
      if (!this.map.TileAt(this.MidTile).Damage.HasValue)
        return;
      this.ApplyDamage(this.map.TileAt(this.MidTile).Damage.Value);
    }

    protected void SetSpeedMod()
    {
      if (this.map.TileAt(this.BottomMidTile).SpeedMod.HasValue)
        this.speedMod = this.map.TileAt(this.BottomMidTile).SpeedMod.Value;
      else
        this.speedMod = 1f;
    }

    protected virtual void ApplyGravity()
    {
      if ((double) this.Ymom >= 40.0)
        return;
      this.Ymom += this.gravity;
    }

    protected void ActivatePickUps()
    {
      TileLocation tileLocation = this.xDirection != XDirection.Right ? this.RightMidTile : this.LeftMidTile;
      if (this.map.TileAt(tileLocation).PickUpType == PickupType.None)
        return;
      this.map.ActivatePickUp(tileLocation, this);
    }

    protected void ActivateTile()
    {
      if (!this.map.TileAt(this.xDirection != XDirection.Right ? this.RightMidTile : this.LeftMidTile).ActivationTile)
        return;
      this.map.ActivateTile(this.BottomMidTile, this);
    }

    private void GetBounds()
    {
      if (this.xDirection == XDirection.Right)
        this.xBound = this.map.XObstructionCheck(this.xDirection, new TileLocation[2]
        {
          this.BSubTile,
          this.DSubTile
        });
      else
        this.xBound = this.map.XObstructionCheck(this.xDirection, new TileLocation[2]
        {
          this.ASubTile,
          this.CSubTile
        });
      if (this.yDirection == YDirection.Down)
        this.yBound = this.map.YObstructionCheck(this.yDirection, new TileLocation[2]
        {
          this.CSubTile,
          this.DSubTile
        });
      else
        this.yBound = this.map.YObstructionCheck(this.yDirection, new TileLocation[2]
        {
          this.ASubTile,
          this.BSubTile
        });
    }

    protected void ECC()
    {
      if (this.xDirection == XDirection.Right)
      {
        if (this.yDirection == YDirection.Up)
        {
          if (!this.map.TileAt(this.BSubTile).Collision)
            return;
          this.position.X += this.map.TileAt(this.BSubTile).Location.X - this.BCorner.X;
          this.position.Y += this.map.TileAt(this.BSubTile).Location.Y + 256f - this.BCorner.Y;
          this.Xmom = 0.0f;
          this.Ymom = 0.0f;
        }
        else
        {
          if (!this.map.TileAt(this.DSubTile).Collision)
            return;
          this.position.X += this.map.TileAt(this.DSubTile).Location.X - this.DCorner.X;
          this.position.Y += this.map.TileAt(this.DSubTile).Location.Y - this.DCorner.Y;
          this.Xmom = 0.0f;
          this.Ymom = 0.0f;
        }
      }
      else if (this.yDirection == YDirection.Up)
      {
        if (!this.map.TileAt(this.ASubTile).Collision)
          return;
        this.position.X += this.map.TileAt(this.ASubTile).Location.X + 256f - this.ACorner.X;
        this.position.Y += this.map.TileAt(this.ASubTile).Location.Y + 256f - this.ACorner.Y;
        this.Xmom = 0.0f;
        this.Ymom = 0.0f;
      }
      else
      {
        if (!this.map.TileAt(this.CSubTile).Collision)
          return;
        this.position.X += this.map.TileAt(this.CSubTile).Location.X + 256f - this.CCorner.X;
        this.position.Y += this.map.TileAt(this.CSubTile).Location.Y - this.CCorner.Y;
        this.Xmom = 0.0f;
        this.Ymom = 0.0f;
      }
    }

    protected XDirection FindTextureSDirection()
    {
      if (this.currentDirections.Right)
        return XDirection.Right;
      return this.currentDirections.Left ? XDirection.Left : this.textureDirection;
    }

    protected XDirection FindXDirection()
    {
      if ((double) this.Xmom > 0.0)
        return XDirection.Right;
      return (double) this.Xmom < 0.0 ? XDirection.Left : this.xDirection;
    }

    protected YDirection FindYDirection() => (double) this.Ymom >= 0.0 ? YDirection.Down : YDirection.Up;

    protected virtual void yMove()
    {
      if (this.yBound.HasValue)
      {
        switch (this.yDirection)
        {
          case YDirection.Up:
            if ((double) this.position.Y + (double) this.Ymom <= (double) this.yBound.Value)
            {
              this.position.Y += this.yBound.Value - this.position.Y;
              this.Ymom = 0.0f;
              break;
            }
            this.position.Y += this.Ymom;
            break;
          case YDirection.Down:
            if ((double) this.position.Y + (double) this.height + (double) this.Ymom >= (double) this.yBound.Value)
            {
              this.position.Y += this.yBound.Value - (this.position.Y + this.Height);
              this.Ymom = 0.0f;
              break;
            }
            this.position.Y += this.Ymom;
            break;
        }
      }
      else
      {
        this.position.Y += this.Ymom;
        this.CanJump = false;
      }
    }

    protected virtual void xMove()
    {
      this.Xmom *= this.speedMod;
      if (this.xBound.HasValue)
      {
        switch (this.xDirection)
        {
          case XDirection.Right:
            if ((double) this.position.X + (double) this.width + (double) this.Xmom >= (double) this.xBound.Value)
            {
              this.position.X += this.xBound.Value - (this.position.X + this.width);
              this.Xmom = 0.0f;
              break;
            }
            this.position.X += this.Xmom;
            break;
          case XDirection.Left:
            if ((double) this.position.X + (double) this.Xmom <= (double) this.xBound.Value)
            {
              this.position.X += this.xBound.Value - this.position.X;
              this.Xmom = 0.0f;
              break;
            }
            this.position.X += this.Xmom;
            break;
        }
      }
      else
        this.position.X += this.Xmom;
    }

    protected void GetHealthState(XDirection xDirection, YDirection yDirection)
    {
      if (this.health > 0)
        return;
      this.dead = true;
      this.position = this.map.PlayerSpawn;
      this.health = 100;
      this.Xmom = 0.0f;
      this.Ymom = 0.0f;
    }

    protected void GetActionState()
    {
      this.prevState = this.actionState;
      this.prevLegState = this.actionLegState;
      if (this.attackTimer > 0)
      {
        if (this.textureDirection == XDirection.Right)
        {
          this.actionState = Entity.TorsoState.AttackRight;
          this.actionLegState = this.prevLegState;
        }
        else
        {
          this.actionState = Entity.TorsoState.AttackLeft;
          this.actionLegState = this.prevLegState;
        }
      }
      else if (this.yState == Entity.YMoveStates.OnGround)
      {
        if ((double) this.Xmom == 0.0 && (double) this.Ymom == 0.0)
        {
          if (this.brace)
          {
            if (this.xDirection == XDirection.Right)
            {
              this.actionState = Entity.TorsoState.BraceWallRight;
              this.actionLegState = Entity.LegState.Nothing;
            }
            else
            {
              this.actionState = Entity.TorsoState.BraceWallLeft;
              this.actionLegState = Entity.LegState.Nothing;
            }
          }
          else if (this.textureDirection == XDirection.Right)
          {
            this.actionState = Entity.TorsoState.IdleRight;
            this.actionLegState = Entity.LegState.IdleRight;
          }
          else
          {
            this.actionState = Entity.TorsoState.IdleLeft;
            this.actionLegState = Entity.LegState.IdleLeft;
          }
        }
        else if ((double) Math.Abs(this.Xmom) > 5.0)
        {
          if (this.textureDirection == XDirection.Right)
          {
            this.actionState = Entity.TorsoState.RunRight;
            this.actionLegState = Entity.LegState.RunRight;
          }
          else
          {
            this.actionState = Entity.TorsoState.RunLeft;
            this.actionLegState = Entity.LegState.RunLeft;
          }
        }
        else if (this.textureDirection == XDirection.Right)
        {
          this.actionState = Entity.TorsoState.IdleRight;
          this.actionLegState = Entity.LegState.IdleRight;
        }
        else
        {
          this.actionState = Entity.TorsoState.IdleLeft;
          this.actionLegState = Entity.LegState.IdleLeft;
        }
      }
      else if (this.brace)
      {
        if ((double) this.Ymom <= 0.0)
        {
          if (this.xDirection == XDirection.Right)
          {
            this.actionState = Entity.TorsoState.WallLeapRight;
            this.actionLegState = Entity.LegState.Nothing;
          }
          else
          {
            this.actionState = Entity.TorsoState.WallLeapLeft;
            this.actionLegState = Entity.LegState.Nothing;
          }
        }
        else if (this.xDirection == XDirection.Right)
        {
          this.actionState = Entity.TorsoState.SlideRight;
          this.actionLegState = Entity.LegState.Nothing;
        }
        else
        {
          this.actionState = Entity.TorsoState.SlideLeft;
          this.actionLegState = Entity.LegState.Nothing;
        }
      }
      else if (this.xDirection == XDirection.Right)
      {
        this.actionState = Entity.TorsoState.JumpRight;
        this.actionLegState = Entity.LegState.JumpRight;
      }
      else
      {
        this.actionState = Entity.TorsoState.JumpLeft;
        this.actionLegState = Entity.LegState.JumpLeft;
      }
    }

    public virtual void GotoState()
    {
      if (this.actionState != this.prevState)
      {
        switch (this.actionState)
        {
          case Entity.TorsoState.IdleRight:
            this.torsoTexture = this.torsoTextures[Entity.TorsoState.IdleRight];
            break;
          case Entity.TorsoState.IdleLeft:
            this.torsoTexture = this.torsoTextures[Entity.TorsoState.IdleLeft];
            break;
          case Entity.TorsoState.JumpLeft:
            this.torsoTexture = this.torsoTextures[Entity.TorsoState.JumpLeft];
            break;
          case Entity.TorsoState.JumpRight:
            this.torsoTexture = this.torsoTextures[Entity.TorsoState.JumpRight];
            break;
          case Entity.TorsoState.RunLeft:
            this.torsoTexture = this.torsoTextures[Entity.TorsoState.RunLeft];
            break;
          case Entity.TorsoState.RunRight:
            this.torsoTexture = this.torsoTextures[Entity.TorsoState.RunRight];
            break;
          case Entity.TorsoState.SlideLeft:
            this.torsoTexture = this.bodyTextures[Entity.TorsoState.SlideLeft];
            break;
          case Entity.TorsoState.SlideRight:
            this.torsoTexture = this.bodyTextures[Entity.TorsoState.SlideRight];
            break;
          case Entity.TorsoState.WallLeapLeft:
            this.torsoTexture = this.bodyTextures[Entity.TorsoState.WallLeapLeft];
            break;
          case Entity.TorsoState.WallLeapRight:
            this.torsoTexture = this.bodyTextures[Entity.TorsoState.WallLeapRight];
            break;
          case Entity.TorsoState.BraceWallLeft:
            this.torsoTexture = this.bodyTextures[Entity.TorsoState.BraceWallLeft];
            break;
          case Entity.TorsoState.BraceWallRight:
            this.torsoTexture = this.bodyTextures[Entity.TorsoState.BraceWallRight];
            break;
          case Entity.TorsoState.AttackLeft:
            this.torsoTextures[Entity.TorsoState.AttackLeft].Reset();
            this.torsoTexture = this.torsoTextures[Entity.TorsoState.AttackLeft];
            break;
          case Entity.TorsoState.AttackRight:
            this.torsoTextures[Entity.TorsoState.AttackRight].Reset();
            this.torsoTexture = this.torsoTextures[Entity.TorsoState.AttackRight];
            break;
          default:
            this.torsoTexture = this.torsoTextures[Entity.TorsoState.IdleRight];
            break;
        }
      }
      if (this.legTextures == null || this.actionLegState == this.prevLegState)
        return;
      switch (this.actionLegState)
      {
        case Entity.LegState.IdleRight:
          this.legTexture = this.legTextures[Entity.LegState.IdleRight];
          break;
        case Entity.LegState.IdleLeft:
          this.legTexture = this.legTextures[Entity.LegState.IdleLeft];
          break;
        case Entity.LegState.RunRight:
          this.legTexture = this.legTextures[Entity.LegState.RunRight];
          break;
        case Entity.LegState.RunLeft:
          this.legTexture = this.legTextures[Entity.LegState.RunLeft];
          break;
        case Entity.LegState.JumpRight:
          this.legTexture = this.legTextures[Entity.LegState.JumpRight];
          break;
        case Entity.LegState.JumpLeft:
          this.legTexture = this.legTextures[Entity.LegState.JumpLeft];
          break;
        case Entity.LegState.Nothing:
          this.legTexture = (AnimatedTexture) null;
          break;
        default:
          this.legTexture = this.legTextures[Entity.LegState.IdleRight];
          break;
      }
    }

    protected void GetXState(XDirection xDirection)
    {
      switch (xDirection)
      {
        case XDirection.Right:
          if (this.map.TileAt(this.TileSlightRight).Collision)
          {
            this.xState = Entity.XMoveStates.NearWall;
            break;
          }
          this.xState = Entity.XMoveStates.NotNearWall;
          break;
        case XDirection.Left:
          if (this.map.TileAt(this.TileSlightLeft).Collision)
          {
            this.xState = Entity.XMoveStates.NearWall;
            break;
          }
          this.xState = Entity.XMoveStates.NotNearWall;
          break;
      }
    }

    protected void GetYState(YDirection yDirection)
    {
      switch (yDirection)
      {
        case YDirection.Up:
          if (this.map.TileAt(this.TileBotSlight).Collision)
          {
            this.yState = Entity.YMoveStates.OnGround;
            break;
          }
          this.yState = Entity.YMoveStates.InAir;
          break;
        case YDirection.Down:
          if (this.map.TileAt(this.TileBotSlight).Collision)
          {
            this.yState = Entity.YMoveStates.OnGround;
            break;
          }
          this.yState = Entity.YMoveStates.InAir;
          break;
      }
    }

    protected virtual void ApplyFriction()
    {
      if ((double) Math.Abs(this.Xmom) > 0.5)
        this.Xmom *= this.friction;
      else
        this.Xmom = 0.0f;
    }

    protected void Attack()
    {
      if (this.attackTimer == 0)
        this.attackTimer = this.attackTime;
      if (this.attack == null)
        return;
      this.attack((object) this, this.e);
    }

    public void GetAttacked(object sender, EventArgs e)
    {
      Entity attacker = sender as Entity;
      this.KnockBack(attacker);
      if (this.previousAttacker != null && attacker == this.previousAttacker)
        return;
      this.damageCoolDown = attacker.attackTime;
      this.previousAttacker = attacker;
      this.attackTimer = 0;
      this.ApplyDamage(attacker.Damage);
      this.stun = 60;
    }

    protected void KnockBack(Entity attacker)
    {
      if (attacker.TextureDirection == XDirection.Left)
      {
        this.Xmom = -7f;
        this.Ymom = -4.666667f;
      }
      else
      {
        this.Xmom = 7f;
        this.Ymom = -4.666667f;
      }
    }

    private void ApplyDamage(int damage) => this.health -= damage;

    public void ApplyHealth(int health) => this.health += health;

    public static float Distance(float x1, float y1, float x2, float y2) => (float) Math.Sqrt(Math.Pow((double) x2 - (double) x1, 2.0) + Math.Pow((double) y2 - (double) y1, 2.0));

    public static Vector2 MidPoint(Vector2 a, Vector2 b) => new Vector2((a.X + b.X) / 2f, (a.Y + b.Y) / 2f);

    public delegate void AttackHandler(object sender, EventArgs e);

    protected enum TorsoState
    {
      IdleRight,
      IdleLeft,
      JumpLeft,
      JumpRight,
      RunLeft,
      RunRight,
      DeathLeft,
      DeathRight,
      SlideLeft,
      SlideRight,
      WallLeapLeft,
      WallLeapRight,
      BraceWallLeft,
      BraceWallRight,
      AttackLeft,
      AttackRight,
    }

    protected enum LegState
    {
      IdleRight,
      IdleLeft,
      RunRight,
      RunLeft,
      JumpRight,
      JumpLeft,
      Nothing,
    }

    protected enum XMoveStates
    {
      NearWall,
      NotNearWall,
    }

    protected enum YMoveStates
    {
      OnGround,
      InAir,
    }

    protected struct Directions
    {
      public bool Up { get; set; }

      public bool Down { get; set; }

      public bool Right { get; set; }

      public bool Left { get; set; }

      public bool Attack { get; set; }
    }
  }
}
