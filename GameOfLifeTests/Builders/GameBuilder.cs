using System.Collections.Generic;
using GameOfLife;

namespace GameOfLifeTests
{
    internal class GameBuilder
    {
        private int _size;
        private List<(int x, int y)> _initialLivingCells = new List<(int x, int y)>();

        public GameBuilder AGameOfSize(int size)
        {
            _size = size;

            return this;
        }

        public GameBuilder WithLivingCellAt(int x, int y)
        {
            _initialLivingCells.Add((x, y));

            return this;
        }

        public Game Build()
        {
            return new Game(_size, _initialLivingCells);
        }
    }
}