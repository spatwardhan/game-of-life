using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;

namespace GameOfLife.Core.Commands
{
    public class RunCommand : ICommand
    {
        public Result Execute(Settings settings, string commandText)
        {
            var result = new Result();

            if (commandText == ">")
            {
                result.Status = Status.CONTINUE;
                RuleEngine.MoveToNextGeneration(settings.Grid);
                ++settings.CurrentGeneration;
            }
            else if (commandText == "#")
            {
                result.Status = Status.VALID;
            }
            else
            {
                if (settings.CurrentGeneration >= settings.Generations)
                {
                    result.Status = Status.VALID;
                }
                else
                {
                    result.Status = Status.INVALID;
                    result.MessageText = "Invalid input entered!";
                }
            }

            return result;
        }

        public void ExecutePost(Settings settings)
        {
            settings.CurrentGeneration = 0;
        }

        public void ExecutePre(Settings settings)
        {
            if (settings.CurrentGeneration == 0)
                Console.WriteLine("Initial position");
            else
                Console.WriteLine($"Generation {settings.CurrentGeneration}");

            GridOperations.ShowGrid(settings.Grid);

            if (settings.CurrentGeneration >= settings.Generations)            
                Console.WriteLine("End of generation. Press any key to return to main menu");
            else
                Console.WriteLine("Enter > to go to next generation or # to go back to main menu");
        }
    }
}
