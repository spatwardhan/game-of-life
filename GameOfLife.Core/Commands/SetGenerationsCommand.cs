using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Core.Commands
{
    public class SetGenerationsCommand : ICommand
    {
        public Result Execute(Settings settings, string commandText)
        {
            var isValid = int.TryParse(commandText, out int generations);
            var result = new Result();

            if (!isValid)
            {
                result.Status = Status.INVALID;
                result.MessageText = "Please enter valid input!";
            }
            else if (isValid && (generations < settings.MinGenerations || generations > settings.MaxGenerations))
            {
                result.Status = Status.OUT_OF_RANGE;
                result.MessageText = "Number of generations must be between 3 and 20!";
            }
            else
            {
                settings.Generations = generations;
                result.Status = Status.VALID;
            }
            return result;
        }

        public void ExecutePost(Settings settings)
        {
            
        }

        public void ExecutePre(Settings settings)
        {
            Console.WriteLine("Please enter the number of generation (10-20):");
        }
    }
}
