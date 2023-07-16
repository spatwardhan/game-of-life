using Xunit;
using GameOfLife.Core;
using GameOfLife.Core.Commands;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Tests
{
    public class RunCommandTests
    {
        [Fact]
        public void ShouldMoveToNextGenerationWhenWithinLimit()
        {
            var settings = new Settings { Grid = new Grid(5, 5), Generations = 3, CurrentGeneration = 0 };
            var command = new RunCommand();
            var result = command.Execute(settings, ">");
            Assert.Equal(Status.CONTINUE, result.Status);
            Assert.Null(result.MessageText);
            Assert.Equal(1, settings.CurrentGeneration);
        }

        [Fact]
        public void ShouldHandleReturnToMainMenu()
        {
            var settings = new Settings { Grid = new Grid(2, 2) };
            var command = new RunCommand();
            var result = command.Execute(settings, "#");
            Assert.Equal(Status.VALID, result.Status);
            Assert.Null(result.MessageText);
        }

        [Fact]
        public void ShouldRejectInvalidInputWhenCurrentGenerationWithinLimit()
        {
            var settings = new Settings { Grid = new Grid(2, 2), Generations = 3, CurrentGeneration = 2 };
            var command = new RunCommand();
            var result = command.Execute(settings, "something");
            Assert.Equal(Status.INVALID, result.Status);
            Assert.Equal("Invalid input entered!", result.MessageText);
        }

        [Fact]
        public void ShouldAcceptAnyInputWhenGenerationLimitReached()
        {
            var settings = new Settings { Grid = new Grid(2, 2), Generations = 3, CurrentGeneration = 3 };
            var command = new RunCommand();
            var result = command.Execute(settings, "something");
            Assert.Equal(Status.VALID, result.Status);
            Assert.Null(result.MessageText);
        }
    }
}
