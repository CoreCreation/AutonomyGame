// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.Particle
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AutonomyTheGame.Autonomy0._1
{
  public class Particle
  {
    private GraphicsDevice gd;
    private Vector2 particleLocation;
    private float speed;
    private float? life;
    private float partileLife;
    private int size;
    private float degreeRange;
    private double sprayAngle;
    private double sprayRandian;
    private double sprayAngleX;
    private double sprayAngleY;
    private Color startColor;
    private Color endColor;
    private Texture2D particle;
    private Part[] particles;

    public Particle(
      Vector2 particleLocation,
      int particleCount,
      float speed,
      float? lifespan,
      float lifeParticle,
      int size,
      float degreeRange,
      double sprayAngle,
      Color color)
    {
      //this.gd = new GraphicsDevice();
      this.particleLocation = particleLocation;
      this.speed = speed;
      this.life = lifespan;
      this.partileLife = lifeParticle;
      this.size = size;
      this.degreeRange = degreeRange;
      this.sprayAngle = sprayAngle;
      this.startColor = color;
      this.endColor = new Color((int) this.startColor.R, (int) this.startColor.G, (int) this.startColor.B, 50);
      this.particle = Div.getTexture(this.gd, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, size, size);
      this.particles = new Part[particleCount];
    }

    public void Update()
    {
      if (this.life.HasValue)
      {
        float? life = this.life;
        if (((double) life.GetValueOrDefault() > 0.0 ? 0 : (life.HasValue ? 1 : 0)) != 0)
          return;
      }
      if (this.life.HasValue)
      {
        Particle particle = this;
        float? life = particle.life;
        particle.life = life.HasValue ? new float?(life.GetValueOrDefault() - 1f) : new float?();
      }
      for (int index = 0; index < this.particles.Length; ++index)
      {
        if ((double) this.particles[index].Age >= (double) this.partileLife && GeneralMethods.random.Next(0, 100) > 50)
          this.particles[index] = new Part(this.sprayAngle, (double) this.speed, (int) this.degreeRange, this.particleLocation, this.partileLife);
        this.particles[index].Update();
      }
    }

    public void Draw(SpriteBatch sb)
    {
      for (int index = 0; index < this.particles.Length; ++index)
      {
        if ((double) this.particles[index].Age < (double) this.particles[index].Life)
          sb.Draw(this.particle, this.particles[index].Location, this.startColor);
      }
    }
  }
}
