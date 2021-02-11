// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.Part
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;

namespace AutonomyTheGame.Autonomy0._1
{
  internal struct Part
  {
    private float life;
    private float age;
    private double xmom;
    private double ymom;
    private Vector2 location;

    public float Life => this.life;

    public float Age => this.age;

    public Vector2 Location => this.location;

    public Part(double degree, double speed, int variance, Vector2 start, float life)
    {
      this.age = 0.0f;
      this.life = life;
      this.xmom = GeneralMethods.random.Next(10) <= 5 ? GeneralMethods.getX(speed, GeneralMethods.getAngle(degree + (double) GeneralMethods.random.Next(variance))) : GeneralMethods.getX(speed, GeneralMethods.getAngle(degree - (double) GeneralMethods.random.Next(variance)));
      this.ymom = GeneralMethods.random.Next(10) <= 5 ? GeneralMethods.getY(speed, GeneralMethods.getAngle(degree + (double) GeneralMethods.random.Next(variance))) : GeneralMethods.getY(speed, GeneralMethods.getAngle(degree - (double) GeneralMethods.random.Next(variance)));
      this.location = start;
    }

    public void Update()
    {
      this.location.X += (float) this.xmom;
      this.location.Y += (float) this.ymom;
      ++this.age;
    }
  }
}
