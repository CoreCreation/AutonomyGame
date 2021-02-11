// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.Button
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace AutonomyTheGame.Autonomy0._1
{
  internal abstract class Button
  {
    public EventArgs e;
    protected MouseState prevState;
    protected MouseState currentState;
    protected Vector2 location;
    protected Rectangle area;
    protected int height;
    protected int width;

    public event Button.ClickHandler click;

    public event Button.RightClickHandler rightClick;

    public event Button.ScrollOver scrollOver;

    public event Button.ScrollOut scrollOut;

    public Vector2 Location => this.location;

    public Button(int x, int y, int width, int height)
    {
      this.location = new Vector2((float) x, (float) y);
      this.area = new Rectangle(x, y, width, height);
      this.height = height;
      this.width = width;
    }

    public abstract void Draw(SpriteBatch sb);

    public virtual void Update(MouseState mouse)
    {
      this.prevState = this.currentState;
      this.currentState = mouse;
      this.ClickCheck();
      this.RightClickCheck();
      this.ScrollOverCheck();
      this.ScrollOutCheck();
    }

    public bool CheckBounds(MouseState mouse) => this.area.Contains(new Point(mouse.X, mouse.Y));

    protected virtual void ClickCheck()
    {
      if (this.click == null || this.currentState.LeftButton != ButtonState.Pressed || (!this.CheckBounds(this.currentState) || this.prevState.LeftButton == ButtonState.Pressed))
        return;
      this.click((object) this, this.e);
    }

    protected virtual void RightClickCheck()
    {
      if (this.rightClick == null || this.currentState.RightButton != ButtonState.Pressed || (!this.CheckBounds(this.currentState) || this.prevState.RightButton == ButtonState.Pressed))
        return;
      this.rightClick((object) this, this.e);
    }

    protected virtual void ScrollOverCheck()
    {
      if (this.scrollOver == null || !this.CheckBounds(this.currentState))
        return;
      this.scrollOver((object) this, this.e);
    }

    protected virtual void ScrollOutCheck()
    {
      if (this.scrollOut == null || this.CheckBounds(this.currentState))
        return;
      this.scrollOut((object) this, this.e);
    }

    public void ClickCommand()
    {
      if (this.click == null)
        return;
      this.click((object) this, this.e);
    }

    protected Texture2D GetTexture(
      int r,
      int g,
      int b,
      int width,
      int height,
      GraphicsDevice gd)
    {
      Texture2D texture2D = new Texture2D(gd, width, height);
      Color[] data = new Color[width * height];
      for (int index = 0; index < data.Length; ++index)
        data[index] = new Color(r, g, b);
      texture2D.SetData<Color>(data);
      return texture2D;
    }

    public delegate void ClickHandler(object sender, EventArgs e);

    public delegate void RightClickHandler(object sender, EventArgs e);

    public delegate void ScrollOver(object sender, EventArgs eS);

    public delegate void ScrollOut(object sender, EventArgs eO);
  }
}
