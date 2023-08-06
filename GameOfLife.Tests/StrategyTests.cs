using GameOfLife.Core.Strategies;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GameOfLife.Tests
{
    public class StrategyTests
    {
        [Theory]
        [InlineData(4)]
        [InlineData(8)]
        public void ShouldHandleOverpopulationForLiveCells(int liveNeighbourCount)
        {
            var isLive = true;
            var strategy = CellUpdateStrategyFactory.GetStrategy(isLive);

            var newState = strategy.UpdateCellState(liveNeighbourCount);

            Assert.IsType<LiveCellUpdateStrategy>(strategy);
            Assert.False(newState);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void ShouldHandleUnderpopulationForLiveCells(int liveNeighbourCount)
        {
            var isLive = true;
            var strategy = CellUpdateStrategyFactory.GetStrategy(isLive);

            var newState = strategy.UpdateCellState(liveNeighbourCount);

            Assert.IsType<LiveCellUpdateStrategy>(strategy);
            Assert.False(newState);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public void ShouldAllowLiveCellsWithTwoOrThreeNeighboursToSurvive(int liveNeighbourCount)
        {
            var isLive = true;
            var strategy = CellUpdateStrategyFactory.GetStrategy(isLive);

            var newState = strategy.UpdateCellState(liveNeighbourCount);

            Assert.IsType<LiveCellUpdateStrategy>(strategy);
            Assert.True(newState);
        }

        [Fact]
        public void ShouldHandleReproductionForDeadCells()
        {
            var isLive = false;
            var strategy = CellUpdateStrategyFactory.GetStrategy(isLive);

            var newState = strategy.UpdateCellState(3);

            Assert.IsType<DeadCellUpdateStrategy>(strategy);
            Assert.True(newState);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(8)]
        public void ShouldLetDeadCellWithoutExactlyThreeLiveNeighboursStayDead(int liveNeighbourCount)
        {
            var isLive = false;
            var strategy = CellUpdateStrategyFactory.GetStrategy(isLive);

            var newState = strategy.UpdateCellState(liveNeighbourCount);

            Assert.IsType<DeadCellUpdateStrategy>(strategy);
            Assert.False(newState);
        }
    }
}
