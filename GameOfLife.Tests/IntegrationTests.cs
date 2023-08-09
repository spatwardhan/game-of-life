using GameOfLife.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Xunit;

namespace GameOfLife.Tests
{
    public class IntegrationTests
    {
        static readonly string MainPrompt = $"Welcome to Conway's Game of Life{Environment.NewLine}[1] Specify grid size{Environment.NewLine}[2] Specify number of generation{Environment.NewLine}[3] Specify initial live cells{Environment.NewLine}[4] Run{Environment.NewLine}Please enter your selection";
        static readonly string GridPrompt = "Please enter grid size in w h format (example: 10 15):";
        static readonly string GenerationsPrompt = "Please enter the number of generation (10-20):";
        static readonly string PopulatePrompt = "Please enter live cell coordinate in x y format, ~ to clear all the previously entered cells or # to go back to main menu:";
        static readonly string RunPrompt = "Enter > to go to next generation or # to go back to main menu";
        static readonly string ExitPrompt = $"Invalid option selected, program will exit!{Environment.NewLine}";

        [Fact]
        public void ShouldHandleValidInputForSetGrid()
        {
            var inputs = new string[] { "1", "5 5", "#" };
            var outputs = new string[] { MainPrompt, GridPrompt, MainPrompt, ExitPrompt };

            RunTest(inputs, outputs);
        }

        [Fact]
        public void ShouldHandleValidInputForSetGenerations()
        {
            var inputs = new string[] { "2", "3", "#" };
            var outputs = new string[] { MainPrompt, GenerationsPrompt, MainPrompt, ExitPrompt };

            RunTest(inputs, outputs);
        }

        [Fact]
        public void ShouldHandleValidInputForPopulateGrid()
        {
            var inputs = new string[] { "3", "2 1", "2 2", "#", "#" };
            var outputs = new string[] { MainPrompt, PopulatePrompt, PopulatePrompt, PopulatePrompt, MainPrompt, ExitPrompt };

            RunTest(inputs, outputs);
        }

        [Fact]
        public void ShouldRunAllStepsForValidInput()
        {
            var gen0 = string.Join(Environment.NewLine, "Initial position", ". o o ", ". o . ", ". . o ", RunPrompt);
            var gen1 = string.Join(Environment.NewLine, "Generation 1", ". o o ", ". o . ", ". . . ", RunPrompt);
            var gen2 = string.Join(Environment.NewLine, "Generation 2", ". o o ", ". o o ", ". . . ", RunPrompt);
            var gen3 = string.Join(Environment.NewLine, "Generation 3", ". o o ", ". o o ", ". . . ", "End of generation. Press any key to return to main menu");


            var inputs = new string[] { "1", "3 3", "2", "3", "3", "1 0", "2 0", "1 1", "2 2", "#", "4", ">", ">", ">", "#" };
            var outputs = new string[] { MainPrompt, GridPrompt, MainPrompt, GenerationsPrompt,
                MainPrompt, PopulatePrompt, PopulatePrompt,PopulatePrompt,PopulatePrompt,PopulatePrompt,
                MainPrompt, gen0, gen1, gen2, gen3, MainPrompt, ExitPrompt };

            RunTest(inputs, outputs);

        }

        private static void RunTest(string[] inputs, string[] outputs)
        {
            using StringWriter sw = new StringWriter();
            using StringReader sr = new StringReader(string.Join(Environment.NewLine, inputs));
            var expected = string.Join(Environment.NewLine, outputs);

            Console.SetOut(sw);
            Console.SetIn(sr);
            Program.Main(new string[] { });

            Assert.Equal(expected, sw.ToString());
        }
    }
}
