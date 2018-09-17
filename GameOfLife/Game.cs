using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public class Game
    {
        public int Size { get; }
        public Cell[] Cells { get; }

        private int FlatSize =>
            Size * Size;

        public Game(int size, IEnumerable<(int x, int y)> initialLivingCells)
        {
            if (initialLivingCells == null)
                throw new ArgumentException(nameof(initialLivingCells));

            Size = size;
            Cells = new Cell[FlatSize];
            InitGame(initialLivingCells);
        }

        public Cell CellAt(int x, int y)
        {
            return Cells[x * Size + y];
        }

        public void UpdateState()
        {
            CalculateNextGeneration();

            foreach (var cell in Cells)
            {
                cell.UpdateState();
            }
        }

        private void CalculateNextGeneration()
        {
            foreach (var cell in Cells)
            {
                cell.CalculateNextState();
            }
        }

        private void InitGame(IEnumerable<(int x, int y)> initialLivingCells)
        {
            InitCells(initialLivingCells.ToList());
            CalculateNeighbors();
        }

        private void InitCells(IReadOnlyCollection<(int x, int y)> initialLivingCells)
        {
            for (var i = 0; i < Size; i++)
                for (var j = 0; j < Size; j++)
                {
                    Cells[i * Size + j] = new Cell(i, j, initialLivingCells.Any(c => c.x == i && c.y == j));
                }
        }

        private void CalculateNeighbors()
        {
            foreach (var cell in Cells)
            {
                cell.SetNeighbors(AllNeighborsOf((cell.X, cell.Y)));
            }
        }

        private IEnumerable<Cell> AllNeighborsOf((int x, int y) cellCoordinates)
        {
            return this.CoordinatesOfAllNeighbors(cellCoordinates)
                .Select(neighbor => CellAt(neighbor.x, neighbor.y));
        }
    }
}