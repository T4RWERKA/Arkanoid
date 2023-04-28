using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Classes
{
    internal class Ball: DisplayObject
    {
        [JsonInclude]
        public uint damage;
        public Ball() : base() 
        {
            shape = new CircleShape();
            ((CircleShape)shape).Radius = 10;
            shape.FillColor = SFML.Graphics.Color.White;
        }
        public Ball(int x, int y, int radius = 10) : base()
        {
            breakable = false;
            movable = breaking = true;
            shape = new CircleShape();
            shape.Position = new Vector2f(x, y);
            ((CircleShape)shape).Radius = radius;
            InitCoordinates();
            shape.FillColor = SFML.Graphics.Color.White;
            damage = 1;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            ((Drawable)shape).Draw(target, states);
        }

        public override void CollisionHandler(object? sender, CollisionEventArgs e)
        {
            DisplayObject obj = (e.obj1 == this) ? e.obj2 : e.obj1; 
            if (left < obj.left || right > obj.right)
                ReflectX();
            else
                ReflectY();
        }
    }
}
