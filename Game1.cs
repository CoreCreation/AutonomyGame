// Decompiled with JetBrains decompiler
// Type: AutonomyTheGame.Autonomy0._1.Game1
// Assembly: Autonomy0.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85A77D47-475F-40F4-8DF6-CCEF1A7DF31E
// Assembly location: C:\Program Files (x86)\Core Creation Studios\Autonomy0.1.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AutonomyTheGame.Autonomy0._1
{
  public class Game1 : Game
  {
    private KeyboardState currentKey;
    private KeyboardState previousKey;
    private MouseState currentM;
    private MouseState prevM;
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private Camera camera;
    private Player player;
    private AIOverLord lord;
    public Map map1;
    private string mapPath;
    private Menu menu;
    private Loading loadScreen;
    private Pause pauseScreen;
    private CameraMap cameraMap;
    private Gui gui;
    private GameGUI gameGui;
    private EditMap map;
    private MapSelection mapSelect;
    private Texture2D dask_Backgourd;
    private Scroll Cloud1;
    private Scroll Cloud2;
    private GameState gameState;
    private GameState gameStateIntent;
    private GameState gameStatePrev;

    public Game1()
    {
      this.graphics = new GraphicsDeviceManager((Game) this);
      this.IsMouseVisible = true;
      this.IsFixedTimeStep = true;
      this.graphics.PreferredBackBufferHeight = (int) this.GetResolution().Y;
      this.graphics.PreferredBackBufferWidth = (int) this.GetResolution().X;
      this.graphics.IsFullScreen = true;
      //this.GraphicsDevice.Viewport = new Viewport(0, 0, this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight);
      this.Content.RootDirectory = "Content";
      this.gameState = GameState.Menu;
      this.mapPath = "Content//Story//PlaceHolder.amf";
    }

    protected override void Initialize()
    {
      this.loadScreen = new Loading(this.Content, this.GraphicsDevice);
      base.Initialize();
    }

    protected override void LoadContent()
    {
      this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
      switch (this.gameStateIntent)
      {
        case GameState.Menu:
          this.menu = new Menu();
          this.menu.Instil(this.Content, this.GraphicsDevice);
          this.menu.start.click += new Button.ClickHandler(this.SwitchToGame);
          this.menu.editor.click += new Button.ClickHandler(this.SwitchToEditor);
          break;
        case GameState.InGame:
          this.camera = new Camera(this.GraphicsDevice.Viewport);
          this.player = new Player();
          this.lord = new AIOverLord();
          this.LoadMap(this.mapPath);
          this.map1.Instil(this, this.Content);
          this.player.Instil(this.Content, this.map1);
          this.camera.Instil(new Vector2(this.player.Location.X - (float) (this.GraphicsDevice.Viewport.Width / 2), this.player.Location.Y));
          this.lord.Instil(this.map1, this.Content, this.player);
          this.gameGui = new GameGUI(this.Content, this.player, this.map1);
          this.dask_Backgourd = this.Content.Load<Texture2D>("Graphics\\Dask_Backgourd");
          this.Cloud1 = new Scroll(new Texture2D[3]
          {
            this.Content.Load<Texture2D>("Graphics\\Cloud\\Cloud_Layer1_001"),
            this.Content.Load<Texture2D>("Graphics\\Cloud\\Cloud_Layer1_002"),
            this.Content.Load<Texture2D>("Graphics\\Cloud\\Cloud_Layer1_003")
          }, 4f, this.map1.xMax, this.GraphicsDevice.Viewport.Width);
          this.Cloud2 = new Scroll(new Texture2D[3]
          {
            this.Content.Load<Texture2D>("Graphics\\Cloud\\Cloud_Layer2_001"),
            this.Content.Load<Texture2D>("Graphics\\Cloud\\Cloud_Layer2_002"),
            this.Content.Load<Texture2D>("Graphics\\Cloud\\Cloud_Layer2_003")
          }, 3f, this.map1.xMax, this.GraphicsDevice.Viewport.Width);
          this.pauseScreen = new Pause(this.Content, this.GraphicsDevice, this.GraphicsDevice.Viewport);
          this.pauseScreen.menu.click += new Button.ClickHandler(this.SwitchToMenuGame);
          this.pauseScreen.resume.click += new Button.ClickHandler(this.ResumeGame);
          break;
        case GameState.Editor:
          this.GraphicsDevice.Viewport = new Viewport(0, 0, this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight);
          this.cameraMap = new CameraMap(this.GraphicsDevice.Viewport, this.currentM);
          this.gui = new Gui();
          this.map = new EditMap(this, this.Content, this.cameraMap);
          this.gui.Instil(this.Content, this.GraphicsDevice, this.map, this);
          this.pauseScreen = new Pause(this.Content, this.GraphicsDevice, this.GraphicsDevice.Viewport);
          this.pauseScreen.menu.click += new Button.ClickHandler(this.SwitchToMenuEditor);
          this.pauseScreen.resume.click += new Button.ClickHandler(this.ResumeEditor);
          break;
      }
    }

    protected override void UnloadContent()
    {
    }

    protected override void Update(GameTime gameTime)
    {
      this.prevM = this.currentM;
      this.currentM = Mouse.GetState();
      this.previousKey = this.currentKey;
      this.currentKey = Keyboard.GetState();
      this.gameStatePrev = this.gameState;
      switch (this.gameState)
      {
        case GameState.Menu:
          if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            this.Exit();
          this.menu.Update(this.currentM);
          break;
        case GameState.InGame:
          if (this.currentKey.IsKeyDown(Keys.Escape) && !this.previousKey.IsKeyDown(Keys.Escape))
            this.PauseGame();
          this.camera.LookAt(this.player.Location, this.player.Location, this.player.XSpeed);
          this.map1.Update(this.camera);
          this.lord.Update(gameTime);
          this.lord.SetTarget((Entity) this.player);
          this.Cloud1.Update();
          this.Cloud2.Update();
          this.player.Update(gameTime.ElapsedGameTime, this.currentKey, this.previousKey);
          break;
        case GameState.Loading:
          return;
        case GameState.Paused:
          if (this.currentKey.IsKeyDown(Keys.Escape) && !this.previousKey.IsKeyDown(Keys.Escape))
            this.pauseScreen.resume.ClickCommand();
          this.pauseScreen.Update(this.currentM);
          break;
        case GameState.Editor:
          if (this.currentKey.IsKeyDown(Keys.Escape) && !this.previousKey.IsKeyDown(Keys.Escape))
          {
            switch (this.gui.Mode)
            {
              case EditorMode.Editor:
                this.PauseGame();
                break;
              case EditorMode.MapSelection:
                this.gui.CancelMapRequest();
                break;
            }
          }
          this.cameraMap.LookAt(this.currentKey, this.currentM);
          this.map.Update();
          this.gui.Update(this.currentM, Vector2.Transform(new Vector2((float) this.currentM.X, (float) this.currentM.Y), Matrix.Invert(this.cameraMap.CreateMatrix(Vector2.One))));
          break;
      }
      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      this.GraphicsDevice.Clear(Color.DodgerBlue);
      switch (this.gameState)
      {
        case GameState.Menu:
          this.spriteBatch.Begin();
          this.menu.Draw(this.spriteBatch);
          this.spriteBatch.End();
          break;
        case GameState.InGame:
          this.spriteBatch.Begin();
          this.spriteBatch.Draw(this.dask_Backgourd, new Vector2(0.0f, 0.0f), Color.White);
          this.spriteBatch.End();
          this.spriteBatch.Begin(SpriteSortMode.Deferred, (BlendState) null, (SamplerState) null, (DepthStencilState) null, (RasterizerState) null, (Effect) null, this.camera.CreateMatrix(new Vector2(0.25f, 0.0f)));
          this.Cloud2.Draw(this.spriteBatch);
          this.spriteBatch.End();
          this.spriteBatch.Begin(SpriteSortMode.Deferred, (BlendState) null, (SamplerState) null, (DepthStencilState) null, (RasterizerState) null, (Effect) null, this.camera.CreateMatrix(new Vector2(0.5f, 0.0f)));
          this.Cloud1.Draw(this.spriteBatch);
          this.spriteBatch.End();
          this.spriteBatch.Begin(SpriteSortMode.Deferred, (BlendState) null, (SamplerState) null, (DepthStencilState) null, (RasterizerState) null, (Effect) null, this.camera.CreateMatrix(Vector2.One));
          this.map1.Draw(this.spriteBatch);
          this.player.Draw(this.spriteBatch);
          this.lord.Draw(this.spriteBatch);
          this.spriteBatch.End();
          this.spriteBatch.Begin();
          this.gameGui.Draw(this.spriteBatch);
          this.spriteBatch.End();
          break;
        case GameState.Loading:
          this.spriteBatch.Begin();
          this.loadScreen.Draw(this.spriteBatch);
          this.spriteBatch.End();
          break;
        case GameState.Paused:
          this.spriteBatch.Begin();
          this.pauseScreen.Draw(this.spriteBatch);
          this.spriteBatch.End();
          break;
        case GameState.Editor:
          this.GraphicsDevice.Clear(Color.White);
          this.spriteBatch.Begin(SpriteSortMode.Deferred, (BlendState) null, (SamplerState) null, (DepthStencilState) null, (RasterizerState) null, (Effect) null, this.cameraMap.CreateMatrix(Vector2.One));
          this.map.Draw(this.spriteBatch);
          this.spriteBatch.End();
          this.spriteBatch.Begin();
          this.gui.Draw(this.spriteBatch);
          this.spriteBatch.End();
          break;
      }
      base.Draw(gameTime);
    }

    private Vector2 GetResolution()
    {
      float x = 0.0f;
      float y = 0.0f;
      foreach (DisplayMode supportedDisplayMode in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
      {
        if ((double) x == 0.0)
        {
          x = (float) supportedDisplayMode.Width;
          y = (float) supportedDisplayMode.Height;
        }
        if ((double) supportedDisplayMode.Width > (double) x)
        {
          x = (float) supportedDisplayMode.Width;
          y = (float) supportedDisplayMode.Height;
        }
      }
      return new Vector2(x, y);
    }

    private void LoadMap(string Path)
    {
      using (FileStream fileStream = File.Open(Path, FileMode.Open))
        this.map1 = (Map) new BinaryFormatter().Deserialize((Stream) fileStream);
    }

    public void EditorLoadingToggle()
    {
      if (this.gameState == GameState.Editor)
        this.gameState = GameState.Loading;
      else
        this.gameState = GameState.Editor;
    }

    public void SetPath(string Path) => this.mapPath = Path;

    public void SwitchToGameEdit()
    {
      this.gameState = GameState.Loading;
      this.RunOneFrame();
      this.gameStateIntent = GameState.InGame;
      this.gameStatePrev = GameState.Editor;
      this.UnloadEditor();
      this.LoadContent();
      this.gameState = GameState.InGame;
    }

    public void SwitchToGame(object sender, EventArgs e)
    {
      this.gameState = GameState.Loading;
      this.RunOneFrame();
      this.gameStateIntent = GameState.InGame;
      this.gameStatePrev = GameState.Menu;
      this.LoadContent();
      this.UnloadMenu();
      this.gameState = GameState.InGame;
    }

    public void SwitchToMenuGame(object sender, EventArgs e)
    {
      this.gameState = GameState.Loading;
      this.RunOneFrame();
      this.gameStateIntent = GameState.Menu;
      this.gameStatePrev = GameState.InGame;
      this.LoadContent();
      this.UnloadGame();
      this.gameState = GameState.Menu;
    }

    public void SwitchToMenuEditor(object sender, EventArgs e)
    {
      this.gameState = GameState.Loading;
      this.RunOneFrame();
      this.gameStateIntent = GameState.Menu;
      this.gameStatePrev = GameState.Editor;
      this.LoadContent();
      this.UnloadEditor();
      this.gameState = GameState.Menu;
    }

    public void SwitchToEditor(object sender, EventArgs e)
    {
      this.gameState = GameState.Loading;
      this.RunOneFrame();
      this.gameStateIntent = GameState.Editor;
      this.gameStatePrev = GameState.Menu;
      this.LoadContent();
      this.UnloadMenu();
      this.gameState = GameState.Editor;
    }

    public void PauseGame() => this.gameState = GameState.Paused;

    public void ResumeGame(object sender, EventArgs e) => this.gameState = GameState.InGame;

    public void ResumeEditor(object sender, EventArgs e) => this.gameState = GameState.Editor;

    private void UnloadGame()
    {
      this.camera = (Camera) null;
      this.player = (Player) null;
      this.lord = (AIOverLord) null;
      this.map1 = (Map) null;
      this.pauseScreen = (Pause) null;
      this.gameGui = (GameGUI) null;
      this.dask_Backgourd = (Texture2D) null;
      this.Cloud1 = (Scroll) null;
      this.Cloud2 = (Scroll) null;
    }

    private void UnloadMenu() => this.menu = (Menu) null;

    private void UnloadEditor()
    {
      this.gui = (Gui) null;
      this.map = (EditMap) null;
      this.cameraMap = (CameraMap) null;
      this.pauseScreen = (Pause) null;
    }
  }
}
