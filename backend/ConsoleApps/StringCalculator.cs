using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Contracts;

namespace backend.ConsoleApps
{
    public class StringCalculator : ConsoleApp
    {
        public override string Name => "String Calculator";

        public override string Description => "I'll sum any list of comma separated integers you give me!\nFor example '3,2,5'";

        public override string ReceiveMessage(string message)
        {
            try
            {
                return Add(message);
            }
            catch (Exception exception)
            {
                return $"Sorry, I didn't quite understand that, it sounded like {exception.Message}";
            }
        }
        public string Add(string str)
        {
            if (str == string.Empty) return "0";
            ValidateCommas(str);

            string[] parts = ApplyDelimiters(str);

            List<int> numbers = ThrowIfNonNumber(parts);
            ThrowIfNegativeNumbers(numbers);

            return SumValidNumbers(numbers);
        }

        private List<int> ThrowIfNonNumber(string[] parts)
        {
            var numbers = new List<int>();
            foreach (var part in parts)
            {
                if (!int.TryParse(part, out int outInt) || part.Contains(" "))
                {
                    throw new Exception("Invalid input.");
                }
                numbers.Add(outInt);
            }

            return numbers;
        }

        private string[] ApplyDelimiters(string str)
        {
            string[] separators = ["\n", ","];
            var parts = str.Split(separators, StringSplitOptions.None);
            parts = parts.Where(part => !string.IsNullOrEmpty(part)).ToArray();
            return parts;
        }

        private string SumValidNumbers(List<int> numbers)
        {
            var positiveNumbers = numbers.Where(number => number >= 0)
                                         .Select(number => number < 1000 ? number : 0);

            return positiveNumbers.Sum().ToString();
        }

        private void ThrowIfNegativeNumbers(List<int> numbers)
        {
            var negativeNumbers = numbers.Where(number => number < 0);

            if (negativeNumbers.Any())
            {
                throw new Exception($"Negative numbers are not allowed: [{string.Join(",", negativeNumbers.ToArray())}].");
            }
        }

        private void ValidateCommas(string str)
        {
            string stringWithoutNewLines = str.Replace("\n", "");
            if (stringWithoutNewLines.StartsWith(',') ||
                stringWithoutNewLines.EndsWith(',') ||
                stringWithoutNewLines.Contains(",,"))
            {
                throw new Exception("Invalid input.");
            }
        }

        public string Add()
        {
            throw new Exception("No input.");
        }
    }
}


