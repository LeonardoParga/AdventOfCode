using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public class Day7 : Day, IChallenge
    {
        public IEnumerable<string> Run()
        {
            var targetType = "shiny gold";
            var bags = new List<Bag>();

            foreach (var inputLine in Input)
                CreateBag(bags, inputLine);

            yield return ResultString(Step1(bags, targetType));
            yield return ResultString(Step2(bags, targetType));
        }

        private int Step1(List<Bag> bags, string targetType)
        {
            var count = 0;

            foreach (var bag in bags)
                count += bag.ContainsTargetType(targetType) ? 1 : 0;

            return count;
        }

        private int Step2(List<Bag> bags, string targetType)
        {
            var bag = bags.First(x => x.Type == targetType);
            return bag.DeepCount() - 1; // removing one since DeepCount has to count the bag itself, and in this case I don't need the target bag to be counted
        }

        private void CreateBag(List<Bag> bags, string input)
        {
            var bagType = Regex.Match(input, @"^[\w\s]+?(?=\sbags)").Value;

            var bag = bags.FirstOrDefault(x => x.Type == bagType);

            if (bag == null)
            {
                bag = new Bag(bagType);
                bags.Add(bag);
            }

            var innerBagMatches = Regex.Matches(input, @"(\d{1,})\s([\w\s]+)?(?=\sbags?)");

            foreach (Match innerBagMatch in innerBagMatches)
                bag.InnerBags.Add(CreateInnerBag(bags, int.Parse(innerBagMatch.Groups[1].Value), innerBagMatch.Groups[2].Value));
        }

        private InnerBag CreateInnerBag(List<Bag> bags, int innerBagCount, string innerBagType)
        {
            var innerBagBag = bags.FirstOrDefault(x => x.Type == innerBagType);
            if (innerBagBag == null)
            {
                var newBag = new Bag(innerBagType);
                bags.Add(newBag);
                innerBagBag = newBag;
            }

            return new InnerBag(innerBagBag, innerBagCount);
        }

        private class Bag
        {
            public Bag(string type)
            {
                Type = type;
                InnerBags = new List<InnerBag>();
            }

            public string Type { get; set; }
            public List<InnerBag> InnerBags { get; set; }

            public bool ContainsTargetType(string targetType)
            {
                if (InnerBags.Any(x => x.Bag.Type == targetType))
                    return true;
                else
                    foreach (var innerBag in InnerBags)
                        if (innerBag.Bag.ContainsTargetType(targetType))
                            return true;

                return false;
            }

            public int DeepCount()
            {
                return 1 + InnerBags.Select(x => x.DeepCount()).Sum(); // adding 1 for the bag itself
            }
        }

        private class InnerBag
        {
            public InnerBag(Bag bag, int count)
            {
                Bag = bag;
                Count = count;
            }

            public Bag Bag { get; set; }
            private int Count { get; set; }

            public int DeepCount()
            {
                return Count * Bag.DeepCount();
            }
        }
    }
}
