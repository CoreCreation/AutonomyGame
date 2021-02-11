// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.GameGUI
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AutonomyTheGame.Autonomy0._1
{
  internal class GameGUI
  {
    private Map map;
    private SpriteFont font;
    private Player player;

    public GameGUI(ContentManager content, Player player, Map map)
    {
      this.player = player;
      this.map = map;
      this.font = content.Load<SpriteFont>("Font");
    }

    public void Draw(SpriteBatch sb)
    {
      sb.DrawString(this.font, this.player.Location.ToString(), new Vector2(0.0f, 0.0f), Color.White);
      sb.DrawString(this.font, this.player.Health.ToString(), new Vector2(0.0f, 20f), Color.White);
      sb.DrawString(this.font, this.player.Stamina.ToString(), new Vector2(0.0f, 40f), Color.White);
      sb.DrawString(this.font, this.player.XSpeed.ToString(), new Vector2(0.0f, 60f), Color.White);
      sb.DrawString(this.font, this.player.YSpeed.ToString(), new Vector2(0.0f, 80f), Color.White);
      sb.DrawString(this.font, (this.map.TileX * this.map.TileY).ToString(), new Vector2(0.0f, 100f), Color.White);
      sb.DrawString(this.font, this.map.TileX.ToString(), new Vector2(0.0f, 120f), Color.White);
      sb.DrawString(this.font, this.map.TileY.ToString(), new Vector2(0.0f, 140f), Color.White);
    }
  }
}
