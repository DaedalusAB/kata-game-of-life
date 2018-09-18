using System.Linq;
using Xunit;

namespace GameOfLifeTests
{
    public class GameTests
    {
        private GameBuilder GameBuilder => new GameBuilder();

        [Fact]
        public void GameWithWidthAndHeigthWhichAreDifferent()
        {
            var width = 2;
            var height = 4;
            var game = GameBuilder
                .OfHeight(4)
                .OfWidth(2)
                .Build();

            Assert.Equal(width, game.Width);
            Assert.Equal(height, game.Height);
            Assert.Equal(width * height, game.Cells.Count());
            Assert.All(game.Cells, cell => Assert.False(cell.State));
        }

        [Fact]
        public void GameInitializesCellsWithNeighbors()
        {
            var game = GameBuilder
                .AGameOfSize(2)
                .WithLivingCellAt(0, 0)
                .Build();

            var aCell = game.CellAt(0, 0);

            Assert.Equal(3, aCell.Neighbors.Count());
            Assert.Contains(aCell.Neighbors, cell => cell == game.CellAt(0, 1));
            Assert.Contains(aCell.Neighbors, cell => cell == game.CellAt(1, 1));
            Assert.Contains(aCell.Neighbors, cell => cell == game.CellAt(1, 0));
        }

        [Fact]
        public void GameWithOneLivingCellUpdates_TheCellDies()
        {
            var game = GameBuilder
                .AGameOfSize(2)
                .WithLivingCellAt(0, 0)
                .Build();


            var gameAfter = game.UpdateState();
            var cellAfter = gameAfter.CellAt(0, 0);

            Assert.False(cellAfter.State);
        }

        
    }
}
