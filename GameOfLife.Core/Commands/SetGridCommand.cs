using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;
using System.Text.RegularExpressions;

namespace GameOfLife.Core.Commands
{
    public class SetGridCommand : ICommand
    {
        static readonly Regex DimensionsFormat = new Regex(@"(\d{1,2})\s(\d{1,2})");

        public Result Execute(Settings settings, string commandText)
        {
            var result = new Result();
            try
            {
                var match = DimensionsFormat.Match(commandText);
                var width = int.Parse(match.Groups[1].Value);
                var height = int.Parse(match.Groups[2].Value);

                if (width <= 0 || width > 25 || height <= 0 || height > 25)
                {
                    result.Status = Status.OUT_OF_RANGE;
                    result.MessageText = "Dimensions must be between 1 to 25 inclusive!";
                }
                else
                {
                    settings.Grid.Width = width;
                    settings.Grid.Height = height;
                    result.Status = Status.VALID;
                }

            }

            catch (Exception ex)
            {
                result.Status = Status.INVALID;
                result.MessageText = ex.Message;
            }

            return result;
        }

        public void ExecutePost(Settings settings)
        {
            GridOperations.ResetGrid(settings.Grid);            
        }

        public void ExecutePre(Settings settings)
        {
            Console.WriteLine("Please enter grid size in w h format (example: 10 15):");
        }
    }
}
