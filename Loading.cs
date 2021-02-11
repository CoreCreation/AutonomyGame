// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.Loading
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AutonomyTheGame.Autonomy0._1
{
  internal class Loading
  {
    public TextButton LoadingButton;

    public Loading(ContentManager content, GraphicsDevice gd) => this.LoadingButton = new TextButton(nameof (Loading), content, 50, 50, 50, 50, gd);

    public void Draw(SpriteBatch sb) => this.LoadingButton.Draw(sb);
  }
}
