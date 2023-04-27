using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    internal class Balls: IEnumerable
    {
        public List<Ball> balls;
        public Balls()
        {
            balls = new List<Ball>();
        }
        public void Add(Ball ball)
        {
            balls.Add(ball);
        }
        public void Remove(Ball ball)
        {
            balls.Remove(ball);
        }
        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)balls).GetEnumerator();
        }
    }
}
