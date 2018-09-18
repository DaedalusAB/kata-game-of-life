using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class Game
    {
        public Cell[] Cells { get; }

        private int FlatSize =>
            Height * Width;

        public int Width { get; set; }
        public int Height { get; set; }

        public Game(int height, int width, IEnumerable<(int x, int y)> initialLivingCells)
        {
            if (initialLivingCells == null)
                throw new ArgumentException(nameof(initialLivingCells));

            Width = width;
            Height = height;
            Cells = new Cell[FlatSize];
            InitGame(initialLivingCells);
        }

        public Cell CellAt(int x, int y)
        {
            return Cells[y * Width + x];
        }

        public void UpdateState()
        {
            CalculateNextGeneration();

            foreach (var cell in Cells)
            {
                cell.UpdateState();
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    sb.Append(CellAt(x, y) + " ");
                }

                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
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
            for (var y = 0; y < Height; y++)
                for (var x = 0; x < Width; x++)
                {
                    Cells[y * Width + x] = new Cell(x, y, initialLivingCells.Any(c => c.x == y && c.y == x));
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