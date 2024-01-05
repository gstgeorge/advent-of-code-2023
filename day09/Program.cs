namespace day09;

class Program
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");

        int total1 = 0;
        int total2 = 0;

        foreach (string line in lines)
        {
            List<List<int>> sequences = [line.Split(' ').Select(x => int.Parse(x)).ToList()];

            while (sequences.Last().Any(x => x != 0))
            {
                List<int> lastSequence = sequences.Last();
                List<int> newSequence = [];

                for (int i = 0; i < lastSequence.Count - 1; i++)
                {
                    newSequence.Add(lastSequence[i+1] - lastSequence[i]);
                }

                sequences.Add(newSequence);
            }

            sequences.Last().Add(0);

            for (int i = sequences.Count - 2; i >= 0; i--)
            {
                sequences[i].Add(sequences[i].Last() + sequences[i+1].Last());
                sequences[i].Insert(0, sequences[i].First() - sequences[i+1].First());
            }

            total1 += sequences.First().Last();
            total2 += sequences.First().First();
        }

        Console.WriteLine($"Part One: {total1}");
        Console.WriteLine($"Part Two: {total2}");
    }
}
