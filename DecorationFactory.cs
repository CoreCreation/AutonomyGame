// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.DecorationFactory
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework.Content;

namespace AutonomyTheGame.Autonomy0._1
{
  internal class DecorationFactory
  {
    private ContentManager Content;

    public DecorationFactory(ContentManager Content) => this.Content = Content;

    public Decoration GetDecoration(
      DecorationType type,
      float x,
      float y,
      bool borderTop,
      bool borderBottom,
      bool borderLeft,
      bool borderRight)
    {
      switch (type)
      {
        case DecorationType.MountedLight:
          return (Decoration) new MountedLight(this.Content, x, y);
        case DecorationType.DoorRight:
          return (Decoration) new DoorRight(this.Content, x, y);
        case DecorationType.DoorLeft:
          return (Decoration) new DoorLeft(this.Content, x, y);
        case DecorationType.Camera:
          return (Decoration) new CameraDecoration(this.Content, x, y);
        default:
          return (Decoration) null;
      }
    }
  }
}
