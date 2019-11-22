using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace AdventOfCode
{
    public static class FileHandler
    {
        public static string[] OpenFile(string fileName)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{fileName}.txt");
            return File.ReadAllLines(path);
        }
    }
}
