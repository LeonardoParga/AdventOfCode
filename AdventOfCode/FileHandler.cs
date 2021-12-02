using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace AdventOfCode
{
    public static class FileHandler
    {
        public static string[] ReadFile(string fileName, int year)
        {
            string path = GetFullPath(fileName, year);
            return File.ReadAllLines(path);
        }

        public static string ReadFileText(string fileName, int year)
        {
            string path = GetFullPath(fileName, year);
            return File.ReadAllText(path);
        }

        private static string GetFullPath(string fileName, int year) =>
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $@"{year}\Inputs\{fileName}.txt");
    }
}
