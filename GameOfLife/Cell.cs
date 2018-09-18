using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public class Cell
    {
        public int X { get; }
        public int Y { get; }
        public bool State { get; }
        public IEnumerable<Cell> Neighbors { get; private set; }

        public Cell(int x, int y, bool state)
        {
            X = x;
            Y = y;
            State = state;
            Neighbors = new List<Cell>();
        }

        public Cell CalculateNextState()
        {
            var livingNeighbors = Neighbors.Count(c => c.State);
            return new Cell(X, Y, State
                ? livingNeighbors >= 2 && livingNeighbors < 4
                : livingNeighbors == 3);
        }

        public void SetNeighbors(IEnumerable<Cell> neighbors)
        {
            Neighbors = neighbors;
        }

        public override string ToString()
        {
            return State
                ? "o"
                : " ";
        }
    }
}