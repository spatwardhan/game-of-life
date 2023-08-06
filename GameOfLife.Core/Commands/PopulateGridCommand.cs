using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;
using System.Text.RegularExpressions;

namespace GameOfLife.Core.Commands
{
    public class PopulateGridCommand : ICommand
    {
        static readonly Regex DimensionsFormat = new Regex(@"(\d{1,2})\s(\d{1,2})");

        public Result Execute(Settings settings, string commandText)
        {
            var result = new Result();

            if (commandText == "~")
            {
                GridOperations.ResetGrid(settings.Grid);
                settings.LiveCells.Clear();
                result.Status = Status.CONTINUE;
                result.MessageText = "Please enter live cell coordinate in x y format, ~ to clear all the previously entered cells or # to go back to main menu:";
            }
            else if (DimensionsFormat.IsMatch(commandText))
            {
                try
                {
                    var match = DimensionsFormat.Match(commandText);
                    var x = int.Parse(match.Groups[1].Value);
                    var y = int.Parse(match.Groups[2].Value);

                    if (x < 0 || x >= settings.Grid.Width || y < 0 || y >= settings.Grid.Height)
                    {
                        result.Status = Status.OUT_OF_RANGE;
                        result.MessageText = "Position must be within the grid dimensions!";
                    }
                    else
                    {
                        settings.LiveCells.Add(new Cell(x, y));
                        result.Status = Status.CONTINUE;
                    }
                }
                catch (Exception ex)
                {
                    result.Status = Status.INVALID;
                    result.MessageText = ex.Message;
                }
            }
            else if (commandText == "#")
            {
                result.Status = Status.VALID;
            }
            else
            {
                result.Status = Status.INVALID;
                result.MessageText = "Please enter live cell coordinate in x y format, ~ to clear all the previously entered cells or # to go back to main menu:";
            }

            return result;
        }

        public void ExecutePost(Settings settings)
        {
            GridOperations.UpdateGrid(settings.Grid, settings.LiveCells);
        }

        public void ExecutePre(Settings settings)
        {
            Console.WriteLine("Please enter live cell coordinate in x y format, ~ to clear all the previously entered cells or # to go back to main menu:");
        }
    }
}
