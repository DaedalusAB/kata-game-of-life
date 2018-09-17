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
        public void UpdateALivingCellWith_LessThanTwoLivingNeighbors_Dies()
        {
            var cell = CellBuilder
                .ALivingCell()
                .WithLivingNeighbors(1)
                .Build();

            cell.CalculateNextState();

            Assert.False(cell.NextState);
        }

        [Fact]
        public void UpdateALivingCellWith_TwoNeighbors_Survives()
        {
            var cell = CellBuilder
                .ALivingCell()
                .WithLivingNeighbors(2)
                .Build();

            cell.CalculateNextState();

            Assert.True(cell.NextState);
        }

        [Fact]
        public void UpdateALivingCellWith_ThreeNeighbors_Survives()
        {
            var cell = CellBuilder
                .ALivingCell()
                .WithLivingNeighbors(3)
                .Build();

            cell.CalculateNextState();

            Assert.True(cell.NextState);
        }

        [Fact]
        public void UpdateALivingCellWith_MoreThanThreeNeighbors_Dies()
        {
            var cell = CellBuilder
                .ALivingCell()
                .WithLivingNeighbors(4)
                .Build();

            cell.CalculateNextState();

            Assert.False(cell.NextState);
        }

        [Fact]
        public void UpdateADeadCellWith_LessThanThreeNeighbors_StaysDead()
        {
            var cell = CellBuilder
                .ADeadCell()
                .WithLivingNeighbors(2)
                .Build();

            cell.CalculateNextState();

            Assert.False(cell.NextState);
        }

        [Fact]
        public void UpdateADeadCellWith_ThreeNeighbors_ComesToLife()
        {
            var cell = CellBuilder
                .ADeadCell()
                .WithLivingNeighbors(3)
                .Build();

            cell.CalculateNextState();

            Assert.True(cell.NextState);
        }

        [Fact]
        public void UpdateADeadCellWith_MoreThanThreeNeighbors_StaysDead()
        {
            var cell = CellBuilder
                .ADeadCell()
                .WithLivingNeighbors(4)
                .Build();

            cell.CalculateNextState();

            Assert.False(cell.NextState);
        }

        [Fact]
        public void CellMutatesIntoNextState()
        {
            var cell = CellBuilder
                .ALivingCell()
                .WithLivingNeighbors(4)
                .Build();

            cell.CalculateNextState();
            cell.UpdateState();

            Assert.False(cell.State);
        }
    }
}
