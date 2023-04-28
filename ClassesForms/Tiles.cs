using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    internal class Tiles: List<Tile>
    {
        public void Add(Tile tile, ref int tilesNumber, EventHandler eventHandler)
        {
            ((List<Tile>)this).Add(tile);
            if (tile.breakable && !tile.broken)
            {
                tilesNumber++;
                tile.TileBreaksEvent += eventHandler;
            }
        }
    }
}
