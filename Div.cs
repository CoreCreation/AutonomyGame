// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.Div
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AutonomyTheGame.Autonomy0._1
{
  internal static class Div
  {
    public static Texture2D getTexture(
      GraphicsDevice gd,
      int r,
      int g,
      int b,
      int width,
      int height)
    {
      Texture2D texture2D = new Texture2D(gd, width, height);
      Color[] data = new Color[width * height];
      for (int index = 0; index < data.Length; ++index)
        data[index] = new Color(r, g, b);
      texture2D.SetData<Color>(data);
      return texture2D;
    }
  }
}
