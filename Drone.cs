// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.Drone
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
  internal class Drone : Enemy
  {
    private const float range = 1016f;
    private Random random;

    public Drone(ContentManager content, Map map, Vector2 spawn)
      : base(content, map, spawn)
    {
      this.random = new Random();
      this.torsoTextures = this.GetTorso(content);
      this.textureOffSet = new Vector2(175f, 96f);
      this.texturelegOffSet = new Vector2(175f, 95f);
      this.torsoTexture = this.torsoTextures[Entity.TorsoState.IdleRight];
    }

    public override void Update(TimeSpan gameTime)
    {
      base.Update(gameTime);
      this.ApplyGravity();
      this.Move();
      this.AttackLogic();
      this.GetMoveChanges();
    }

    protected override void GetDirections()
    {
      if (this.target == null)
        return;
      if (this.stun == 0)
      {
        if ((double) Entity.Distance(this.Location.X, this.Location.Y, this.target.Location.X, this.target.Location.Y) >= 1016.0)
          return;
        if ((double) this.target.Location.X > (double) this.Location.X + (double) this.Width * 2.0)
          this.currentDirections.Right = true;
        else
          this.currentDirections.Right = false;
        if ((double) this.target.Location.X < (double) this.Location.X - (double) this.Width * 2.0)
          this.currentDirections.Left = true;
        else
          this.currentDirections.Left = false;
        if ((double) this.target.Location.X > (double) this.Location.X - (double) this.Width * 2.0 && (double) this.target.Location.X < (double) this.Location.X && this.textureDirection == XDirection.Right)
          this.currentDirections.Left = true;
        if ((double) this.target.Location.X >= (double) this.Location.X + (double) this.Width * 2.0 || (double) this.target.Location.X <= (double) this.Location.X || this.textureDirection != XDirection.Left)
          return;
        this.currentDirections.Right = true;
      }
      else
      {
        this.currentDirections.Right = false;
        this.currentDirections.Left = false;
      }
    }

    public void Move()
    {
      if (this.currentDirections.Right && (double) this.Xmom < 4.66666650772095)
        this.Xmom += 2f;
      if (this.currentDirections.Left && (double) this.Xmom > -4.66666650772095)
        this.Xmom -= 2f;
      if (this.currentDirections.Left || this.currentDirections.Right)
        return;
      this.ApplyFriction();
    }

    public void AttackLogic()
    {
      if (this.target == null || (double) Entity.Distance(this.Location.X, this.Location.Y, this.target.Location.X, this.target.Location.Y) >= (double) this.attackRange || this.attackingCountdown > 0)
        return;
      if (this.textureDirection == XDirection.Right && (double) this.target.Location.X > (double) this.Location.X)
      {
        this.Attack();
        this.attackingCountdown = 500;
      }
      else
      {
        if (this.textureDirection != XDirection.Left || (double) this.target.Location.X >= (double) this.Location.X)
          return;
        this.Attack();
        this.attackingCountdown = 500;
      }
    }

    private Dictionary<Entity.TorsoState, AnimatedTexture> GetTorso(
      ContentManager content)
    {
      return new Dictionary<Entity.TorsoState, AnimatedTexture>()
      {
        [Entity.TorsoState.IdleLeft] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\Drone\\IdleL"), 15, 4, 350f, 450f, true),
        [Entity.TorsoState.IdleRight] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\Drone\\IdleR"), 15, 4, 350f, 450f, true),
        [Entity.TorsoState.JumpLeft] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\Drone\\IdleL"), 15, 4, 350f, 450f, true),
        [Entity.TorsoState.JumpRight] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\Drone\\IdleR"), 15, 4, 350f, 450f, true),
        [Entity.TorsoState.RunLeft] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\Drone\\RunL"), 1, 4, 350f, 450f, true),
        [Entity.TorsoState.RunRight] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\Drone\\RunR"), 1, 4, 350f, 450f, true),
        [Entity.TorsoState.AttackLeft] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\Drone\\AttackL"), 10, 4, 350f, 450f, true),
        [Entity.TorsoState.AttackRight] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\Drone\\AttackR"), 10, 4, 350f, 450f, true)
      };
    }
  }
}
