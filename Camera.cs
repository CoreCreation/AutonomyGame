// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.Camera
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AutonomyTheGame.Autonomy0._1
{
  public class Camera
  {
    protected float zoom;
    protected Matrix transform;
    protected Vector2 position;
    protected Vector2 origin;
    protected Viewport viewport;

    public Vector2 Origin => this.origin;

    public float Width => (float) this.viewport.Width;

    public float Height => (float) this.viewport.Height;

    public float Zoom
    {
      get => this.zoom;
      set => this.zoom = value;
    }

    public Matrix Transform
    {
      get => this.transform;
      set => this.transform = value;
    }

    public Vector2 Position
    {
      get => this.position;
      set => this.position = value;
    }

    public Camera(Viewport viewport)
    {
      this.zoom = 0.5f;
      this.viewport = viewport;
      this.origin = new Vector2((float) viewport.Width / 2f, (float) viewport.Height / 2f);
    }

    public void Instil(Vector2 LookAt) => this.position = LookAt;

    public void LookAt(Vector2 lookAt, Vector2 Location, float xSpeed)
    {
      this.BoundCheck(lookAt);
      if ((double) lookAt.X >= (double) this.position.X + (double) (this.viewport.Width / 6 * 4))
        this.position.X += Math.Abs(xSpeed);
      if ((double) lookAt.X <= (double) this.position.X + (double) (this.viewport.Width / 6 * 2))
        this.position.X -= Math.Abs(xSpeed);
      this.position.Y = Location.Y - (float) (this.viewport.Height / 2);
    }

    private void BoundCheck(Vector2 lookAt)
    {
      if ((double) lookAt.X <= (double) this.viewport.Width + (double) this.position.X && (double) lookAt.X >= (double) this.position.X && ((double) lookAt.Y <= (double) this.viewport.Height + (double) this.position.Y && (double) lookAt.Y >= (double) this.position.Y))
        return;
      this.Instil(new Vector2(lookAt.X - (float) this.viewport.Width / 2f, lookAt.Y - (float) this.viewport.Height / 2f));
    }

    public Matrix CreateMatrix(Vector2 parallax) => Matrix.CreateTranslation(new Vector3(-this.position * parallax, 0.0f)) * Matrix.CreateTranslation(new Vector3(-this.origin, 0.0f)) * Matrix.CreateScale(this.Zoom, this.Zoom, 1f) * Matrix.CreateTranslation(new Vector3(this.origin, 0.0f));
  }
}
