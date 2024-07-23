using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Contracts;

namespace backend.ConsoleApps
{
    public class Complimenter : ConsoleApp
    {
        public override string Description => "I will brighten your day :)";

        public override string Name => "Complimenter";

        private List<string> _compliments;
        private int _currentComplimentIndex = 0;

        public Complimenter()
        {
            _compliments = ["You're awesome!",
                            "You rock my world!",
                            "You brighten everyone's day!"];
        }

        public override string ReceiveMessage(string message)
        {
            return _compliments[(_currentComplimentIndex++) % _compliments.Count];
        }
    }
}