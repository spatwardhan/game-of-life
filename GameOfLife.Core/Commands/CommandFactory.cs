using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Core.Commands
{
    public static class CommandFactory
    {
        public static ICommand GetCommand(string input)
        {
            ICommand command = default;
            switch (input)
            {
                case "1":
                    command = new SetGridCommand();
                    break;
                case "2":
                    command = new SetGenerationsCommand();
                    break;
                case "3":
                    command = new PopulateGridCommand();
                    break;
                case "4":
                    command = new RunCommand();
                    break;
                default:                    
                    break;
            };

            return command;
        }
    }
}
