// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.AnimatedTexture
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AutonomyTheGame.Autonomy0._1
{
  public class AnimatedTexture
  {
    private Texture2D texture;
    private float width;
    private float height;
    private int frames;
    private int totalFrames;
    private int x;
    private int y;
    private bool repeat;
    private int frameTicks;
    private int currentTick;

    public AnimatedTexture(
      Texture2D texture,
      int totalFrames,
      int ticks,
      float height,
      float width,
      bool repeat)
    {
      this.totalFrames = totalFrames;
      this.texture = texture;
      this.frames = 1;
      this.x = 0;
      this.y = 0;
      this.frameTicks = ticks;
      this.currentTick = 0;
      this.width = width;
      this.height = height;
      this.repeat = repeat;
    }

    public void Animate()
    {
      if (this.currentTick++ != this.frameTicks)
        return;
      this.StepFrame();
      this.currentTick = 0;
    }

    public void Draw(SpriteBatch sb, Vector2 position, Color color) => sb.Draw(this.texture, position, new Rectangle?(new Rectangle((int) this.width * this.x, (int) this.height * this.y, (int) this.width, (int) this.height)), color);

    public void Reset()
    {
      this.y = 0;
      this.x = 0;
      this.frames = 0;
    }

    public void StepFrame()
    {
      if (this.frames + 1 < this.totalFrames)
      {
        ++this.frames;
        if ((double) (this.x + 1) * (double) this.width >= (double) this.texture.Width)
        {
          ++this.y;
          this.x = 0;
        }
        else
          ++this.x;
      }
      else
      {
        if (!this.repeat)
          return;
        this.y = 0;
        this.x = 0;
        this.frames = 0;
      }
    }
  }
}
