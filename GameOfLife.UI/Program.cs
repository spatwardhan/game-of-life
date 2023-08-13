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

        public static void Main(string[] args)
        {
            _settings = new Settings
            {
                Grid = new Grid(25, 25), //initialise with largest acceptable dimensions
                LiveCells = new List<(int,int)>()
            };
            ProcessChoice();
        }

        private static void ProcessChoice()
        {
            bool isCommandKnown = true;
            do
            {
                Console.WriteLine(MainPrompt);
                var choice = Console.ReadLine();
                var command = CommandFactory.GetCommand(choice);

                if (command != null)
                    ProcessChoice(command);
                else
                    isCommandKnown = false;

            } while (isCommandKnown);

            Console.WriteLine("Invalid option selected, program will exit!");
            return;
        }

        private static void ProcessChoice(ICommand command)
        {
            Result result;

            do
            {
                command.ExecutePre(_settings);
                var commandText = Console.ReadLine();

                result = command.Execute(_settings, commandText);
                if (!string.IsNullOrEmpty(result.MessageText))
                    Console.WriteLine(result.MessageText);
            } while (result.Status != Status.VALID);

            command.ExecutePost(_settings);
        }
    }
}