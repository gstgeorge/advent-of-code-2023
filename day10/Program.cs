using System.Text.RegularExpressions;

namespace day10;

class Program
{
    enum Direction { N, E, S, W };

    static void Main(string[] args)
    {
        List<List<char>> pipes = File.ReadAllLines("input.txt").Select(x => x.ToList()).ToList();

        List<(int, int)> pipeSteps = []; // Store steps of pipe traversal

        // Find starting point
        for (int i = 0; i < pipes.Count(); i++)
        {
            if (pipes[i].Contains('S'))
            {
                pipeSteps.Add((i, pipes[i].IndexOf('S')));
                break;
            }
        }

        // Find starting direction
        (int, int) nextStep;

        // North
        if (pipeSteps[0].Item1 > 0 &&
            Regex.IsMatch(pipes[pipeSteps[0].Item1 - 1][pipeSteps[0].Item2].ToString(), @"[\||7|F]"))
        {
            nextStep = (pipeSteps[0].Item1 - 1, pipeSteps[0].Item2);
        }

        // East
        else if (pipeSteps[0].Item2 < pipes[0].Count - 1 &&
                Regex.IsMatch(pipes[pipeSteps[0].Item1][pipeSteps[0].Item2 + 1].ToString(), @"[-|7|J]"))
        {
            nextStep = (pipeSteps[0].Item1, pipeSteps[0].Item2 + 1);
        }      

        // South  
        else if (pipeSteps[0].Item1 < pipes.Count() - 1 &&
                Regex.IsMatch(pipes[pipeSteps[0].Item1][pipeSteps[0].Item2].ToString(), @"[\||L|J]"))
        {
            nextStep = (pipeSteps[0].Item1 + 1, pipeSteps[0].Item2);
        }       

        // West 
        else if (pipeSteps[0].Item2 > 0 &&
                Regex.IsMatch(pipes[pipeSteps[0].Item1][pipeSteps[0].Item2].ToString(), @"[-|L|F]"))
        {
            nextStep = (pipeSteps[0].Item1, pipeSteps[0].Item2 - 1);
        }

        else throw new Exception("Starting direction cannot be determined.");

        // Traverse pipe
        while (nextStep != pipeSteps[0])
        {
            (int, int) prevStep = pipeSteps.Last();
            pipeSteps.Add(nextStep);

            switch (pipes[nextStep.Item1][nextStep.Item2])
            {
                case '|':
                    if (prevStep.Item1 < nextStep.Item1) // Did we come from above?
                    {
                        nextStep = (nextStep.Item1 + 1, nextStep.Item2); // Go down
                    }
                    else nextStep = (nextStep.Item1 - 1, nextStep.Item2); // Go up
                    break;

                case '-':
                    if (prevStep.Item2 < nextStep.Item2) // Did we come from the left?
                    {
                        nextStep = (nextStep.Item1, nextStep.Item2 + 1); // Go right
                    }
                    else nextStep = (nextStep.Item1, nextStep.Item2 - 1); // Go left
                    break;

                case 'L':
                    if (prevStep.Item1 < nextStep.Item1) // Did we come from above?
                    {
                        nextStep = (nextStep.Item1, nextStep.Item2 + 1); // Go right
                    }
                    else nextStep = (nextStep.Item1 - 1, nextStep.Item2); // Go up
                    break;

                case 'J':
                    if (prevStep.Item1 < nextStep.Item1) // Did we come from above?
                    {
                        nextStep = (nextStep.Item1, nextStep.Item2 - 1); // Go left
                    }
                    else nextStep = (nextStep.Item1 - 1, nextStep.Item2); // Go up
                    break;

                case '7':
                    if (prevStep.Item2 < nextStep.Item2) // Did we come from the left?
                    {
                        nextStep = (nextStep.Item1 + 1, nextStep.Item2); // Go down
                    }
                    else nextStep = (nextStep.Item1, nextStep.Item2 - 1); // Go left
                    break;

                case 'F':
                    if (prevStep.Item1 > nextStep.Item1) // Did we come from below?
                    {
                        nextStep = (nextStep.Item1, nextStep.Item2 + 1); // Go right
                    }
                    else nextStep = (nextStep.Item1 + 1, nextStep.Item2); // Go down
                    break;

                default:
                    throw new Exception("Invalid pipe encountered during traversal.");
            }
        }
    
        Console.WriteLine($"Part One: {pipeSteps.Count / 2}");
    }
}
