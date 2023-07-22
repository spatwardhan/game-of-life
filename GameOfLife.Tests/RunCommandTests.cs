using Xunit;
using GameOfLife.Core;
using GameOfLife.Core.Commands;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Tests
{
    public class RunCommandTests
    {
        private readonly Settings _settings = new Settings { Grid = new Grid(2,2), Generations = 3};
        private readonly RunCommand _command = new RunCommand();

        [Fact]
        public void ShouldMoveToNextGenerationWhenWithinLimit()
        {
            _settings.CurrentGeneration = 0;

            var result = _command.Execute(_settings, ">");
            
            Assert.Equal(Status.CONTINUE, result.Status);
            Assert.Null(result.MessageText);
            Assert.Equal(1, _settings.CurrentGeneration);
        }

        [Fact]
        public void ShouldHandleReturnToMainMenu()
        {
            var result = _command.Execute(_settings, "#");
            
            Assert.Equal(Status.VALID, result.Status);
            Assert.Null(result.MessageText);
        }

        [Fact]
        public void ShouldRejectInvalidInputWhenCurrentGenerationWithinLimit()
        {
            _settings.CurrentGeneration = 2;
            
            var result = _command.Execute(_settings, "something");
            
            Assert.Equal(Status.INVALID, result.Status);
            Assert.Equal("Invalid input entered!", result.MessageText);
        }

        [Fact]
        public void ShouldAcceptAnyInputWhenGenerationLimitReached()
        {
            _settings.CurrentGeneration = 3;
            
            var result = _command.Execute(_settings, "something");
            
            Assert.Equal(Status.VALID, result.Status);
            Assert.Null(result.MessageText);
        }
    }
}
