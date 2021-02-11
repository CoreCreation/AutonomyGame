// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.CameraMap
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AutonomyTheGame.Autonomy0._1
{
  internal class CameraMap
  {
    protected float zoom;
    private bool controlKey;
    private int oldScrollWheel;
    protected Matrix transform;
    protected Vector2 position;
    protected Vector2 origin;
    protected Viewport viewport;

    public float Width => (float) this.viewport.Width;

    public float Height => (float) this.viewport.Height;

    public Vector2 Origin => this.origin;

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

    public float Speed => 15f / this.zoom;

    public float MaxSpeed => 30f / this.zoom;

    public CameraMap(Viewport viewport, MouseState mouse)
    {
      this.zoom = 0.5f;
      this.position = Vector2.One;
      this.viewport = viewport;
      this.origin = new Vector2((float) viewport.Width / 2f, (float) viewport.Height / 2f);
      this.oldScrollWheel = mouse.ScrollWheelValue;
    }

    public void LookAt(KeyboardState keyState, MouseState mouse)
    {
      if (mouse.ScrollWheelValue > this.oldScrollWheel)
        this.zoom += 0.05f;
      else if (mouse.ScrollWheelValue < this.oldScrollWheel && (double) this.zoom >= 0.0500000007450581)
        this.zoom -= 0.05f;
      this.oldScrollWheel = mouse.ScrollWheelValue;
      this.controlKey = keyState.IsKeyDown(Keys.LeftShift);
      if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D))
      {
        if (this.controlKey)
          this.position.X += this.MaxSpeed;
        else
          this.position.X += this.Speed;
      }
      if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.A))
      {
        if (this.controlKey)
          this.position.X -= this.MaxSpeed;
        else
          this.position.X -= this.Speed;
      }
      if (keyState.IsKeyDown(Keys.Up) || keyState.IsKeyDown(Keys.W))
      {
        if (this.controlKey)
          this.position.Y -= this.MaxSpeed;
        else
          this.position.Y -= this.Speed;
      }
      if (!keyState.IsKeyDown(Keys.Down) && !keyState.IsKeyDown(Keys.S))
        return;
      if (this.controlKey)
        this.position.Y += this.MaxSpeed;
      else
        this.position.Y += this.Speed;
    }

    public void Move(float x, float y) => this.position = new Vector2(this.position.X += x, this.position.Y += y);

    public Matrix CreateMatrix(Vector2 parallax) => Matrix.CreateTranslation(new Vector3(-this.position * parallax, 0.0f)) * Matrix.CreateTranslation(new Vector3(-this.origin, 0.0f)) * Matrix.CreateScale(this.Zoom, this.Zoom, 1f) * Matrix.CreateTranslation(new Vector3(this.origin, 0.0f));
  }
}
