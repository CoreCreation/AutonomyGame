// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.PlaceableButton
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AutonomyTheGame.Autonomy0._1
{
  internal class PlaceableButton : Button
  {
    private Placeable item;
    private bool tipDrawBool;
    private Texture2D tipBox;
    private Vector2 tipBoxLocation;
    private SpriteFont font;
    private string tipText;

    public Placeable Item => this.item;

    public PlaceableButton(
      int x,
      int y,
      int width,
      int height,
      TileType itemType,
      ContentManager content)
      : base(x, y, width, height)
    {
      this.Instil(content, itemType);
    }

    public PlaceableButton(
      int x,
      int y,
      int width,
      int height,
      DecorationType itemType,
      ContentManager content)
      : base(x, y, width, height)
    {
      this.Instil(content, itemType);
    }

    public PlaceableButton(
      int x,
      int y,
      int width,
      int height,
      PickupType itemType,
      ContentManager content)
      : base(x, y, width, height)
    {
      this.Instil(content, itemType);
    }

    private void Instil(ContentManager content, TileType itemType) => this.item = (Placeable) new TileFactory(content).GetTile(itemType, 0.0f, 0.0f, false, false, false, false);

    private void Instil(ContentManager content, DecorationType itemType) => this.item = (Placeable) new DecorationFactory(content).GetDecoration(itemType, 0.0f, 0.0f, false, false, false, false);

    private void Instil(ContentManager content, PickupType itemType) => this.item = (Placeable) new PickupFactory(content).GetPickup(itemType, 0.0f, 0.0f);

    public override void Update(MouseState mouse)
    {
      base.Update(mouse);
      this.GetTip(mouse);
    }

    public override void Draw(SpriteBatch sb)
    {
      sb.Draw(this.item.GetTexture(), this.location, new Rectangle?(), Color.White, 0.0f, Vector2.One, 0.25f, SpriteEffects.None, 1f);
      if (!this.tipDrawBool || this.tipBox == null)
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
