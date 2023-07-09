using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.UI
{
    public class Program
    {
        static readonly string MainPrompt = $"Welcome to Conway's Game of Life{Environment.NewLine}[1] Specify grid size{Environment.NewLine}[2] Specify number of generation{Environment.NewLine}[3] Specify initial live cells{Environment.NewLine}[4] Run{Environment.NewLine}Please enter your selection";

        static void Main(string[] args)
        {            
            Console.WriteLine(MainPrompt);
            var choice = Console.Read();

            switch(choice)
            {
                case '1': SetGrid();
                break;
                case '2': SetGenerations();
                break;
                case '3': InitializeGrid();
                break;
                default: break;
            }
        }

        private static void SetGrid()
        {

        }

        private static void SetGenerations()
        {

        }

        private static void InitializeGrid()
        {

        }
    }
}
