// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.Program
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using System;

namespace AutonomyTheGame.Autonomy0._1
{
  public static class Program
  {
    private static Game1 game;

    [STAThread]
    private static void Main()
    {
      using (Game1 game1 = new Game1())
        game1.Run();
    }
  }
}
