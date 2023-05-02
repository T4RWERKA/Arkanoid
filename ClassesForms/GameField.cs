using ClassesForms;
using SFML.Graphics;
using SFML.System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Classes
{
    internal class GameField: Drawable
    {
        private static readonly string backgroundImagePath = @"textures\GreenBackground.png";

        public int width, height;
        public Sprite backgroundSprite;
        private Tiles fieldTiles;
        public Balls balls;
        public Bonuses bonuses;
        public PlayerTile playerTile;
        public List<DisplayObject> displayObjects;
        private int tilesNumber;
        public InfoField infoField;

        public event EventHandler WinEvent;
        public event EventHandler<UpdateFieldEventArgs> UpdateFieldEvent;
        public GameField(int gameAreaWidth, int infoAreaWidth, int height)
        {
            fieldTiles = new Tiles();
            balls = new Balls();
            bonuses = new Bonuses();
            this.width = gameAreaWidth;
            this.height = height;

            Texture texture = new Texture(backgroundImagePath);
            backgroundSprite = new Sprite(texture);
            backgroundSprite.Position = new Vector2f(0, 0);
            backgroundSprite.Scale = new Vector2f((float)width / backgroundSprite.TextureRect.Width, (float)height / backgroundSprite.TextureRect.Height);

            infoField = new InfoField(infoAreaWidth, height, new Vector2i(gameAreaWidth, 0));
            UpdateFieldEvent += infoField.OnUpdateField;
        }
        public void InitField(List<DisplayObject> displayObjects)
        {
            this.displayObjects = displayObjects;
            tilesNumber = 0;
            if (displayObjects.Count == 0)
            {
                // создаем четыре прямоугольника вокруг окна
                int borderThickness = 50;
                SFML.Graphics.Color borderColor = SFML.Graphics.Color.Transparent;
                FieldTile[] borders = new FieldTile[4];
                for (int i = 0; i < 4; i++)
                {
                    borders[i] = new FieldTile();
                }

                // верхняя граница
                borders[0].shape = new RectangleShape(new Vector2f(width, borderThickness));
                borders[0].shape.Position = new Vector2f(0, -borderThickness);
                borders[0].left = 0;
                borders[0].right = width;
                borders[0].top = -borderThickness;
                borders[0].bottom = 0;

                // нижняя граница
                borders[1].shape = new RectangleShape(new Vector2f(width, borderThickness));
                borders[1].shape.Position = new Vector2f(0, height);
                borders[1].left = 0;
                borders[1].right = width;
                borders[1].top = height;
                borders[1].bottom = height + borderThickness;

                // левая граница
                borders[2].shape = new RectangleShape(new Vector2f(borderThickness, height));
                borders[2].shape.Position = new Vector2f(-borderThickness, 0);
                borders[2].left = -borderThickness;
                borders[2].right = 0;
                borders[2].top = 0;
                borders[2].bottom = height;

                // правая граница 
                borders[3].shape = new RectangleShape(new Vector2f(borderThickness, height));
                borders[3].shape.Position = new Vector2f(width, 0);
                borders[3].left = width;
                borders[3].right = width + borderThickness;
                borders[3].top = 0;
                borders[3].bottom = height;

                // добавляем границы в список отображаемых объектов
                foreach (DisplayObject? border in borders)
                {
                    ((FieldTile)border).color = MyColor.Transparent;
                    border.InitCoordinates();
                    displayObjects.Add(border);
                    fieldTiles.Add(border as Tile, ref tilesNumber, OnTileBreaks);
                }

                int speedCoef = 100;
                var ball = new Ball(width / 2, (int)(width * 0.75));
                ball.speed = new Vector2i(width / speedCoef, height / speedCoef);
                displayObjects.Add(ball);
                balls.Add(ball);
                playerTile = new PlayerTile((int)width / 2, (int)(ball.bottom + 40));
                displayObjects.Add(playerTile);
                int tileWidth = 64, tileHeight = 32;
                for (int i = 40; i <= width - 40 - tileWidth; i += tileWidth)
                {
                    for (int j = 40; j <= height / 2 - 40 - tileHeight; j += tileHeight)
                    {
                        var tile = new FieldTile(i, j);
                        displayObjects.Add(tile);
                        fieldTiles.Add(tile, ref tilesNumber, OnTileBreaks);
                    }
                }
            }
            else
            {
                for (int i = 0; i < displayObjects.Count; i++)
                {
                    switch (displayObjects[i])
                    {
                        case FieldTile fieldTile:
                            fieldTiles.Add(fieldTile, ref tilesNumber, OnTileBreaks);
                            break;
                        case Ball ball:
                            balls.Add(ball);
                            break;
                        case PlayerTile player:
                            playerTile = player;
                            break;
                        case Bonus bonus:
                            bonuses.Add(bonus);
                            break;
                    }
                }
            }
            UpdateFieldEvent?.Invoke(this, new UpdateFieldEventArgs(displayObjects));
        }
        public void OnTileBreaks(object? sender, EventArgs e)
        {
            tilesNumber--;
            if (tilesNumber == 0)
            {
                WinEvent?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                Random random = new Random();
                double probability = 0.3;
                double randomValue = random.NextDouble();
                if (randomValue < probability)
                {
                    var bonus = new Bonus(((Tile)sender).left, ((Tile)sender).top);
                    bonuses.Add(bonus);
                    displayObjects.Add(bonus);
                }
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(backgroundSprite, states);
            target.Draw(infoField, states);
        }
    }
    internal class UpdateFieldEventArgs : EventArgs
    {
        public List<DisplayObject> displayObjects;
        public UpdateFieldEventArgs(List<DisplayObject> displayObjects)
        {
            this.displayObjects = displayObjects;
        }
    }
}
