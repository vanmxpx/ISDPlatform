using System;
using System.Collections.Generic;
using System.Text;

namespace OracleDBUpdater.Commands.SQLCommands
{
    interface ICommand
    {
        string Query { get; }
        void Execute();
    }
}
