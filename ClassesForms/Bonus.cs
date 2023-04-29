using Classes;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ClassesForms
{
    public enum BonusType
    {
        Points_100
    }
    internal class Bonus: DisplayObject
    {
        private static readonly string blueTexturePath = @"textures\BlueBonus.png";
        public readonly int defaultSpeed = 2;

        public BonusType type;
        public Bonus(int x, int y): base()
        {
            shape = new RectangleShape();
            ((RectangleShape)shape).Size = new Vector2f(48, 24);
            shape.Position = new Vector2f(x, y);
            InitCoordinates();

            Random random = new Random();
            type = (BonusType)random.Next(Enum.GetValues(typeof(BonusType)).Length);
            shape.Texture = new Texture(type switch
            {
                BonusType.Points_100 => blueTexturePath
            }
            );

            movable = true;
            breaking = false;
            speed = new Vector2i(0, defaultSpeed);
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            ((Drawable)shape).Draw(target, states);
        }

        public override void OnCollision(object? sender, CollisionEventArgs e)
        {
            DisplayObject obj = (e.obj1 == this) ? e.obj2 : e.obj1;
            if (obj is PlayerTile)
            {
                broken = true;
            }
        }
    }
}
