using System.Collections.Generic;
using GameOfLife;

namespace GameOfLifeTests
{
    public class CellBuilder
    {
        private int _x = 0;
        private int _y = 0;
        private bool _cellState = false;
        private readonly List<Cell> _neighbors = new List<Cell>();

        public CellBuilder ADeadCell()
        {
            return this;
        }

        public CellBuilder ALivingCell()
        {
            _cellState = true;

            return this;
        }

        public CellBuilder WithLivingNeighbors(int count)
        {
            while (count > 0)
            {
                _neighbors.Add(new Cell(0, 0, true));
                count--;
            }

            return this;
        }

        public Cell Build()
        {
            var cell = new Cell(_x, _y, _cellState);
            cell.SetNeighbors(_neighbors);

            return cell;
        }
    }
}