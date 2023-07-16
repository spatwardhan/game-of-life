using System;
using System.Collections.Generic;
using GameOfLife.Core;
using GameOfLife.Core.Commands;

namespace GameOfLife.UI
{
    public class Program
    {
        static readonly string MainPrompt = $"Welcome to Conway's Game of Life{Environment.NewLine}[1] Specify grid size{Environment.NewLine}[2] Specify number of generation{Environment.NewLine}[3] Specify initial live cells{Environment.NewLine}[4] Run{Environment.NewLine}Please enter your selection";
        static Settings _settings;

        static void Main(string[] args)
        {
            _settings = new Settings
            {
                Grid = new Grid(25, 25), //initialise with largest acceptable dimensions
                LiveCells = new List<Cell>()
            }; 
            ProcessChoice();
        }

        private static void ProcessChoice()
        {
            Console.WriteLine(MainPrompt);
            var choice = Console.ReadLine();
            var command = CommandFactory.GetCommand(choice);
            if (command == null)
            {
                Console.WriteLine("Invalid option selected, program will exit!");
                return;
            }
            else
            {
                ProcessChoice(command);
            }
        }

        private static void ProcessChoice(ICommand command)
        {
            while (true)
            {
                command.ExecutePre(_settings);
                var commandText = Console.ReadLine();

                var result = command.Execute(_settings, commandText);
                if (!string.IsNullOrEmpty(result.MessageText))
                    Console.WriteLine(result.MessageText);
                if (result.Status == Status.VALID)
                    break;
            }

            command.ExecutePost(_settings);

            ProcessChoice();
        }
    }
}