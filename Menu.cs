// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.Menu
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AutonomyTheGame.Autonomy0._1
{
  internal class Menu
  {
    private ContentManager content;
    public TextButton start;
    public TextButton editor;
    private Texture2D splash;

    public void Update(MouseState mouseState)
    {
      this.start.Update(mouseState);
      this.editor.Update(mouseState);
    }

    public void Instil(ContentManager content, GraphicsDevice gd)
    {
      this.content = content;
      this.start = new TextButton("Start", content, 50, gd.Viewport.Height - 100, 60, 50, gd);
      this.editor = new TextButton("Editor", content, 130, gd.Viewport.Height - 100, 70, 50, gd);
      this.splash = content.Load<Texture2D>("Graphics\\PromotionalArtWork");
    }

    public void Draw(SpriteBatch sb)
    {
      sb.Draw(this.splash, new Vector2(0.0f, 0.0f), Color.White);
      this.start.Draw(sb);
      this.editor.Draw(sb);
    }
  }
}
