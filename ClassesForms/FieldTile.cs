using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Classes
{
    internal class FieldTile: Tile
    {
        [JsonInclude]
        public int durability;
        private Bonuses bonuses;

        public FieldTile() : base() 
        {
            shape.Texture = new Texture("D:\\Study\\ООП\\Classes\\Classes\\Textures\\RedTile.png");
        }
        public FieldTile(int x, int y, int width, int height) : 
            base(x, y, width, height)
        { } 
        public FieldTile(int x, int y, int width = 64, int height = 32, bool breakable = true, bool movable = false, bool breaking = false, int speed_X = 0, int speed_Y = 0) : 
            base(x, y, width, height, breakable, movable, breaking, speed_X, speed_Y)
        {
            bonuses = new Bonuses();
            shape.Texture = new Texture("D:\\Study\\ООП\\Classes\\Classes\\Textures\\RedTile.png");
        }
        public void AddBonus() { }

        public override void OnCollision(object? sender, CollisionEventArgs e)
        {
            DisplayObject obj = (e.obj1 == this) ? e.obj2 : e.obj1;
            if (breakable)
            {
                Break();
            }
        }
    }
}
