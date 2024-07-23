using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Contracts;

namespace backend.ConsoleApps
{
    public class RandomNumber : ConsoleApp
    {
        public override string Description => "I will give you a random number between 1 and 10";

        public override string Name => "Random Number";

        public override string ReceiveMessage(string message)
        {
            return Random.Shared.Next(0, 11).ToString();
        }
    }
}