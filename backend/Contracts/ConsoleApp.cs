using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Contracts
{
    public class ConsoleApp
    {
        public virtual string Name => "ConsoleApp";
        public virtual string Description => "I don't do much";
        public virtual string Introduce() => $"Hi! My name is {Name}, and {Description}";
        public virtual string ReceiveMessage(string message) => "I don't know how to respond to anything";
    }
}