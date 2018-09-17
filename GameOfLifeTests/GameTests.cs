using System.Linq;
using Xunit;

namespace GameOfLifeTests
{
    public class GameTests
    {
        private GameBuilder GameBuilder => new GameBuilder();

        [Fact]
        public void CreateAGameAllCellsShouldBeDead()
        {
            var game = GameBuilder
                .AGameOfSize(2)
                .Build();

            Assert.Equal(2, game.Size);
            Assert.Equal(2 * 2, game.Cells.Count());
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

            var cell = game.CellAt(0, 0);

            game.UpdateState();

            Assert.False(cell.State);
        }
    }
}
