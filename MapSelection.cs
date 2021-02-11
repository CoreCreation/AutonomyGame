// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.MapSelection
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutonomyTheGame.Autonomy0._1
{
  internal class MapSelection : Option
  {
    private IEnumerable<string> holder;
    private Dictionary<TextButton, string> Maps;
    private int buttonSize;
    private int buttonSpace;
    private Gui gui;

    public MapSelection(ContentManager content, GraphicsDevice gd, Gui parent, string Path)
      : base(content, gd)
    {
      this.gui = parent;
      this.buttonSpace = 66;
      this.buttonSize = 64;
      this.holder = Directory.EnumerateFiles(Path);
      this.Maps = new Dictionary<TextButton, string>();
      this.background = Div.getTexture(gd, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, gd.Viewport.Width, gd.Viewport.Height);
      this.GetGrid();
      this.GetButtons(gd);
    }

    public override void Update(MouseState mouse)
    {
      foreach (Button key in this.Maps.Keys)
        key.Update(mouse);
    }

    public override void Draw(SpriteBatch sb)
    {
      sb.Draw(this.background, new Vector2(0.0f, 0.0f), Color.White);
      foreach (Button key in this.Maps.Keys)
        key.Draw(sb);
    }

    private void GetButtons(GraphicsDevice gd)
    {
      int num1 = 1;
      int num2 = 1;
      foreach (string path in this.holder)
      {
        if (num1 < this.width)
        {
          this.Maps.Add(new TextButton(Path.GetFileNameWithoutExtension(path), this.content, this.buttonSpace * num1, this.buttonSpace * num2, this.buttonSize, this.buttonSize, gd), path);
          ++num1;
        }
        else
        {
          num1 = 1;
          ++num2;
        }
      }
    }

    private void GetGrid()
    {
      this.width = this.view.Width / this.buttonSpace - 1;
      this.height = this.view.Height / this.buttonSpace - 1;
      if (this.height * this.width >= this.holder.Count<string>())
        return;
      this.buttonSpace -= 10;
      this.buttonSize -= 10;
      this.GetGrid();
    }

    private void SelectMap(object sender, EventArgs e) => this.gui.ReturnToGuiOpen(this.Maps[sender as TextButton]);

    private void SelectPath(object sender, EventArgs e) => this.gui.ReturnToGuiSave(this.Maps[sender as TextButton]);

    private void SelectNew(object sender, EventArgs e) => this.gui.ReurnToGuiNew(this.Maps[sender as TextButton]);

    public void SetClickOpen()
    {
      foreach (TextButton key in this.Maps.Keys)
      {
        key.click -= new Button.ClickHandler(this.SelectNew);
        key.click -= new Button.ClickHandler(this.SelectPath);
        key.click += new Button.ClickHandler(this.SelectMap);
      }
    }

    public void SetClickNew()
    {
      foreach (TextButton key in this.Maps.Keys)
      {
        key.click -= new Button.ClickHandler(this.SelectMap);
        key.click -= new Button.ClickHandler(this.SelectPath);
        key.click += new Button.ClickHandler(this.SelectNew);
      }
    }

    public void SetClickPath()
    {
      foreach (TextButton key in this.Maps.Keys)
      {
        key.click -= new Button.ClickHandler(this.SelectMap);
        key.click -= new Button.ClickHandler(this.SelectNew);
        key.click += new Button.ClickHandler(this.SelectPath);
      }
    }
  }
}
