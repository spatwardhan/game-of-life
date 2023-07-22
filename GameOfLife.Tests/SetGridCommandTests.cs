using Xunit;
using GameOfLife.Core;
using GameOfLife.Core.Commands;

namespace GameOfLife.Tests
{
    public class SetGridCommandTests
    {
        private readonly Settings _settings = new Settings { Grid = new Grid(25, 25) };
        private readonly SetGridCommand _command = new SetGridCommand();

        [Fact]
        public void ShouldSetGridForValidInput()
        {
            var result = _command.Execute(_settings, "5 5");

            Assert.Equal(Status.VALID, result.Status);
            Assert.Null(result.MessageText);
            Assert.Equal(5, _settings.Grid.Width);
            Assert.Equal(5, _settings.Grid.Height);
        }

        [Fact]
        public void ShouldRejectOutOfRangeInput()
        {
            var result = _command.Execute(_settings, "50 50");

            Assert.Equal(Status.OUT_OF_RANGE, result.Status);
            Assert.Equal("Dimensions must be between 1 to 25 inclusive!", result.MessageText);
            Assert.Equal(25, _settings.Grid.Width);
            Assert.Equal(25, _settings.Grid.Height);
        }

        [Fact]
        public void ShouldRejectInvalidInput()
        {
            var result = _command.Execute(_settings, "bad input");

            Assert.Equal(Status.INVALID, result.Status);
            Assert.Equal("Input string was not in a correct format.", result.MessageText);
            Assert.Equal(25, _settings.Grid.Width);
            Assert.Equal(25, _settings.Grid.Height);
        }

        [Theory]
        [InlineData(Status.INVALID,"bad input", "Input string was not in a correct format.")]
        [InlineData(Status.OUT_OF_RANGE, "50 50", "Dimensions must be between 1 to 25 inclusive!")]
        public void ShouldRetainLastSetValuesOnSubsequentBadInput(Status status, string commandText, string messageText)
        {
            var result1 = _command.Execute(_settings, "10 10");

            Assert.Equal(Status.VALID, result1.Status);
            Assert.Null(result1.MessageText);
            Assert.Equal(10, _settings.Grid.Width);
            Assert.Equal(10, _settings.Grid.Height);

            var result2 = _command.Execute(_settings, commandText);

            Assert.Equal(status, result2.Status);
            Assert.Equal(messageText, result2.MessageText);
            Assert.Equal(10, _settings.Grid.Width);
            Assert.Equal(10, _settings.Grid.Height);
        }
    }
}
