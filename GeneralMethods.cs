// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.GeneralMethods
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using System;

namespace AutonomyTheGame.Autonomy0._1
{
  internal static class GeneralMethods
  {
    public static Random random = new Random();

    public static double getAngle(double number)
    {
      if (number > 360.0)
      {
        number -= 360.0;
        GeneralMethods.getAngle(number);
      }
      return number * Math.PI / 180.0;
    }

    public static double getY(double hypo, double angle) => Math.Sin(angle) * hypo;

    public static double getX(double hypo, double angle) => Math.Cos(angle) * hypo;
  }
}
