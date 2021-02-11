// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.Pause
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AutonomyTheGame.Autonomy0._1
{
  internal class Pause
  {
    public SpriteFont font;
    public TextButton resume;
    public TextButton menu;
    public Texture2D color;

    public Pause(ContentManager content, GraphicsDevice gd, Viewport view)
    {
      this.resume = new TextButton("Resume", content, 50, 110, 80, 50, gd);
      this.menu = new TextButton("Menu", content, 50, 170, 50, 50, gd);
      this.font = content.Load<SpriteFont>("Font");
      this.color = Div.getTexture(gd, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, view.Width, view.Height);
    }

    public void Update(MouseState ms)
    {
      this.resume.Update(ms);
      this.menu.Update(ms);
    }

    public void Draw(SpriteBatch sb)
    {
      sb.Draw(this.color, new Vector2(0.0f, 0.0f), Color.White);
      sb.DrawString(this.font, nameof (Pause), new Vector2(50f, 50f), Color.Black);
      this.resume.Draw(sb);
      this.menu.Draw(sb);
    }
  }
}
