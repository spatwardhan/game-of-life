using System;
using Xunit;
using GameOfLife.Core;
using System.Collections.Generic;

namespace GameOfLife.Tests
{
    public class RuleEngineTests
    {
        [Theory]
        [InlineData(true, 0, Scenario.UNDERPOPULATION)]
        [InlineData(true, 1, Scenario.UNDERPOPULATION)]
        [InlineData(true, 2, Scenario.DEFAULT)]
        [InlineData(true, 3, Scenario.DEFAULT)]
        [InlineData(true, 4, Scenario.OVERPOPULATION)]
        [InlineData(true, 8, Scenario.OVERPOPULATION)]
        [InlineData(false, 3, Scenario.REPRODUCTION)]
        [InlineData(false, 0, Scenario.DEFAULT)]
        [InlineData(false, 2, Scenario.DEFAULT)]
        [InlineData(false, 4, Scenario.DEFAULT)]
        [InlineData(false, 8, Scenario.DEFAULT)]
        public void ShouldReturnCorrectTransitionScenarioForCell(bool isLive, int liveNeighbourCount, Scenario expectedScenario)
        {

            var actual = RuleEngine.GetScenarioForTransition(isLive, liveNeighbourCount);

            Assert.Equal(expectedScenario, actual);
        }       
    }
}
