using System.Data;

namespace day07;

class Program
{
    class Hand : IComparable<Hand>
    {
        protected static List<char> CardTypes = ['2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A'];
        protected static List<string> HandTypes = ["HC", "1P", "2P", "3K", "FH", "4K", "5K"];

        public Hand(string cards, string bid)
        {
            Bid = int.Parse(bid);
            Cards = cards;
            HandType = DetermineHand(cards);
        }
        
        public int Bid { get; }
        public string Cards { get; }
        public string HandType { get; }

        protected virtual string DetermineHand(string cards)
        {
            Dictionary<char, int> cardCounts = [];

            foreach (char c in cards)
            {
                if (cardCounts.ContainsKey(c)) cardCounts[c]++;
                else cardCounts.Add(c, 1);
            }

            var orderedCardCounts = cardCounts.OrderByDescending(x => x.Value);

            int numDistinct = orderedCardCounts.Count();
            int firstQty = orderedCardCounts.First().Value;

            if (firstQty == 5) return "5K";

            else if (firstQty == 4) return "4K";

            else if (firstQty == 3)
            {
                if (numDistinct == 2) return "FH";
                else return "3K";
            }

            else if (firstQty == 2)
            {
                if (numDistinct == 3) return "2P";
                else return "1P";
            }

            else return "HC";
        }

        protected virtual int CompareCard(char card, char other)
        {
            return CardTypes.IndexOf(card) - CardTypes.IndexOf(other);
        }

        public virtual int CompareTo(Hand? other)
        {
            if (other == null) return 1;
            
            if (HandType != other.HandType)
            {
                return HandTypes.IndexOf(HandType) - HandTypes.IndexOf(other.HandType);
            }

            else 
            {
                for (int i = 0; i < Cards.Length; i++)
                {
                    if (Cards[i] != other.Cards[i])
                    {
                        return CompareCard(Cards[i], other.Cards[i]);
                    }
                }

                return 0;
            }
        }
    }

    class HandPtTwo(string cards, string bid) : Hand(cards, bid)
    {
        private new static List<char> CardTypes = ['J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A'];

        protected override string DetermineHand(string cards)
        {
            Dictionary<char, int> cardCounts = [];

            foreach (char c in cards)
            {
                if (cardCounts.ContainsKey(c)) cardCounts[c]++;
                else cardCounts.Add(c, 1);
            }

            var orderedCardCounts = cardCounts.OrderByDescending(x => x.Value);
            
            int numDistinct = orderedCardCounts.Count();
            int firstQty = orderedCardCounts.First().Value;
            bool hasJ = cardCounts.ContainsKey('J');

            if (firstQty == 5) return "5K";

            else if (firstQty == 4) 
            {
                if (hasJ) return "5K";
                else return "4K";
            }

            else if (firstQty == 3)
            {
                if (hasJ) 
                {
                    if (numDistinct == 2) return "5K";
                    return "4K";
                }

                else if (numDistinct == 2) return "FH";
                else return "3K";
            }

            else if (firstQty == 2)
            {
                if (hasJ)
                {
                    if (numDistinct == 3) 
                    {
                        if (cardCounts['J'] == 2) return "4K";
                        else return "FH";
                    }

                    else return "3K";
                }

                else if (numDistinct == 3) return "2P";
                else return "1P";
            }

            else if (hasJ) return "1P";

            else return "HC";
        }

        protected override int CompareCard(char card, char other)
        {
            return CardTypes.IndexOf(card) - CardTypes.IndexOf(other);
        }
    }

    static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input.txt");

        SortedSet<Hand> hands = [];
        SortedSet<HandPtTwo> handsPtTwo = [];

        foreach (string line in lines)
        {
            string[] split = line.Split(' ');
            hands.Add(new Hand(split[0], split[1]));
            handsPtTwo.Add(new HandPtTwo(split[0], split[1]));
        }

        Console.WriteLine($"Part One: {hands.Select((x, i) => x.Bid * (i + 1)).Sum()}");
        Console.WriteLine($"Part Two: {handsPtTwo.Select((x, i) => x.Bid * (i + 1)).Sum()}");
    }
}
