using ClassesForms;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Color = SFML.Graphics.Color;
using System.Text.Json;

namespace Classes
{
    internal class Game
    {
        private Players players;
        private GameField gameField;
        private List<DisplayObject> displayObjects;
        private Sprite backgroundSprite;
        public RenderWindow window;
        private bool isStoped;
        private bool isWiorking;

        public event EventHandler<CollisionEventArgs> CollisionEvent;

        public Game()
        {
            window = new RenderWindow(new VideoMode(600, 600), "Arkanoid");
            window.SetFramerateLimit(60);
            window.Closed += (sender, args) => window.Close();
            window.KeyPressed += OnKeyPressed;

            displayObjects = new List<DisplayObject>();
            gameField = new GameField(displayObjects, (int)window.Size.X, (int)window.Size.Y);
            Texture texture = new Texture("D:\\Study\\ООП\\Classes\\Classes\\Textures\\GreenBackground.png");
            backgroundSprite = new Sprite(texture);
            backgroundSprite.Scale = new Vector2f((float)window.Size.X / backgroundSprite.TextureRect.Width, (float)window.Size.Y / backgroundSprite.TextureRect.Height);
        }
        public void InitGame()
        {
            gameField.InitField(displayObjects);
            window.KeyPressed += gameField.playerTile.OnKeyPressed;
            window.KeyReleased += gameField.playerTile.OnKeyReleased;
        }
        private void OnKeyPressed(object? sender, SFML.Window.KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
            {
                Stop();
                var menu = new MyMenu();
                menu.Show();
                menu.ExitEvent += OnMyMenuExit;
                menu.ContinueEvent += OnMyMenuContinue;
                menu.FormClosed += OnMyMenuContinue;
                menu.SaveEvent += OnMyMenuSave;
                menu.LoadEvent += OnMyMenuLoad;
            }
        }
        private void OnMyMenuExit(object? sender, EventArgs e)
        {
            window.Close();
            isWiorking = false;
        }
        private void OnMyMenuContinue(object? sender, EventArgs e) 
        { 
            Continue(); 
        }
        private async void OnMyMenuLoad(object? sender, EventArgs e)
        {
            await Load();
            InitGame();
        }
        private void OnMyMenuSave(object? sender, EventArgs e) 
        {
            Save();
        }
        public void CheckCollisions()
        {
            for (int i = 0; i <  displayObjects.Count; i++) 
            {
                DisplayObject obj1 = displayObjects[i];
                if (!obj1.broken)
                {
                    for (int j = i + 1; j < displayObjects.Count; j++)
                    {
                        DisplayObject obj2 = displayObjects[j];
                        if (!obj2.broken && obj1.MyIntersects(obj2))
                        {
                            CollisionEvent += obj1.CollisionHandler;
                            CollisionEvent += obj2.CollisionHandler;
                            CollisionEvent?.Invoke(this, new CollisionEventArgs(obj1, obj2));
                            CollisionEvent -= obj1.CollisionHandler;
                            CollisionEvent -= obj2.CollisionHandler;
                        }
                    }
                }
            }
        }
        public void MoveObjects()
        {
            foreach (DisplayObject obj in displayObjects)
            {
                if (obj.movable)
                    obj.Move();
            }
        }

        private void DrawField()
        {
            window.Draw(backgroundSprite);
            foreach (DisplayObject obj in displayObjects)
            {
                if (!obj.broken)
                    window.Draw(obj);
            }
        }

        public void GameLoop()
        {
            isWiorking = true;
            window.Closed += (sender, args) => { isWiorking = false; };
            while (isWiorking)
            {
                window.DispatchEvents();
                if (!isStoped)
                {
                    CheckCollisions();
                    MoveObjects();
                    DrawField();
                    window.Display();
                }
            }
        }
        public void Continue() 
        {
            isStoped = false;
        }
        public void Stop() 
        {
            isStoped = true;
        }
        public async Task Load() 
        {
            var serializeOptions = new JsonSerializerOptions();
            serializeOptions.Converters.Add(new DisplayObjectConverterWithTypeDiscriminator());

            using (FileStream fs = new FileStream("user.json", FileMode.Open, FileAccess.Read))
            {
                displayObjects = await JsonSerializer.DeserializeAsync<List<DisplayObject>>(fs, serializeOptions);
            }
        }
        private async Task SerializeJson(FileStream fs)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                IncludeFields = true
            };
            serializeOptions.Converters.Add(new DisplayObjectConverterWithTypeDiscriminator());

            await JsonSerializer.SerializeAsync<List<DisplayObject>>(fs, displayObjects, serializeOptions);
        }
        public async void Save() 
        {
            using (FileStream fs = new FileStream("user.json", FileMode.Create, FileAccess.Write))
            {
                await SerializeJson(fs);
            }
        }
    }

    internal class CollisionEventArgs : EventArgs
    {
        public DisplayObject obj1, obj2;
        public CollisionEventArgs(DisplayObject obj1, DisplayObject obj2)
        {
            this.obj1 = obj1;
            this.obj2 = obj2;
        }

    }
}
