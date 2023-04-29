using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Color = SFML.Graphics.Color;

namespace Classes
{
    internal class FieldTile : Tile
    {
        private const string transparentTexturePath = @"textures\TransparentTexture.png";
        private const string redTexturePath = @"textures\RedTile.png";

        public int durability;
        public MyColor color 
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                shape.Texture = new Texture(value switch
                {
                    MyColor.Transparent => transparentTexturePath,
                    MyColor.Red => redTexturePath,
                    _ => throw new NotImplementedException(),
                }
                );
            }
        }
        private MyColor _color;
        private Bonuses bonuses;

        public FieldTile() : base() 
        {
        }
        public FieldTile(int x, int y, int width, int height) : 
            base(x, y, width, height)
        { } 
        public FieldTile(int x, int y, int width = 64, int height = 32, bool breakable = true, bool movable = false, bool breaking = false, int speed_X = 0, int speed_Y = 0) : 
            base(x, y, width, height, breakable, movable, breaking, speed_X, speed_Y)
        {
            bonuses = new Bonuses();
            color = MyColor.Red;
        }
        public void AddBonus() { }

        public override void OnCollision(object? sender, CollisionEventArgs e)
        {
            DisplayObject obj = (e.obj1 == this) ? e.obj2 : e.obj1;
            if (breakable && obj.breaking)
            {
                Break();
            }
        }
    }
}
