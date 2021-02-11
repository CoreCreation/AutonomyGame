// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.Scroll
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AutonomyTheGame.Autonomy0._1
{
  internal class Scroll
  {
    private Texture2D[] textures;
    private float speed;
    private float scrollWidth;
    private Scroll.ScrollTexture[] texturesToDraw;
    private int viewWidth;

    public Scroll(Texture2D[] textures, float speed, float scrollWidth, int viewWidth)
    {
      this.textures = textures;
      this.speed = speed;
      this.scrollWidth = scrollWidth;
      this.viewWidth = viewWidth;
      this.texturesToDraw = new Scroll.ScrollTexture[4];
      this.texturesToDraw[3] = this.GetScrollTexture();
    }

    public void Update()
    {
      for (int index = 0; index < this.texturesToDraw.Length; ++index)
      {
        if (this.texturesToDraw[index].Texture != null)
        {
          this.texturesToDraw[index].position.X += this.speed;
          if ((double) this.texturesToDraw[index].Position.X >= (double) this.scrollWidth + 1000.0)
            this.texturesToDraw[index] = this.GetScrollTexture();
        }
        else if ((double) this.texturesToDraw[index + 1].Position.X > (double) this.scrollWidth / 2.0)
          this.texturesToDraw[index] = this.GetScrollTexture();
      }
    }

    public void Draw(SpriteBatch sb)
    {
      foreach (Scroll.ScrollTexture scrollTexture in this.texturesToDraw)
      {
        if (scrollTexture.Texture != null)
          sb.Draw(scrollTexture.Texture, scrollTexture.Position, Color.White);
      }
    }

    private Scroll.ScrollTexture GetScrollTexture()
    {
      Texture2D texture = this.textures[GeneralMethods.random.Next(this.textures.Length)];
      return new Scroll.ScrollTexture(texture, new Vector2((float) -(texture.Width + this.viewWidth), 0.0f));
    }

    private struct ScrollTexture
    {
      private Texture2D texture;
      public Vector2 position;

      public Texture2D Texture => this.texture;

      public Vector2 Position => this.position;

      public ScrollTexture(Texture2D texture, Vector2 position)
      {
        this.texture = texture;
        this.position = position;
      }
    }
  }
}
