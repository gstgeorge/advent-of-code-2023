using System.Text.RegularExpressions;

namespace day02;

class Program
{
    static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input.txt");

        Dictionary<string, int> maxValues = new()
        {
            { "red",   12 },
            { "green", 13} ,
            { "blue",  14 }

        };

        Regex p = new Regex(@"(?<count>\d+) (?<color>red|green|blue)");

        int total1 = 0;
        int total2 = 0;

        foreach (string line in lines)
        {
            string[] game = line.Split(':');
            string[] sets = game[1].Split(';');

            bool valid = true;

            Dictionary<string, int> fewestCubes = new()
            {
                { "red",   0 },
                { "green", 0 },
                { "blue",  0 }
            };

            foreach (string set in sets)
            {
                MatchCollection matches = p.Matches(set);

                foreach (Match match in matches)
                {
                    int matchVal = Int32.Parse(match.Groups["count"].Value);
                    string matchColor = match.Groups["color"].Value;

                    if (matchVal > maxValues[matchColor])
                    {
                        valid = false;
                    }

                    if (matchVal > fewestCubes[matchColor])
                    {
                        fewestCubes[matchColor] = matchVal;
                    }
                }
            }

            if (valid) 
            {
                total1 += Int32.Parse(Regex.Match(game[0], @"\d+").Value);
            }

            total2 += fewestCubes["red"] * fewestCubes["green"] * fewestCubes["blue"];
        }

        Console.WriteLine($"Part 1: {total1}");
        Console.WriteLine($"Part 2: {total2}");
    }
}
