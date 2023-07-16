using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace GameOfLife.Core.Commands
{
    public interface ICommand
    {
        Result Execute(Settings settings, string commandText);

        void ExecutePre(Settings settings);

        void ExecutePost(Settings settings);
    }
}
