using System.Collections.Generic;
using GameOfLife;

namespace GameOfLifeTests
{
    internal class GameBuilder
    {
        private int _width = 2;
        private int _height = 2;
        private List<(int x, int y)> _initialLivingCells = new List<(int x, int y)>();

        public GameBuilder AGameOfSize(int size)
        {
            _height = size;
            _width = size;

            return this;
        }

        public GameBuilder WithLivingCellAt(int x, int y)
        {
            _initialLivingCells.Add((x, y));

            return this;
        }

        public GameBuilder OfHeight(int heigth)
        {
            _height = heigth;

            return this;
        }

        public GameBuilder OfWidth(int width)
        {
            _width = width;

            return this;
        }

        public Game Build()
        {
            return new Game(_height, _width, _initialLivingCells);
        }


        
    }
}