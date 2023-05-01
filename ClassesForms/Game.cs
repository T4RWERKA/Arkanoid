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
using System.Reflection;

namespace Classes
{
    public enum MyColor
    {
        Transparent,
        Red
    }
    internal class Game
    {
        private Players players;
        private GameField gameField;
        private List<DisplayObject>? displayObjects;
        public RenderWindow window;
        private bool isStoped;
        private bool isWiorking;

        public event EventHandler<CollisionEventArgs> CollisionEvent;
        public event EventHandler BallFellEvent;

        public Game()
        {
            uint height = 600;
            uint gameFieldWidth = 600;
            uint infoFieldWidth = 300;
            window = new RenderWindow(new VideoMode(gameFieldWidth + infoFieldWidth, height), "Arkanoid");
            window.SetFramerateLimit(60);
            window.Closed += (sender, args) => window.Close();
            window.KeyPressed += OnKeyPressed;
            BallFellEvent += OnBallFell;

            displayObjects = new List<DisplayObject>();
            gameField = new GameField((int)gameFieldWidth, (int)infoFieldWidth, (int)height);
            gameField.infoField.LoseEvent += OnLose;
            BallFellEvent += gameField.infoField.OnBallFell;

            players = new Players();
            var player = new Player(gameField);
            player.WinEvent += OnWin;
            players.Add(player);
        }
        public void InitGame()
        {
            gameField.InitField(displayObjects);
            window.KeyPressed += gameField.playerTile.OnKeyPressed;
            window.KeyReleased += gameField.playerTile.OnKeyReleased;
        }
        private void OnLose(object? sender, EventArgs e)
        {
            Stop();
            MessageBox.Show
            (
                "You lost",
                "You lost",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly
            );
            window.Close();
            isWiorking = false;
        }
        private async void OnBallFell(object? sender, EventArgs e)
        {
            /*await Load("begin_state");
            InitGame();*/
        }
        private void OnWin(object? sender, EventArgs e)
        {
            Stop();
            MessageBox.Show
            (
                $"You won\nScore: {((Player)sender).score}",
                "You won",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly
            );
            window.Close();
            isWiorking = false;
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
            await Load("save");
            InitGame();
        }
        private async void OnMyMenuSave(object? sender, EventArgs e) 
        {
            await Save("save");
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
                            CollisionEvent += obj1.OnCollision;
                            CollisionEvent += obj2.OnCollision;
                            CollisionEvent?.Invoke(this, new CollisionEventArgs(obj1, obj2));
                            CollisionEvent -= obj1.OnCollision;
                            CollisionEvent -= obj2.OnCollision;

                            if (obj1 is FieldTile && obj2 is Ball && obj1.top == gameField.height || 
                                obj2 is FieldTile && obj1 is Ball && obj2.top == gameField.height)
                                BallFellEvent?.Invoke(this, EventArgs.Empty);
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
            window.Draw(gameField);
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
                    window.Clear();
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
        public async Task Load(string fileName) 
        {
            displayObjects = TxtSerializator.DeserializeTxt($"{fileName}.txt");
            displayObjects = await JsonSerializator.DeserializeJson($"{fileName}.json");
        }
        public async Task Save(string fileName) 
        {
            await JsonSerializator.SerializeJson($"{fileName}.json", displayObjects);
            await TxtSerializator.SerializeTxt($"{fileName}.txt", displayObjects);
        }
    }

    internal class CollisionEventArgs: EventArgs
    {
        public DisplayObject obj1, obj2;
        public CollisionEventArgs(DisplayObject obj1, DisplayObject obj2)
        {
            this.obj1 = obj1;
            this.obj2 = obj2;
        }

    }
}
