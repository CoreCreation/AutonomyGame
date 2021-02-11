// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.TileButton
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AutonomyTheGame.Autonomy0._1
{
  internal class TileButton : Button
  {
    private Tile tile;
    private bool tipDrawBool;
    private Texture2D tipBox;
    private Vector2 tipBoxLocation;
    private SpriteFont font;
    private string tipText;

    public Tile Tile => this.tile;

    public TileButton(
      int x,
      int y,
      int width,
      int height,
      TileType tileType,
      ContentManager content,
      Color color,
      GraphicsDevice gd)
      : base(x, y, width, height)
    {
      this.Instil(content, tileType);
    }

    public TileButton(
      int x,
      int y,
      int width,
      int height,
      TileType tileType,
      ContentManager content,
      GraphicsDevice gd)
      : base(x, y, 256, 256)
    {
      this.Instil(content, tileType);
      //GraphicsDevice gd = new GraphicsDevice(adapter, graphicsProfile, PresentationParameters)
      this.tipBox = Div.getTexture(gd, 80, 156, 180, 300, 100);
      this.tipBoxLocation = new Vector2((float) (gd.Viewport.Width / 2 - 150), 0.0f);
      this.font = content.Load<SpriteFont>("Font");
      this.tipText = this.tile.TileType.ToString() + "\nCollison:" + this.tile.Collision.ToString() + "\nSpawn:" + this.tile.Spawn.HasValue.ToString() + "\nDamage:" + this.tile.Damage.HasValue.ToString();
    }

    private void Instil(ContentManager content, TileType tileType) => this.tile = new TileFactory(content).GetTile(tileType, 0.0f, 0.0f, false, false, false, false);

    public override void Update(MouseState mouse)
    {
      base.Update(mouse);
      this.GetTip(mouse);
      if (this.tile.Anim == null)
        return;
      this.tile.Anim.Animate();
    }

    public override void Draw(SpriteBatch sb)
    {
      sb.Draw(this.tile.GetTexture(), this.location, new Rectangle?(), Color.White, 0.0f, Vector2.One, 0.25f, SpriteEffects.None, 1f);
      if (!this.tipDrawBool)
        return;
      sb.Draw(this.tipBox, this.tipBoxLocation, Color.White);
      sb.DrawString(this.font, this.tipText, this.tipBoxLocation, Color.White);
    }

    private void GetTip(MouseState mouse)
    {
      if (this.CheckBounds(mouse))
        this.tipDrawBool = true;
      else
        this.tipDrawBool = false;
    }
  }
}
