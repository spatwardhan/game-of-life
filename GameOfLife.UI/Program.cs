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
        static Grid grid = default;

        static void Main(string[] args)
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
                default: break;
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
                grid = new Grid(width,height);
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

        }

        private static void InitializeGrid()
        {

        }
    }
}