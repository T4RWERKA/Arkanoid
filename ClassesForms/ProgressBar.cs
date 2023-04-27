using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    internal class ProgressBar
    {
        public int progress { get; private set; }
        private int x, y;
        private Color color;
        public void IncProgress()
        {
            progress++;
        }
        public void Draw() { }
    }
}
