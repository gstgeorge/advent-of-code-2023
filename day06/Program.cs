using System.Text.RegularExpressions;

namespace day06;

class Program
{
    static void Main(string[] args)
    {
        string[] input = File.ReadAllLines("input.txt");
        long[][] data = input.Select(x => Regex.Matches(x, @"\d+").Select(y => long.Parse(y.Value)).ToArray()).ToArray();

        List<int> waysToWin = new List<int>();

        string[] partTwoData = new string[2];

        for (long i = 0; i < data[0].Length; i++)
        {
            waysToWin.Add(EvaluateRace(data[0][i], data[1][i]));
            partTwoData[0] += data[0][i].ToString();
            partTwoData[1] += data[1][i].ToString();
        }

        Console.WriteLine($"Part One: {waysToWin.Aggregate((x, y) => x * y)}");
        Console.WriteLine($"Part Two: {EvaluateRace(long.Parse(partTwoData[0]), long.Parse(partTwoData[1]))}");
    }
    static int EvaluateRace(long time, long distance)
    {
        int count = 0;

        for (long i = 0; i < time; i++)
        {
            if (i * (time - i) > distance)
            {
                count++;
            }
        }

        return count;
    }
}
