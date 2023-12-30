using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace day08;

class Program
{
    static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input.txt");

        string direction = lines[0];

        Dictionary<string, Tuple<string, string>> maps = [];

        foreach (string s in lines[2..lines.Length])
        {
            var groups = Regex.Match(s, @"(?<node>\w+) = \((?<L>\w+), (?<R>\w+)\)").Groups;
            maps.Add(groups["node"].Value, new Tuple<string, string>(groups["L"].Value, groups["R"].Value));
        }

        // Part One
        int steps1 = 0;
        string node = "AAA";

        while (node != "ZZZ")
        {
            node = direction[steps1 % direction.Length] switch
            {
                'L' => maps[node].Item1,
                'R' => maps[node].Item2,
                _ => throw new ArgumentException("Invalied instruction")
            };

            steps1++;
        }

        Console.WriteLine($"Part One: {steps1}");

        // Part Two
        List<int> stepsPartTwo = [];

        foreach (string s in maps.Keys.Where(x => x.Last() == 'A'))
        {
            int stepsTemp = 0;
            string nodeTemp = s;

            while (nodeTemp.Last() != 'Z')
            {
                nodeTemp = direction[stepsTemp % direction.Length] switch
                {
                    'L' => maps[nodeTemp].Item1,
                    'R' => maps[nodeTemp].Item2,
                    _ => throw new ArgumentException("Invalied instruction")
                };

                stepsTemp++;
            }

            stepsPartTwo.Add(stepsTemp);
        }

        long steps2 = 1;

        foreach (int i in stepsPartTwo)
        {
            steps2 = steps2 * i / gcd(steps2, i);
        }

        Console.WriteLine($"Part Two: {steps2}");
    }

    public static long gcd(long a, long b)
    {
        if (a == 0)
            return b;
        return gcd(b % a, a);
    }
}
