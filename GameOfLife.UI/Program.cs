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
        static readonly string DimensionsFormat = @"(\d{1,2})\s(\d{1,2})";
        static Grid Grid = default;
        static int Generations = default;
        const int MinGenerations = 3;
        const int MaxGenerations = 20;
        static int CurrentGeneration = 0;
        static RuleEngine RuleEngine;

        static void Main(string[] args)
        {            
            ProcessChoice();
        }

        private static void ProcessChoice()
        {
            Console.WriteLine(MainPrompt);
            var choice = Console.ReadLine();
            switch(choice)
            {
                case "1": SetGrid();
                break;
                case "2": SetGenerations();
                break;
                case "3": InitializeGrid();
                break;
                case "4": Run();
                break;
                default: return;
            }
        }

        private static void SetGrid()
        {                
            var regex = new Regex(DimensionsFormat);
            Console.WriteLine("Please enter grid size in w h format (example: 10 15):");
            var dimensions = Console.ReadLine();
            try
            {                
                var match = regex.Match(dimensions);
                var width = int.Parse(match.Groups[1].Value);
                var height = int.Parse(match.Groups[2].Value);
                Grid = new Grid(width,height);
                ProcessChoice();
            }
            catch(ArgumentException aex)
            {
                Console.WriteLine(aex.Message);
                SetGrid();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                SetGrid();
            }
        }

        private static void SetGenerations()
        {
            Console.WriteLine("Please enter the number of generation (10-20):");
            var gen = Console.ReadLine();
            var generations = 0;
            var isValid = int.TryParse(gen, out generations);
            if(!isValid)
            {
                Console.WriteLine("Please enter valid input!");
                SetGenerations();
            }
                
            else if(isValid && (generations < MinGenerations || generations > MaxGenerations))
            {
                Console.WriteLine("Number of generations must be between 3 and 20!");
                SetGenerations();
            }               

            Generations = generations;

            ProcessChoice();
        }

        private static void InitializeGrid()
        {
            var regex = new Regex(DimensionsFormat);
            var liveCells = new List<Cell>();
            Grid.ResetGrid();
            Console.WriteLine("Please enter live cell coordinate in x y format, ~ to clear all the previously entered cells or # to go back to main menu:");

            var command = "";
            while((command = Console.ReadLine()) != "#")
            {
                if(command == "~")
                {
                    InitializeGrid();
                }
                else if(regex.IsMatch(command))
                {
                    try
                    {
                        var match = regex.Match(command);
                        var x = int.Parse(match.Groups[1].Value);
                        var y = int.Parse(match.Groups[2].Value);
                        liveCells.Add(new Cell(x,y));
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        InitializeGrid();
                    }
                }
                else
                {
                    break;
                }
            }
            if(command == "#")
            {
                Grid.UpdateGrid(liveCells);
                ProcessChoice();
            }

        }

        private static void Run()
        {            
            if(CurrentGeneration == 0)
                Console.WriteLine("Initial position");
            else
                Console.WriteLine($"Generation {CurrentGeneration}");
            Grid.ShowGrid();
            if(CurrentGeneration >= Generations)
            {
                Console.WriteLine("End of generation. Press any key to return to main menu");
                Console.ReadLine();
                ProcessChoice();
            }
            else
            {
                Console.WriteLine("Enter > to go to next generation or # to go back to main menu");
                var command = Console.ReadLine();
                if(command == ">")
                {
                    RuleEngine = new RuleEngine(Grid);
                    RuleEngine.MoveToNextGeneration();
                    ++CurrentGeneration;
                    Run();
                }
                else
                {
                    ProcessChoice();
                }
            }
        }
    }
}