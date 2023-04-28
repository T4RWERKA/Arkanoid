using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Classes
{
    internal class GameField
    {
        public int width, height;
        private Tiles fieldTiles;
        public Balls balls;
        public PlayerTile playerTile;
        private ProgressBar progressBar;
        public List<DisplayObject> displayObjects;
        public void InitField(List<DisplayObject> displayObjects)
        {
            if (displayObjects.Count == 0)
            {
                // создаем четыре прямоугольника вокруг окна
                int borderThickness = 50;
                SFML.Graphics.Color borderColor = SFML.Graphics.Color.Red;
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
                foreach (DisplayObject border in borders)
                {
                    border.shape.FillColor = borderColor;
                    border.InitCoordinates();
                    displayObjects.Add(border);
                    fieldTiles.Add(border as Tile);
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
                        fieldTiles.Add(tile);
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
                            fieldTiles.Add(fieldTile);
                            break;
                        case Ball ball:
                            balls.Add(ball);
                            break;
                        case PlayerTile player:
                            playerTile = player;
                            break;
                    }
                }
            }
        }

        public GameField(List<DisplayObject> displayObjects, int width, int height)
        {
            fieldTiles = new Tiles();
            balls = new Balls();
            this.displayObjects = displayObjects;
            this.width = width;
            this.height = height;
        }
    }
}
