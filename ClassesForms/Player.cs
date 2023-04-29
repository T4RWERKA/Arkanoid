using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesForms
{
    internal class Player
    {
        public int score;
        public GameField gameField;

        public event EventHandler WinEvent;

        public Player(GameField gameField)
        {
            score = 0;
            this.gameField = gameField;
            gameField.UpdateFieldEvent += OnUpdateField;
            gameField.WinEvent += OnWin;
        }
        public void IncScore(int delta)
        {
            score += delta;
            gameField.infoField.SetScore(score);
        }
        public void OnUpdateField(object? sender, UpdateFieldEventArgs e)
        {
            foreach (var obj in e.displayObjects)
            {
                if (obj is Tile)
                    if (obj.breakable)
                        ((Tile)obj).TileBreaksEvent += OnTileBreaks;
            }
            gameField.playerTile.CatchBonusEvent += OnCatchBonus;
        }
        public void OnTileBreaks(object? sender, EventArgs e)
        {
            if (sender is FieldTile)
            {
                IncScore((sender as FieldTile)?.color switch
                {
                    MyColor.Red => 80
                }
                );
            }
        }
        public void OnCatchBonus(object? sender, CatchBonusEventArgs e)
        {
            switch (e.bonusType)
            {
                case BonusType.Points_100:
                    IncScore(100);
                    break;
            }
        }
        public void OnWin(object? sender, EventArgs e)
        {
            WinEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
