// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.Player
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace AutonomyTheGame.Autonomy0._1
{
  internal class Player : Entity
  {
    private uint rollCountDown;
    private KeyboardState currentState;
    private KeyboardState prevKeyState;

    public override void Instil(ContentManager Content, Map map)
    {
      base.Instil(Content, map);
      this.position = map.PlayerSpawn;
      this.gravity = 1f;
      this.torsoTextures = this.GetTorso(Content);
      this.bodyTextures = this.GetBody(Content);
      this.legTextures = this.GetLeg(Content);
      this.textureOffSet = new Vector2(175f, 96f);
      this.texturelegOffSet = new Vector2(175f, 95f);
      this.torsoTexture = this.torsoTextures[Entity.TorsoState.IdleLeft];
      this.legTexture = this.legTextures[Entity.LegState.IdleLeft];
      this.attackTime = 15;
      this.damage = 50;
    }

    public void Update(TimeSpan gameTime, KeyboardState currentState, KeyboardState prevState)
    {
      this.prevKeyState = prevState;
      this.currentState = currentState;
      this.Move(currentState, prevState);
      this.Attacking();
      this.Update(gameTime);
      this.ApplyGravity();
      this.GetHealthState(this.xDirection, this.yDirection);
      this.GetYState(this.yDirection);
      this.ActivatePickUps();
      this.ActivateTile();
      --this.rollCountDown;
    }

    public void Move(KeyboardState currentState, KeyboardState prevState)
    {
      this.brace = false;
      if (currentState.IsKeyDown(Keys.D))
      {
        if ((double) this.Xmom > 28.0 && this.rollCountDown <= 0U)
          this.Xmom = 28f;
        if ((double) this.Xmom < 28.0 && (double) this.Xmom >= 26.0)
          this.Xmom = 28f;
        else if ((double) this.Xmom <= 26.0)
          this.Xmom += 2f;
        if (this.map.TileAt(this.TileRight).Collision)
          this.brace = true;
        else
          this.brace = false;
      }
      if (currentState.IsKeyDown(Keys.A))
      {
        if ((double) this.Xmom < -28.0 && this.rollCountDown <= 0U)
          this.Xmom = -28f;
        if ((double) this.Xmom > -28.0 && (double) this.Xmom <= -26.0)
          this.Xmom = -28f;
        else if ((double) this.Xmom >= -26.0)
          this.Xmom -= 2f;
        if (this.map.TileAt(this.TileLeft).Collision)
          this.brace = true;
        else
          this.brace = false;
      }
      if (currentState.IsKeyDown(Keys.W) && prevState.IsKeyUp(Keys.W))
      {
        if (currentState.IsKeyDown(Keys.A) && this.map.TileAt(this.TileSlightLeft).Collision)
          this.WallCrawl();
        else if (currentState.IsKeyDown(Keys.D) && this.map.TileAt(this.TileSlightRight).Collision)
          this.WallCrawl();
        else if (this.map.TileAt(this.TileBot).Collision)
          this.Ymom -= 40f;
        else if (this.map.TileAt(this.TileSlightLeft).Collision && !this.map.TileAt(this.TileBot).Collision)
        {
          if (this.stamina >= 60)
          {
            this.stamina -= 60;
            this.Xmom = 0.0f;
            this.Xmom += 14f;
            this.Ymom = 0.0f;
            this.Ymom = -40f;
          }
        }
        else if (this.map.TileAt(this.TileSlightRight).Collision && !this.map.TileAt(this.TileBot).Collision && this.stamina >= 60)
        {
          this.stamina -= 60;
          this.Xmom = 0.0f;
          this.Xmom -= 14f;
          this.Ymom = 0.0f;
          this.Ymom = -40f;
        }
      }
      if (currentState.IsKeyDown(Keys.S) && prevState.IsKeyUp(Keys.S) && this.stamina >= 120)
      {
        this.rollCountDown = 30U;
        this.stamina -= 120;
        switch (this.xDirection)
        {
          case XDirection.Right:
            this.Xmom = 42f;
            break;
          case XDirection.Left:
            this.Xmom = -42f;
            break;
        }
      }
      if (currentState.IsKeyDown(Keys.RightShift) && prevState.IsKeyUp(Keys.RightShift) && !this.brace)
        this.Attack();
      if (currentState.IsKeyUp(Keys.A) && currentState.IsKeyUp(Keys.D))
        this.ApplyFriction();
      if (currentState.IsKeyDown(Keys.K))
        this.health = 0;
      if (!this.brace || (double) this.Ymom <= 0.0)
        return;
      this.Ymom /= 2f;
    }

    protected override void GetDirections()
    {
      if (this.currentState.IsKeyDown(Keys.W))
        this.currentDirections.Up = true;
      else
        this.currentDirections.Up = false;
      if (this.currentState.IsKeyDown(Keys.S))
        this.currentDirections.Down = true;
      else
        this.currentDirections.Down = false;
      if (this.currentState.IsKeyDown(Keys.D))
        this.currentDirections.Right = true;
      else
        this.currentDirections.Right = false;
      if (this.currentState.IsKeyDown(Keys.A))
        this.currentDirections.Left = true;
      else
        this.currentDirections.Left = false;
    }

    private void Attacking()
    {
      if (this.attackTimer <= 0)
        return;
      this.Attack();
    }

    private void WallCrawl()
    {
      if (this.stamina < 60)
        return;
      this.Ymom = -40f;
      this.stamina -= 60;
    }

    private Dictionary<Entity.TorsoState, AnimatedTexture> GetTorso(
      ContentManager content)
    {
      return new Dictionary<Entity.TorsoState, AnimatedTexture>()
      {
        [Entity.TorsoState.AttackLeft] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\MainChar\\Torso\\AttackC1L"), 5, 2, 350f, 450f, false),
        [Entity.TorsoState.AttackRight] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\MainChar\\Torso\\AttackC1R"), 5, 2, 350f, 450f, false),
        [Entity.TorsoState.IdleLeft] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\MainChar\\Torso\\IdelL"), 20, 4, 350f, 450f, true),
        [Entity.TorsoState.IdleRight] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\MainChar\\Torso\\IdelR"), 20, 4, 350f, 450f, true),
        [Entity.TorsoState.JumpLeft] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\MainChar\\Torso\\JumpLL"), 1, 4, 350f, 450f, true),
        [Entity.TorsoState.JumpRight] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\MainChar\\Torso\\IdelR"), 1, 4, 350f, 450f, true),
        [Entity.TorsoState.RunLeft] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\MainChar\\Torso\\RunLL"), 7, 4, 350f, 450f, true),
        [Entity.TorsoState.RunRight] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\MainChar\\Torso\\RunLR"), 7, 4, 350f, 450f, true)
      };
    }

    private Dictionary<Entity.TorsoState, AnimatedTexture> GetBody(
      ContentManager content)
    {
      return new Dictionary<Entity.TorsoState, AnimatedTexture>()
      {
        [Entity.TorsoState.DeathLeft] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\MainChar\\FullBody\\DeathL"), 17, 4, 350f, 450f, false),
        [Entity.TorsoState.DeathRight] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\MainChar\\FullBody\\DeathR"), 17, 4, 350f, 450f, false),
        [Entity.TorsoState.BraceWallLeft] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\MainChar\\FullBody\\IdelNextWallL"), 5, 4, 350f, 450f, true),
        [Entity.TorsoState.BraceWallRight] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\MainChar\\FullBody\\IdelNextWallR"), 5, 4, 350f, 450f, true),
        [Entity.TorsoState.SlideLeft] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\MainChar\\FullBody\\SlideL"), 4, 4, 350f, 450f, true),
        [Entity.TorsoState.SlideRight] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\MainChar\\FullBody\\SlideR"), 4, 4, 350f, 450f, true),
        [Entity.TorsoState.WallLeapLeft] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\MainChar\\FullBody\\WallLeapLL"), 13, 4, 350f, 450f, true),
        [Entity.TorsoState.WallLeapRight] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\MainChar\\FullBody\\WallLeapLR"), 13, 4, 350f, 450f, true)
      };
    }

    private Dictionary<Entity.LegState, AnimatedTexture> GetLeg(
      ContentManager content)
    {
      return new Dictionary<Entity.LegState, AnimatedTexture>()
      {
        [Entity.LegState.RunLeft] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\MainChar\\Legs\\RunLL"), 7, 4, 350f, 450f, true),
        [Entity.LegState.RunRight] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\MainChar\\Legs\\RunLR"), 7, 4, 350f, 450f, true),
        [Entity.LegState.IdleLeft] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\MainChar\\Legs\\IdleL"), 20, 4, 350f, 450f, true),
        [Entity.LegState.IdleRight] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\MainChar\\Legs\\IdleR"), 20, 4, 350f, 450f, true),
        [Entity.LegState.JumpLeft] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\MainChar\\Legs\\JumpLL"), 1, 4, 350f, 450f, true),
        [Entity.LegState.JumpRight] = new AnimatedTexture(content.Load<Texture2D>("Graphics\\MainChar\\Legs\\JumpLR"), 1, 4, 350f, 450f, true)
      };
    }
  }
}
