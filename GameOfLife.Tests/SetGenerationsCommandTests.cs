using Xunit;
using GameOfLife.Core;
using GameOfLife.Core.Commands;

namespace GameOfLife.Tests
{
    public class SetGenerationsCommandTests
    {
        [Fact]
        public void ShouldSetGenerationsForValidInput()
        {
            var settings = new Settings();
            var command = new SetGenerationsCommand();
            var result = command.Execute(settings, "4");
            Assert.Equal(Status.VALID, result.Status);
            Assert.Null(result.MessageText);
            Assert.Equal(4, settings.Generations);
        }

        [Fact]
        public void ShouldRejectOutOfRangeInput()
        {
            var settings = new Settings();
            var command = new SetGenerationsCommand();
            var result = command.Execute(settings, "40");
            Assert.Equal(Status.OUT_OF_RANGE, result.Status);
            Assert.Equal("Number of generations must be between 3 and 20!", result.MessageText);
            Assert.Equal(0, settings.Generations);
        }

        [Fact]
        public void ShouldRejectInvalidInput()
        {
            var settings = new Settings();
            var command = new SetGenerationsCommand();
            var result = command.Execute(settings, "not a number");
            Assert.Equal(Status.INVALID, result.Status);
            Assert.Equal("Please enter valid input!", result.MessageText);
            Assert.Equal(0, settings.Generations);
        }
    }
}
