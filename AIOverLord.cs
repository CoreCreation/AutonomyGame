// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.AIOverLord
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AutonomyTheGame.Autonomy0._1
{
  internal class AIOverLord
  {
    private ContentManager content;
    private Player player;
    private List<Enemy> enemies;
    private List<Enemy> deadEnemies;
    private Map map;
    private int enemyCount;

    public AIOverLord()
    {
      this.enemies = new List<Enemy>();
      this.deadEnemies = new List<Enemy>();
      this.enemyCount = 0;
    }

    public void Instil(Map map, ContentManager content, Player player)
    {
      this.map = map;
      this.content = content;
      this.player = player;
      foreach (Tile tile in map.Spawn)
      {
        this.enemies.Add((Enemy) new Drone(content, map, tile.Location));
        player.attack += new Entity.AttackHandler(this.enemies[this.enemies.Count - 1].playerAttacked);
        this.enemies[this.enemies.Count - 1].attack += new Entity.AttackHandler(((Entity) player).GetAttacked);
        ++this.enemyCount;
      }
    }

    public void Update(GameTime gameTime)
    {
      foreach (Enemy enemy in this.enemies)
      {
        if (enemy.Dead)
        {
          this.deadEnemies.Add(enemy);
          --this.enemyCount;
        }
        else if (enemy is Drone)
          (enemy as Drone).Update(gameTime.ElapsedGameTime);
      }
      foreach (Enemy deadEnemy in this.deadEnemies)
        this.enemies.Remove(deadEnemy);
      this.deadEnemies = new List<Enemy>();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      foreach (Entity enemy in this.enemies)
        enemy.Draw(spriteBatch);
    }

    public void SetTarget(Entity e)
    {
      foreach (Enemy enemy in this.enemies)
        enemy.SetEnemy(e);
    }
  }
}
