using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Contracts;

namespace backend.ConsoleApps
{
    public class Echo : ConsoleApp
    {
        public override string Description => "I repeat whatever you say back to you";

        public override string Name => "Echo";

        public override string ReceiveMessage(string message)
        {
            return message;
        }
    }
}