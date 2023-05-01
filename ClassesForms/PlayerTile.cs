using ClassesForms;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    internal class PlayerTile: Tile
    {
        private static readonly string texturePath = @"textures\PlayerTile.png";

        private int defaultSpeed = 5;

        public event EventHandler<CatchBonusEventArgs> CatchBonusEvent;
        public PlayerTile() : base() 
        {
            shape.Texture = new Texture(texturePath);
            ((RectangleShape)shape).Size = new Vector2f(shape.Texture.Size.X * 3, shape.Texture.Size.Y * 3);
        }
        public PlayerTile(int x, int y, int width = 64, int height = 32, bool breakable = false, bool movable = true, bool breaking = false) :
            base(x, y, width, height, breakable, movable, breaking)
        {
            shape.Texture = new Texture(texturePath);
            ((RectangleShape)shape).Size = new Vector2f(shape.Texture.Size.X * 3, shape.Texture.Size.Y * 3);
            InitCoordinates();
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            ((Drawable)shape).Draw(target, states);
        }
        public void LeftSpeed()
        {
            speed = new Vector2i(-defaultSpeed, 0);
        }
        public void RightSpeed()
        {
            speed = new Vector2i(defaultSpeed, 0);
        }
        public void Stop()
        {
            speed = new Vector2i(0, 0);
        }

        public override void OnCollision(object? sender, CollisionEventArgs e)
        {
            DisplayObject obj = (e.obj1 == this) ? e.obj2 : e.obj1;
            if (obj is Tile)
            {
                ReflectX();
                Move();
            }
            else if (obj is Bonus)
            {
                CatchBonusEvent?.Invoke(this, new CatchBonusEventArgs((obj as Bonus).bonusType));
            }
        }
        public void OnKeyPressed(object? sender, SFML.Window.KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Left)
            {
                LeftSpeed();
            }
            else if (e.Code == Keyboard.Key.Right)
            {
                RightSpeed();
            }
        }
        public void OnKeyReleased(object? sender, SFML.Window.KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Left || e.Code == Keyboard.Key.Right)
            {
                Stop();
            }
        }
    }
    public class CatchBonusEventArgs: EventArgs
    {
        public BonusType bonusType;
        public CatchBonusEventArgs(BonusType bonusType) 
        {
            this.bonusType = bonusType;
        }
    }
}
