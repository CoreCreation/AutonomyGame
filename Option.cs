// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.Option
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AutonomyTheGame.Autonomy0._1
{
  internal abstract class Option
  {
    protected ContentManager content;
    protected GraphicsDevice gd;
    protected Texture2D background;
    protected Texture2D splash;
    protected Viewport view;
    protected int width;
    protected int height;

    public Option(ContentManager content, GraphicsDevice gd)
    {
      this.content = content;
      this.gd = gd;
      this.view = gd.Viewport;
    }

    public abstract void Update(MouseState mouse);

    public abstract void Draw(SpriteBatch sb);
  }
}
