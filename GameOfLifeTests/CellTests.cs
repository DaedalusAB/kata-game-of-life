using System.Linq;
using Xunit;

namespace GameOfLifeTests
{
    public class CellTests
    {
        private CellBuilder CellBuilder => new CellBuilder();

        [Fact]
        public void CreateCellWithTwoNeighbors()
        {
            var cell = CellBuilder
                .ALivingCell()
                .WithLivingNeighbors(2)
                .Build();

            Assert.True(cell.State);
            Assert.Equal(2, cell.Neighbors.Count(c => c.State));
        }

        [Fact]
        public void ALivingCellWith_LessThanTwoLivingNeighbors_Dies()
        {
            var cell = CellBuilder
                .ALivingCell()
                .WithLivingNeighbors(1)
                .Build();

            var cellAfter = cell.CalculateNextState();

            Assert.False(cellAfter.State);
        }

        [Fact]
        public void ALivingCellWith_TwoNeighbors_Survives()
        {
            var cell = CellBuilder
                .ALivingCell()
                .WithLivingNeighbors(2)
                .Build();

            var cellAfter = cell.CalculateNextState();

            Assert.True(cellAfter.State);
        }

        [Fact]
        public void ALivingCellWith_ThreeNeighbors_Survives()
        {
            var cell = CellBuilder
                .ALivingCell()
                .WithLivingNeighbors(3)
                .Build();

            var cellAfter = cell.CalculateNextState();

            Assert.True(cellAfter.State);
        }

        [Fact]
        public void ALivingCellWith_MoreThanThreeNeighbors_Dies()
        {
            var cell = CellBuilder
                .ALivingCell()
                .WithLivingNeighbors(4)
                .Build();

            var cellAfter = cell.CalculateNextState();

            Assert.False(cellAfter.State);
        }

        [Fact]
        public void ADeadCellWith_LessThanThreeNeighbors_StaysDead()
        {
            var cell = CellBuilder
                .ADeadCell()
                .WithLivingNeighbors(2)
                .Build();

            var cellAfter = cell.CalculateNextState();

            Assert.False(cellAfter.State);
        }

        [Fact]
        public void ADeadCellWith_ThreeNeighbors_ComesToLife()
        {
            var cell = CellBuilder
                .ADeadCell()
                .WithLivingNeighbors(3)
                .Build();

            var cellAfter = cell.CalculateNextState();

            Assert.True(cellAfter.State);
        }

        [Fact]
        public void ADeadCellWith_MoreThanThreeNeighbors_StaysDead()
        {
            var cell = CellBuilder
                .ADeadCell()
                .WithLivingNeighbors(4)
                .Build();

            var cellAfter = cell.CalculateNextState();

            Assert.False(cellAfter.State);
        }
    }
}
