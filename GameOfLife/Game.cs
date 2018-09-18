using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class Game
    {
        public Cell[] Cells { get; }
        public int Width { get; set; }
        public int Height { get; set; }

        private int FlatSize =>
            Height * Width;

        private static readonly (int x, int y)[] CoordinateDeltas =
        {
            (-1, -1), (0, -1), (1, -1), (-1, 0), (1, 0), (-1, 1), (0, 1), (1, 1)
        };

        public Game(int height, int width, IEnumerable<(int x, int y)> initialLivingCells)
        {
            if (initialLivingCells == null)
                throw new ArgumentException(nameof(initialLivingCells));

            Height = height;
            Width = width;
            Cells = new Cell[FlatSize];
            InitCells(initialLivingCells.ToList());
            CalculateNeighbors();
        }

        private Game(int height, int width, IEnumerable<Cell> cells)
        {
            Height = height;
            Width = width;
            Cells = cells.ToArray();
            CalculateNeighbors();
        }

        public Cell CellAt(int x, int y)
        {
            return Cells[y * Width + x];
        }

        public Game NextGeneration()
        {
            var nextCells = new Cell[FlatSize];
            for (var y = 0; y < Height; y++)
                for (var x = 0; x < Width; x++)
                    nextCells[y * Width + x] = CellAt(x, y).CalculateNextState();
            
            return new Game(Height, Width, nextCells);
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
            return CoordinatesOfAllNeighbors(cellCoordinates)
                .Select(neighbor => CellAt(neighbor.x, neighbor.y));
        }

        public IEnumerable<(int x, int y)> CoordinatesOfAllNeighbors((int x, int y) coordinate)
        {
            return CoordinateDeltas
                .Select(d => (x: coordinate.x + d.x, y: coordinate.y + d.y))
                .Where(c => c.x >= 0 && c.x < Width && c.y >= 0 && c.y < Height);
        }
    }
}