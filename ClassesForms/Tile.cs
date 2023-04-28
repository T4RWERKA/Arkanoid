using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    internal abstract class Tile : DisplayObject
    {
        public Tile() : base()
        { 
            shape = new RectangleShape();
            ((RectangleShape)shape).Size = new Vector2f(64, 32);
        }
        public Tile(int x, int y, int width, int height)
        {
            shape = new RectangleShape();
            shape.Position = new Vector2f(x, y);
            InitCoordinates();
        }
        public Tile(int x, int y, int width = 64, int height = 32, bool breakable = true, bool movable = false, bool breaking = false, int speed_X = 0, int speed_Y = 0) : base()
        {
            this.breakable = breakable;
            this.movable = movable;
            this.breaking = breaking;
            speed = new Vector2i(speed_X, speed_Y);
            shape = new RectangleShape();
            shape.Position = new Vector2f(x, y);
            ((RectangleShape)shape).Size = new Vector2f(width, height);
            InitCoordinates();
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            ((Drawable)shape).Draw(target, states);
        }
    }
}
