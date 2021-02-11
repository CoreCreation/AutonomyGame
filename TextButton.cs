// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.TextButton
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AutonomyTheGame.Autonomy0._1
{
  internal class TextButton : Button
  {
    private SpriteFont font;
    private string buttonText;
    private Texture2D color;
    private Texture2D plainColor;
    private Texture2D ScrollColor;

    public string ButtonText => this.buttonText;

    public TextButton(
      string text,
      ContentManager content,
      int x,
      int y,
      int width,
      int height,
      GraphicsDevice gd)
      : base(x, y, width, height)
    {
      this.plainColor = this.GetTexture(80, 156, 180, width, height, gd);
      this.color = this.plainColor;
      this.ScrollColor = this.GetTexture(80, 80, 80, width, height, gd);
      this.buttonText = text;
      this.scrollOver += new Button.ScrollOver(this.ScrollOverColor);
      this.scrollOut += new Button.ScrollOut(this.ScrollOutColor);
      this.font = content.Load<SpriteFont>("Font");
    }

    public override void Draw(SpriteBatch sb)
    {
      sb.Draw(this.color, this.area, Color.White);
      sb.DrawString(this.font, this.buttonText, new Vector2(this.location.X, this.location.Y), Color.White);
    }

    public void Draw(SpriteBatch sb, int R, int G, int B)
    {
      sb.Draw(this.color, this.area, new Color(R, G, B));
      sb.DrawString(this.font, this.buttonText, new Vector2(this.location.X + (float) (this.height / 4), this.location.Y + (float) (this.width / 4)), Color.White);
    }

    public void Draw(SpriteBatch sb, Color preColor)
    {
      sb.Draw(this.color, this.area, preColor);
      sb.DrawString(this.font, this.buttonText, new Vector2(this.location.X + (float) (this.height / 4), this.location.Y + (float) (this.width / 4)), Color.White);
    }

    private void ScrollOverColor(object sender, EventArgs e) => this.color = this.ScrollColor;

    private void ScrollOutColor(object sender, EventArgs e) => this.color = this.plainColor;
  }
}
