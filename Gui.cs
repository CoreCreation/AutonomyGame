// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.Gui
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace AutonomyTheGame.Autonomy0._1
{
  internal class Gui
  {
    private const int ButtonSize = 64;
    private EditorMode mode;
    private Game1 parentGame;
    private Viewport view;
    private ContentManager content;
    private MouseState currentMouse;
    private Vector2 click;
    private List<Button> buttons;
    private List<PlaceableButton> tabOneButtons;
    private List<PlaceableButton> tabTwoButtons;
    private List<PlaceableButton> tabThreeButtons;
    private List<Button> currentTab;
    public TextButton SaveButton;
    public TextButton OpenButton;
    public TextButton SaveAsButton;
    public TextButton NewButton;
    public TextButton PlayButton;
    public TextButton LightButton;
    public TextButton ShadeButton;
    public TextButton CleanButton;
    public TextButton tabOneButton;
    public TextButton tabTwoButton;
    public TextButton tabThreeButton;
    private Placeable currentPlaceable;
    private Texture2D currentTexture;
    private AnimatedTexture currentAnimTexture;
    private AreaButton mapArea;
    private Texture2D buttonArea;
    private EditMap map;
    private bool lights;
    private string currentPath;
    private string[] enumNames;
    private MapSelection mapSelection;

    public EditorMode Mode => this.mode;

    public void Instil(ContentManager content, GraphicsDevice gd, EditMap map, Game1 parent)
    {
      this.mode = EditorMode.Editor;
      this.mapSelection = new MapSelection(content, gd, this, "Content/Map");
      this.map = map;
      this.content = content;
      this.currentPath = "TestMap.amf";
      this.parentGame = parent;
      this.view = gd.Viewport;
      this.lights = false;
      this.buttons = new List<Button>();
      this.tabOneButton = new TextButton("One", content, 2, 990, 64, 64, gd);
      this.tabOneButton.click += new Button.ClickHandler(this.TabOneClick);
      this.buttons.Add((Button) this.tabOneButton);
      this.tabTwoButton = new TextButton("One", content, 68, 990, 64, 64, gd);
      this.tabTwoButton.click += new Button.ClickHandler(this.TabTwoClick);
      this.buttons.Add((Button) this.tabTwoButton);
      this.tabTwoButton = new TextButton("Two", content, 68, 990, 64, 64, gd);
      this.tabTwoButton.click += new Button.ClickHandler(this.TabTwoClick);
      this.buttons.Add((Button) this.tabTwoButton);
      this.tabThreeButton = new TextButton("Three", content, 134, 990, 64, 64, gd);
      this.tabThreeButton.click += new Button.ClickHandler(this.TabThreeClick);
      this.buttons.Add((Button) this.tabThreeButton);
      this.SaveButton = new TextButton("Save", content, this.view.Width - 66, this.view.Height - 66, 64, 64, gd);
      this.SaveButton.click += new Button.ClickHandler(this.SaveClick);
      this.buttons.Add((Button) this.SaveButton);
      this.OpenButton = new TextButton("Open", content, this.view.Width - 66, this.view.Height - 132, 64, 64, gd);
      this.OpenButton.click += new Button.ClickHandler(this.OpenClick);
      this.buttons.Add((Button) this.OpenButton);
      this.SaveAsButton = new TextButton("Save \nAs", content, this.view.Width - 66, this.view.Height - 198, 64, 64, gd);
      this.SaveAsButton.click += new Button.ClickHandler(this.SaveAsClick);
      this.buttons.Add((Button) this.SaveAsButton);
      this.NewButton = new TextButton("New", content, this.view.Width - 66, this.view.Height - 264, 64, 64, gd);
      this.NewButton.click += new Button.ClickHandler(this.NewClick);
      this.buttons.Add((Button) this.NewButton);
      this.PlayButton = new TextButton("Play", content, this.view.Width - 66, this.view.Height - 330, 64, 64, gd);
      this.PlayButton.click += new Button.ClickHandler(this.PlayClick);
      this.buttons.Add((Button) this.PlayButton);
      this.LightButton = new TextButton("Light", content, this.view.Width - 66, this.view.Height - 396, 64, 64, gd);
      this.LightButton.click += new Button.ClickHandler(this.LightClick);
      this.buttons.Add((Button) this.LightButton);
      this.ShadeButton = new TextButton("Shade", content, this.view.Width - 66, this.view.Height - 462, 64, 64, gd);
      this.ShadeButton.click += new Button.ClickHandler(this.ShadeClick);
      this.buttons.Add((Button) this.ShadeButton);
      this.CleanButton = new TextButton("Clean", content, this.view.Width - 66, this.view.Height - 528, 64, 64, gd);
      this.CleanButton.click += (Button.ClickHandler) ((sender, e) => map.CleanMap());
      this.buttons.Add((Button) this.CleanButton);
      this.mapArea = new AreaButton(332, 0, this.view.Width - 400, this.view.Height);
      this.mapArea.click += new AreaButton.ClickHandler(this.AreaClick);
      this.mapArea.rightClick += new AreaButton.RightClickHandler(this.RightAreaClick);
      this.currentPlaceable = (Placeable) map.tileFactory.GetTile(TileType.Metal, 0.0f, 0.0f, false, false, false, false);
      this.currentTexture = this.currentPlaceable.GetTexture();
      this.enumNames = Enum.GetNames(typeof (TileType));
      this.tabOneButtons = new List<PlaceableButton>();
      this.tabTwoButtons = new List<PlaceableButton>();
      this.tabThreeButtons = new List<PlaceableButton>();
      this.tabOneButtons = this.GetTab(0, this.enumNames.Length, gd);
      this.tabTwoButtons = this.GetPickUpTab();
      this.tabThreeButtons = this.GetDecorationTab();
      this.currentTab = new List<Button>();
      foreach (Button tabOneButton in this.tabOneButtons)
        this.currentTab.Add(tabOneButton);
      this.buttonArea = Div.getTexture(gd, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 332, 992);
    }

    public void Update(MouseState mouse, Vector2 click)
    {
      switch (this.mode)
      {
        case EditorMode.Editor:
          this.click = click;
          this.currentMouse = mouse;
          foreach (Button button in this.buttons)
            button.Update(mouse);
          foreach (Button button in this.currentTab)
            button.Update(mouse);
          this.mapArea.Update(mouse);
          if (this.currentAnimTexture == null)
            break;
          this.currentAnimTexture.Animate();
          break;
        case EditorMode.MapSelection:
          this.mapSelection.Update(mouse);
          break;
      }
    }

    public void Draw(SpriteBatch sb)
    {
      switch (this.mode)
      {
        case EditorMode.Editor:
          sb.Draw(this.buttonArea, new Vector2(0.0f, 0.0f), Color.White);
          sb.Draw(this.currentTexture, new Vector2((float) (this.view.Width - 66), 2f), new Rectangle?(), Color.White, 0.0f, Vector2.One, 0.25f, SpriteEffects.None, 1f);
          foreach (Button button in this.buttons)
            button.Draw(sb);
          using (List<Button>.Enumerator enumerator = this.currentTab.GetEnumerator())
          {
            while (enumerator.MoveNext())
              enumerator.Current.Draw(sb);
            break;
          }
        case EditorMode.MapSelection:
          this.mapSelection.Draw(sb);
          break;
      }
    }

    private List<PlaceableButton> GetTab(
      int startingValue,
      int endingValue,
      GraphicsDevice gd)
    {
      List<PlaceableButton> placeableButtonList = new List<PlaceableButton>();
      placeableButtonList.Add(new PlaceableButton(0, 0, 64, 64, TileType.Metal, this.content));
      placeableButtonList.Add(new PlaceableButton(0, 66, 64, 64, TileType.PlayerSpawn, this.content));
      placeableButtonList.Add(new PlaceableButton(0, 134, 64, 64, TileType.Exit, this.content));
      placeableButtonList.Add(new PlaceableButton(0, 200, 64, 64, TileType.CheckPoint, this.content));
      placeableButtonList.Add(new PlaceableButton(0, 266, 64, 64, TileType.InteriorBase, this.content));
      placeableButtonList.Add(new PlaceableButton(0, 322, 64, 64, TileType.PlatformLeft, this.content));
      placeableButtonList.Add(new PlaceableButton(0, 388, 64, 64, TileType.PlatformRight, this.content));
      placeableButtonList.Add(new PlaceableButton(0, 454, 64, 64, TileType.InteriorNC, this.content));
      placeableButtonList.Add(new PlaceableButton(0, 520, 64, 64, TileType.DroneSpawn, this.content));
      placeableButtonList.Add(new PlaceableButton(0, 586, 64, 64, TileType.Crate, this.content));
      foreach (Button button in placeableButtonList)
        button.click += new Button.ClickHandler(this.TileClick);
      return placeableButtonList;
    }

    private List<PlaceableButton> GetPickUpTab()
    {
      List<PlaceableButton> placeableButtonList = new List<PlaceableButton>();
      placeableButtonList.Add(new PlaceableButton(66, 66, 66, 66, PickupType.Health, this.content));
      placeableButtonList[placeableButtonList.Count - 1].click += new Button.ClickHandler(this.PickupClick);
      return placeableButtonList;
    }

    private List<PlaceableButton> GetDecorationTab()
    {
      List<PlaceableButton> placeableButtonList = new List<PlaceableButton>();
      placeableButtonList.Add(new PlaceableButton(66, 66, 66, 66, DecorationType.MountedLight, this.content));
      placeableButtonList.Add(new PlaceableButton(66, 132, 66, 66, DecorationType.DoorRight, this.content));
      placeableButtonList.Add(new PlaceableButton(66, 198, 66, 66, DecorationType.DoorLeft, this.content));
      foreach (Button button in placeableButtonList)
        button.click += new Button.ClickHandler(this.DecorationClick);
      return placeableButtonList;
    }

    private void TabOneClick(object sender, EventArgs e)
    {
      this.currentTab = new List<Button>();
      foreach (Button tabOneButton in this.tabOneButtons)
        this.currentTab.Add(tabOneButton);
    }

    private void TabTwoClick(object sender, EventArgs e)
    {
      this.currentTab = new List<Button>();
      foreach (Button tabTwoButton in this.tabTwoButtons)
        this.currentTab.Add(tabTwoButton);
    }

    private void TabThreeClick(object sender, EventArgs e)
    {
      this.currentTab = new List<Button>();
      foreach (Button tabThreeButton in this.tabThreeButtons)
        this.currentTab.Add(tabThreeButton);
    }

    private void TileClick(object sender, EventArgs e)
    {
      this.currentPlaceable = (Placeable) this.map.tileFactory.GetTile(((sender as PlaceableButton).Item as Tile).TileType, 0.0f, 0.0f, false, false, false, false);
      if (this.currentPlaceable.GetTexture() != null)
      {
        this.currentTexture = this.currentPlaceable.GetTexture();
        this.currentAnimTexture = (AnimatedTexture) null;
      }
      else
      {
        this.currentAnimTexture = this.currentPlaceable.GetAnimatedTexture();
        this.currentTexture = (Texture2D) null;
      }
    }

    private void PickupClick(object sender, EventArgs e)
    {
      this.currentPlaceable = (Placeable) this.map.pickupFactory.GetPickup(((sender as PlaceableButton).Item as Pickup).Type, 0.0f, 0.0f);
      if (this.currentPlaceable.GetTexture() != null)
      {
        this.currentTexture = this.currentPlaceable.GetTexture();
        this.currentAnimTexture = (AnimatedTexture) null;
      }
      else
      {
        this.currentAnimTexture = this.currentPlaceable.GetAnimatedTexture();
        this.currentTexture = (Texture2D) null;
      }
    }

    private void DecorationClick(object sender, EventArgs e)
    {
      this.currentPlaceable = (Placeable) this.map.decorationFactory.GetDecoration(((sender as PlaceableButton).Item as Decoration).Type, 0.0f, 0.0f, false, false, false, false);
      if (this.currentPlaceable.GetTexture() != null)
      {
        this.currentTexture = this.currentPlaceable.GetTexture();
        this.currentAnimTexture = (AnimatedTexture) null;
      }
      else
      {
        this.currentAnimTexture = this.currentPlaceable.GetAnimatedTexture();
        this.currentTexture = (Texture2D) null;
      }
    }

    private void AreaClick(object sender, EventArgs e)
    {
      if (this.currentPlaceable is Tile)
        this.map.ChangeTileAt(this.click, (Tile) this.currentPlaceable);
      else if (this.currentPlaceable is Decoration)
        this.map.AddDecoration(this.click, (Decoration) this.currentPlaceable);
      else if (this.currentPlaceable is Pickup)
        this.map.AddPickup(this.click, (Pickup) this.currentPlaceable);
      if (this.lights)
        this.map.Light();
      else
        this.map.Shade();
    }

    private void RightAreaClick(object sender, EventArgs e)
    {
      this.map.ChangeTileAt(this.click, this.map.tileFactory.GetTile(TileType.Empty, 0.0f, 0.0f, false, false, false, false));
      if (this.lights)
        this.map.Light();
      else
        this.map.Shade();
    }

    private void SaveClick(object sender, EventArgs e) => this.map.GetMap(this.currentPath);

    private void SaveAsClick(object sender, EventArgs e)
    {
      this.mode = EditorMode.MapSelection;
      this.mapSelection.SetClickPath();
    }

    private void OpenClick(object sender, EventArgs e)
    {
      this.mode = EditorMode.MapSelection;
      this.mapSelection.SetClickOpen();
    }

    private void NewClick(object sender, EventArgs e)
    {
      this.mode = EditorMode.MapSelection;
      this.mapSelection.SetClickNew();
    }

    private void PlayClick(object sender, EventArgs e)
    {
      this.map.GetMap(this.currentPath);
      this.parentGame.SetPath(this.currentPath);
      this.parentGame.SwitchToGameEdit();
    }

    private void LightClick(object sender, EventArgs e)
    {
      this.map.Light();
      this.lights = true;
    }

    private void ShadeClick(object sender, EventArgs e)
    {
      this.map.Shade();
      this.lights = false;
    }

    public void CancelMapRequest() => this.mode = EditorMode.Editor;

    public void ReturnToGuiOpen(string Path)
    {
      this.parentGame.EditorLoadingToggle();
      this.currentPath = Path;
      this.map.OpenMap(this.content, Path);
      this.mode = EditorMode.Editor;
      this.parentGame.EditorLoadingToggle();
    }

    public void ReturnToGuiSave(string Path)
    {
      this.parentGame.EditorLoadingToggle();
      this.currentPath = Path;
      this.map.GetMap(Path);
      this.mode = EditorMode.Editor;
      this.parentGame.EditorLoadingToggle();
    }

    public void ReurnToGuiNew(string Path)
    {
      this.parentGame.EditorLoadingToggle();
      this.currentPath = Path;
      this.map.NewMap(this.content);
      this.mode = EditorMode.Editor;
      this.parentGame.EditorLoadingToggle();
    }
  }
}
