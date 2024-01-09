namespace day11;

class Program
{
    static void Main(string[] args)
    {
        List<List<char>> universeMap = File.ReadAllLines("input.txt").Select(x => x.ToList()).ToList();

        // Mark all empty columns
        for (int i = universeMap[0].Count-1; i >= 0; i--)
        {
            if (universeMap.All(x => x[i] != '#'))
            {
                foreach (var row in universeMap)
                {
                    row[i] = '!';
                }
            }
        }

        // Mark all empty rows
        for (int i = universeMap.Count-1; i >= 0; i--)
        {
            if (universeMap[i].All(x => x != '#'))
            {
                universeMap[i] = Enumerable.Repeat('!', universeMap[i].Count).ToList();
            }
        }

        // Get positions of all galaxies
        List<(int, int)> galaxies = [];

        for (int i = 0; i < universeMap.Count; i++)
        {
            for (int j = 0; j < universeMap[i].Count; j++)
            {
                if (universeMap[i][j] == '#') galaxies.Add((i, j));
            }
        }

        // Find distance between each set of galaxies
        int total1 = 0;
        long total2 = 0;

        for (int i = 0; i < galaxies.Count; i++) // For each galaxy
        {
            for (int j = i + 1; j < galaxies.Count; j++) // For each subsequent galaxy
            {
                // Examine Y distance
                int yStart = int.Min(galaxies[i].Item1, galaxies[j].Item1);
                int yDist = Math.Abs(galaxies[i].Item1 - galaxies[j].Item1); 

                for (int y = yStart+1; y < yStart + yDist; y++) // Add distance due to expansion
                {
                    if (universeMap[y][galaxies[i].Item2] == '!')
                    {
                        total1++;
                        total2 += 999999;
                    }
                }

                total1 += yDist;
                total2 += yDist;

                // Examine X distance
                int xStart = int.Min(galaxies[i].Item2, galaxies[j].Item2);
                int xDist = Math.Abs(galaxies[i].Item2 - galaxies[j].Item2);

                for (int x = xStart+1; x < xStart + xDist; x++) // Add distance due to expansion
                {
                    if (universeMap[galaxies[i].Item1][x] == '!')
                    {
                        total1++;
                        total2 += 999999;
                    }
                }

                total1 += xDist;
                total2 += xDist;
            }
        }

        Console.WriteLine($"Part One: {total1}");
        Console.WriteLine($"Part Two: {total2}");
    }
}
