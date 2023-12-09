using System.Text;
using System.Text.RegularExpressions;

namespace day01;

class Program
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("Resources/input.txt");
        int total = 0;
        
        // PART ONE
        // foreach (string line in lines)
        // {
        //     var result = Regex.Matches(line, @"\d");
        //     total += Convert.ToInt32(result.First().Value + result.Last().Value);
        // }

        // PART TWO

        string[] numbers = new string[]
        {
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine"
        };

        StringBuilder pat = new StringBuilder(@"(?=(\d|");
        pat.AppendJoin("|", numbers);
        pat.Append("))");

        foreach (string line in lines)
        {
            var result = Regex.Matches(line, pat.ToString());

            int first, last;

            if (!Int32.TryParse(result.First().Groups[1].ToString(), out first))
            {
                first = Array.IndexOf(numbers, result.First().Groups[1].ToString()) + 1;
            }

            if (!Int32.TryParse(result.Last().Groups[1].ToString(), out last))
            {
                last = Array.IndexOf(numbers, result.Last().Groups[1].ToString()) + 1;
            }

            int sum = Convert.ToInt32(new string($"{first}{last}"));
            total += sum;
        }
        
        Console.WriteLine(total);
    }
}
