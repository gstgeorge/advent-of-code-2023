namespace day05;

class Program
{
    private static List<List<Tuple<long, long, long>>> maps = new List<List<Tuple<long, long, long>>>();
    
    static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input.txt");

        long[] seeds = lines[0].Split(":")[1].Trim().Split(" ").Select(x => long.Parse(x)).ToArray();

        #region PARSE DATA

        var map = new List<Tuple<long, long, long>>();

        for (int i = 1; i < lines.Length; i++)
        {
            if (lines[i] == String.Empty)
            {
                continue;
            }

            else if (lines[i].Contains(':'))
            {
                if (map.Count > 0)
                {
                    maps.Add(map);
                    map = new List<Tuple<long, long, long>>();
                }

                continue;
            }

            else
            {
                string[] split = lines[i].Split(' ');
                map.Add(new Tuple<long, long, long>(long.Parse(split[0]), long.Parse(split[1]), long.Parse(split[2])));
            }
        }

        maps.Add(map);

        #endregion

        long? total1 = null;
        // long? total2 = null;
        long seedVal;

        // PART ONE
        foreach (long seed in seeds)
        {
            seedVal = EvaluateSeed(seed);
            total1 = (total1 == null || seedVal < total1) ? seedVal : total1;
        }

        // // PART TWO
        // for (int i = 0; i <= seeds.Length - 2; i += 2)
        // {
        //     for (long j = 0; j < seeds[i+1]; j++)
        //     {
        //         seedVal = EvaluateSeed(seeds[i] + j);
        //         total2 = (total2 == null || seedVal < total2) ? seedVal : total2;
        //     }
        // }

        Console.WriteLine($"Part One: {total1}");
        // Console.WriteLine($"Part Two: {total2}");
    }

    static long EvaluateSeed(long seed)
    {
        long seedVal = seed;

        foreach (var m in maps)
        {
            var query = from val in m
                        where seedVal >= val.Item2
                        where seedVal <= val.Item2 + (val.Item3 - 1)
                        select val.Item1 + (seedVal - val.Item2);

            seedVal = query.FirstOrDefault(defaultValue: seedVal);
        }
        
        return seedVal;
    }
}
