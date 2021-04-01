using System;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var challengeType = typeof(_2020.Day6); // Change this to the challenge you wish to run
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => challengeType.IsAssignableFrom(p));

            foreach (var type in types)
            {
                if (type.IsClass)
                {
                    Console.WriteLine($"Running {type.Namespace} {type.Name}");
                    var challenge = Activator.CreateInstance(type);
                    foreach (var result in (challenge as IChallenge).Run())
                    {
                        Console.WriteLine($"{type.Name}: {result}");
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
