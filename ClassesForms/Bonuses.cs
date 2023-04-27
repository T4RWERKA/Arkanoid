using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    internal class Bonuses
    {
        private List<Bonus> bonuses;
        public Bonuses()
        {
            bonuses = new List<Bonus>();
        }
        public void Add(Bonus bonus)
        {
            bonuses.Add(bonus);
        }
        public void Remove(Bonus bonus)
        {
            bonuses.Remove(bonus);
        }
    }
    internal class Bonus
    {
        public enum BonusType
        {
            IncreaseDamage
        }

        private BonusType type;
        public Bonus(BonusType type) { this.type = type; }
        public static BonusType GetRandomType() { return BonusType.IncreaseDamage; }
        public void Draw() { }
        public void Move() { }
        public void Appear() { }
    }
}
