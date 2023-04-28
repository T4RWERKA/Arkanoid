using SFML.Graphics;
using SFML.Graphics.Glsl;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static SFML.Window.Mouse;

namespace Classes
{
    internal abstract class DisplayObject : Drawable
    {
        public bool movable, breakable, broken, breaking;
        public int left, right, top, bottom;
        [JsonIgnore]
        public Shape shape;
        public Vector2i speed;
        public abstract void Draw(RenderTarget target, RenderStates states);
        public abstract void CollisionHandler(object? sender, CollisionEventArgs e);
        public void InitCoordinates()
        {
            FloatRect rect = shape.GetGlobalBounds();
            left = (int)rect.Left;
            top = (int)rect.Top;
            right = (int)(rect.Left + rect.Width);
            bottom = (int)(rect.Top + rect.Height);
        }
        [JsonConstructor]
        public DisplayObject()
        {
            broken = false;
        }
        public bool MyIntersects(DisplayObject obj)
        {
            return shape.GetGlobalBounds().Intersects(obj.shape.GetGlobalBounds());
            //return left < obj.right && right > obj.left && top < obj.bottom && bottom > obj.top;
        }
        public void ReflectX()
        {
            speed.X *= -1;
        }
        public void ReflectY()
        {
            speed.Y *= -1;
        }
        public void Move()
        {
            if (movable)
            {
                shape.Position += (Vector2f)speed;
                left += speed.X;
                right += speed.X;
                top += speed.Y;
                bottom += speed.Y;
            }
        }
    }
}
