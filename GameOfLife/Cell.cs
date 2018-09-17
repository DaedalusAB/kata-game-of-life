using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public class Cell
    {
        public int X { get; }
        public int Y { get; }
        public bool State { get; private set; }
        public bool NextState { get; private set; }
        public IEnumerable<Cell> Neighbors { get; private set; }

        public Cell(int x, int y, bool state)
        {
            X = x;
            Y = y;
            State = state;
            Neighbors = new List<Cell>();
        }

        public void CalculateNextState()
        {
            var livingNeighbors = Neighbors.Count(c => c.State);
            NextState = State
                ? livingNeighbors >= 2 && livingNeighbors < 4
                : livingNeighbors == 3;
        }

        public void UpdateState()
        {
            State = NextState;
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