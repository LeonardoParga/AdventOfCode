using System;
using System.Linq;
using AdventOfCode._2015;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var challengeType = typeof(IChallenge);
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => challengeType.IsAssignableFrom(p));

            foreach (var type in types)
            {
                if (type.IsClass)
                {
                    Console.WriteLine($"Running {type.Namespace} {type.Name}");
                    var challenge = Activator.CreateInstance(type);
                    foreach (var result in (challenge as IChallenge).Run())
                    {
                        Console.WriteLine($"{type.Name} - {result}");
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
