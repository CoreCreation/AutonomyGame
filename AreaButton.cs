// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.AreaButton
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace AutonomyTheGame.Autonomy0._1
{
  internal class AreaButton : Button
  {
    public new EventArgs e;

    public event AreaButton.ClickHandler click;

    public event AreaButton.RightClickHandler rightClick;

    public AreaButton(int x, int y, int width, int height)
      : base(x, y, width, height)
    {
    }

    protected override void ClickCheck()
    {
      if (this.click == null || this.currentState.LeftButton != ButtonState.Pressed || !this.CheckBounds(this.currentState))
        return;
      this.click((object) this, this.e);
    }

    protected override void RightClickCheck()
    {
      if (this.click == null || this.currentState.RightButton != ButtonState.Pressed || !this.CheckBounds(this.currentState))
        return;
      this.rightClick((object) this, this.e);
    }

    public override void Draw(SpriteBatch sb)
    {
    }

    public new delegate void ClickHandler(object sender, EventArgs e);

    public new delegate void RightClickHandler(object sender, EventArgs e);
  }
}
