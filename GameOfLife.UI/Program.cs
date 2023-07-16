using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GameOfLife.Core;

namespace GameOfLife.UI
{
    public class Program
    {
        static readonly string MainPrompt = $"Welcome to Conway's Game of Life{Environment.NewLine}[1] Specify grid size{Environment.NewLine}[2] Specify number of generation{Environment.NewLine}[3] Specify initial live cells{Environment.NewLine}[4] Run{Environment.NewLine}Please enter your selection";
        static readonly Regex DimensionsFormat = new Regex(@"(\d{1,2})\s(\d{1,2})");
        static Grid Grid;
        static int Generations = default;
        const int MinGenerations = 3;
        const int MaxGenerations = 20;
        static int CurrentGeneration = 0;
        static GridOperations GridOps;

        static void Main(string[] args)
        {
            Grid = new Grid(25, 25); //initialise with largest acceptable dimensions
            GridOps = new GridOperations(Grid);
            ProcessChoice();
        }

        private static void ProcessChoice()
        {
            Console.WriteLine(MainPrompt);
            do
            {
                var choice = Console.ReadLine();
                if (choice == "1")
                {
                    SetGrid();
                    break;
                }
                else if (choice == "2")
                {
                    SetGenerations();
                    break;
                }
                else if (choice == "3")
                {
                    PopulateGrid();
                    break;
                }
                else if (choice == "4")
                {
                    Run();
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option selected, program will exit!");
                    return;
                }
            } while (true);

        }

        private static void SetGrid()
        {
            do
            {
                Console.WriteLine("Please enter grid size in w h format (example: 10 15):");
                var dimensions = Console.ReadLine();
                var isValid = true;
                try
                {
                    var match = DimensionsFormat.Match(dimensions);
                    var width = int.Parse(match.Groups[1].Value);
                    var height = int.Parse(match.Groups[2].Value);

                    if (width <= 0 || width > 25 || height <= 0 || height > 25)
                    {
                        Console.WriteLine("Dimensions must be between 1 to 25 inclusive!");
                        isValid = false;
                    }
                    else
                    {
                        Grid.Width = width;
                        Grid.Height = height;
                        GridOps.InitializeGrid();
                    }

                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    isValid = false;
                }

                if (isValid)
                    break;


            } while (true);

            ProcessChoice();
        }

        private static void SetGenerations()
        {
            do
            {
                Console.WriteLine("Please enter the number of generation (10-20):");
                var gen = Console.ReadLine();
                var isValid = int.TryParse(gen, out int generations);

                if (!isValid)
                {
                    Console.WriteLine("Please enter valid input!");
                }
                else if (isValid && (generations < MinGenerations || generations > MaxGenerations))
                {
                    Console.WriteLine("Number of generations must be between 3 and 20!");
                }
                else
                {
                    Generations = generations;
                    break;
                }

            } while (true);

            ProcessChoice();
        }

        private static void PopulateGrid()
        {
            var liveCells = new List<Cell>();
            Console.WriteLine("Please enter live cell coordinate in x y format, ~ to clear all the previously entered cells or # to go back to main menu:");

            do
            {
                string command = Console.ReadLine();

                if (command == "~")
                {
                    GridOps.ResetGrid();
                    Console.WriteLine("Please enter live cell coordinate in x y format, ~ to clear all the previously entered cells or # to go back to main menu:");
                }
                else if (DimensionsFormat.IsMatch(command))
                {
                    try
                    {
                        var match = DimensionsFormat.Match(command);
                        var x = int.Parse(match.Groups[1].Value);
                        var y = int.Parse(match.Groups[2].Value);

                        if (x < 0 || x >= Grid.Height || y < 0 || y >= Grid.Width)
                        {
                            Console.WriteLine("Position must be within the grid dimensions!");
                        }
                        else
                        {
                            liveCells.Add(new Cell(x, y));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (command == "#")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter live cell coordinate in x y format, ~ to clear all the previously entered cells or # to go back to main menu:");
                    continue;
                }

            } while (true);

            GridOps.UpdateGrid(liveCells);
            ProcessChoice();
        }

        private static void Run()
        {
            do
            {
                if (CurrentGeneration == 0)
                    Console.WriteLine("Initial position");
                else
                    Console.WriteLine($"Generation {CurrentGeneration}");

                GridOps.ShowGrid();

                if (CurrentGeneration >= Generations)
                {
                    Console.WriteLine("End of generation. Press any key to return to main menu");
                    Console.ReadLine();
                    break;
                }
                else
                {
                    Console.WriteLine("Enter > to go to next generation or # to go back to main menu");
                    var command = Console.ReadLine();
                    if (command == ">")
                    {
                        RuleEngine.MoveToNextGeneration(Grid);
                        ++CurrentGeneration;                        
                    }
                    else if(command =="#")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input entered!");
                    }
                }

            } while (true);

            ProcessChoice();

        }
    }
}