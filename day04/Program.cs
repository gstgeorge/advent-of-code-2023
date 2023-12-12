using System.Text.RegularExpressions;

namespace day04;

class Program
{
    private readonly static string[] lines = File.ReadAllLines("input.txt");

    static void Main(string[] args)
    {
        int total1 = 0;
        int total2 = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            total1 += Convert.ToInt32(Math.Pow(2, EvalGame(lines[i]) - 1));
            total2 += RecEvalGames(i);
        }

        Console.WriteLine($"Part One: {total1}");
        Console.WriteLine($"Part Two: {total2}");
    }

    public static int EvalGame(string line)
    {
        int numMatches = 0;

        string[] splitLine = line.Split(':');
        string[] game = splitLine[1].Split('|');

        var winNumbers = Regex.Matches(game[0], @"(\d+)").Select(m => m.Value).ToList();
        var gameNumbers = Regex.Matches(game[1], @"(\d+)").Select(m => m.Value).ToList();

        foreach (string winNumber in winNumbers)
        {
            if (gameNumbers.Contains(winNumber))
            {
                numMatches++;
            }
        }

        return numMatches;
    }

    public static int RecEvalGames(int idx)
    {
        int cards = 0;

        for (int i = 1; i <= EvalGame(lines[idx]); i++)
        {
            cards += RecEvalGames(idx + i);
        }

        return cards + 1;
    }
}
