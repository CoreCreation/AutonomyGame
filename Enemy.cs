// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.Enemy
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace AutonomyTheGame.Autonomy0._1
{
  internal abstract class Enemy : Entity
  {
    protected Entity target;
    protected int attackingCountdown;

    public Enemy(ContentManager Content, Map map, Vector2 spawn)
    {
      this.Instil(Content, map);
      this.position = spawn;
      this.gravity = 1f;
    }

    public void SetEnemy(Entity target) => this.target = target;

    public new virtual void Update(TimeSpan gameTime)
    {
      base.Update(gameTime);
      this.GetXState(this.xDirection);
      this.GetYState(this.yDirection);
      this.xMove();
      this.yMove();
      this.GetHealthState(this.xDirection, this.yDirection);
      --this.attackingCountdown;
    }

    public virtual void playerAttacked(object sender, EventArgs e)
    {
      if ((double) Entity.Distance(this.Location.X, this.Location.Y, this.target.Location.X, this.target.Location.Y) > (double) this.target.AttackRange)
        return;
      this.GetAttacked(sender, e);
    }
  }
}
