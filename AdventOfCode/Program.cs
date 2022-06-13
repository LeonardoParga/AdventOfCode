using System;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var challengeType = typeof(_2021.Day8); // Change this to the challenge you wish to run
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => challengeType.IsAssignableFrom(x));

            foreach (var type in types)
            {
                Console.WriteLine($"Running {type.Namespace} {type.Name}");
                var challenge = Activator.CreateInstance(type);
                foreach (var result in (challenge as IChallenge).Run())
                    Console.WriteLine($"{type.Name}: {result}");
            }

            Console.ReadLine();
        }
    }
}
