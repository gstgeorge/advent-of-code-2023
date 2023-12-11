using System.Text;
using System.Text.RegularExpressions;

namespace day03;

class Program
{
    static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input.txt");

        int total1 = 0;
        int total2 = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            bool checkAbove = i > 0;
            bool checkBelow = i < lines.Length - 1;

            #region PART ONE

            var matches = Regex.Matches(lines[i], @"(\d+)");

            foreach (Match match in matches) // for all numerical matches in line
            {
                StringBuilder searchString = new StringBuilder();

                // capture values to the left of the match
                int idx = match.Index - 1;
                if (idx >= 0)
                {
                    if (checkAbove)
                    {
                        searchString.Append(lines[i-1][idx]);
                    }

                    searchString.Append(lines[i][idx]);

                    if (checkBelow)
                    {
                        searchString.Append(lines[i+1][idx]);
                    }
                }

                // capture values in line with the match
                idx++;

                if (checkAbove)
                {
                    searchString.Append(lines[i-1].Substring(idx, match.Length));
                }

                if (checkBelow)
                {
                    searchString.Append(lines[i+1].Substring(idx, match.Length));
                }

                // capture values to the right of the match
                idx += match.Length;

                if (idx < lines[i].Length - 1)
                {
                    if (checkAbove)
                    {
                        searchString.Append(lines[i-1][idx]);
                    }

                    searchString.Append(lines[i][idx]);

                    if (checkBelow)
                    {
                        searchString.Append(lines[i+1][idx]);
                    }
                }

                // check search string for symbols
                if (Regex.IsMatch(searchString.ToString(), @"[^a-zA-Z\d\s.]"))
                {
                    total1 += Int32.Parse(match.Value);
                }
            }

            #endregion

            #region PART TWO

            MatchCollection gears = Regex.Matches(lines[i], @"\*");

            foreach (Match gear in gears)
            {
                int idx = gear.Index;
                List<int> nums = new List<int>();

                if (checkAbove) // check NW, N, and NE
                {
                    if (Char.IsDigit(lines[i-1][idx])) // check N
                    {
                        string strN = lines[i-1][idx].ToString();
                        strN = PartTwoSearchLeft(lines[i-1], idx-1, strN);
                        strN = PartTwoSearchRight(lines[i-1], idx+1, strN);
                        nums.Add(Int32.Parse(strN));
                    }

                    else // check NW and NE
                    {
                        string strNW = PartTwoSearchLeft(lines[i-1], idx-1);
                        if (!string.IsNullOrEmpty(strNW)) 
                        {
                            nums.Add(Int32.Parse(strNW));
                        }

                        string strNE = PartTwoSearchRight(lines[i-1], idx+1);
                        if (!string.IsNullOrEmpty(strNE)) 
                        {
                            nums.Add(Int32.Parse(strNE));
                        }
                    }
                }

                string strW = PartTwoSearchLeft(lines[i], idx-1);
                if (!string.IsNullOrEmpty(strW)) 
                {
                    nums.Add(Int32.Parse(strW));
                }

                string strE = PartTwoSearchRight(lines[i], idx+1);
                if (!string.IsNullOrEmpty(strE)) 
                {
                    nums.Add(Int32.Parse(strE));
                }

                if (checkBelow) // check SW, S, and SE
                {
                    if (Char.IsDigit(lines[i+1][idx])) // check S
                    {
                        string strS = lines[i+1][idx].ToString();
                        strS = PartTwoSearchLeft(lines[i+1], idx-1, strS);
                        strS = PartTwoSearchRight(lines[i+1], idx+1, strS);
                        nums.Add(Int32.Parse(strS));
                    }

                    else // check SW and SE
                    {
                        string strSW = PartTwoSearchLeft(lines[i+1], idx-1);
                        if (!string.IsNullOrEmpty(strSW)) 
                        {
                            nums.Add(Int32.Parse(strSW));
                        }

                        string strSE = PartTwoSearchRight(lines[i+1], idx+1);
                        if (!string.IsNullOrEmpty(strSE)) 
                        {
                            nums.Add(Int32.Parse(strSE));
                        }
                    }
                }                
            
                if (nums.Count == 2)
                {
                    total2 += nums[0] * nums[1];
                }
            }

            #endregion
        }

        Console.WriteLine($"Part One: {total1}");
        Console.WriteLine($"Part Two: {total2}");
    }

    public static string PartTwoSearchRight(string line, int idx, string? existingVal=null)
    {
        StringBuilder sb = existingVal == null ? new StringBuilder() : new StringBuilder(existingVal.ToString());

        while(idx >= 0 && idx < line.Length && Char.IsDigit(line[idx]))
        {
            sb.Append(line[idx++]);
        }

        return sb.ToString();
    }

    public static string PartTwoSearchLeft(string line, int idx, string? existingVal=null)
    {
        StringBuilder sb = existingVal == null ? new StringBuilder() : new StringBuilder(existingVal.ToString());

        while(idx >= 0 && idx < line.Length && Char.IsDigit(line[idx]))
        {
            sb.Insert(0, line[idx--]);
        }

        return sb.ToString();
    }
}
