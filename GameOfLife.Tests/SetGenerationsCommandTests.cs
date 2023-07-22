using Xunit;
using GameOfLife.Core;
using GameOfLife.Core.Commands;

namespace GameOfLife.Tests
{
    public class SetGenerationsCommandTests
    {
        private readonly Settings _settings = new Settings();
        private readonly SetGenerationsCommand _command = new SetGenerationsCommand();

        [Fact]
        public void ShouldSetGenerationsForValidInput()
        {
            var result = _command.Execute(_settings, "4");
            
            Assert.Equal(Status.VALID, result.Status);
            Assert.Null(result.MessageText);
            Assert.Equal(4, _settings.Generations);
        }

        [Fact]
        public void ShouldRejectOutOfRangeInput()
        {
            var result = _command.Execute(_settings, "40");
            
            Assert.Equal(Status.OUT_OF_RANGE, result.Status);
            Assert.Equal("Number of generations must be between 3 and 20!", result.MessageText);
            Assert.Equal(0, _settings.Generations);
        }

        [Fact]
        public void ShouldRejectInvalidInput()
        {
            var result = _command.Execute(_settings, "not a number");
            
            Assert.Equal(Status.INVALID, result.Status);
            Assert.Equal("Please enter valid input!", result.MessageText);
            Assert.Equal(0, _settings.Generations);
        }
    }
}
