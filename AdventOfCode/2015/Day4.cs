using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode._2015
{
    public class Day4 : Day, IChallenge
    {
        public IEnumerable<string> Run()
        {
            yield return ResultString(GetNumber(Input[0]));
            yield return ResultString(GetNumber(Input[0], true));
        }

        private int GetNumber(string input, bool getSix = false)
        {
            using (var md5Hash = MD5.Create())
            {
                var number = 0;
                while (true)
                {
                    byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input + number));
                    var values = data.Take(3).Select(x => x.ToString("X2")).ToList();
                    if (values[0] == "00" && values[1] == "00" && values[2][0] == '0' && (getSix ? values[2][1] == '0' : true))
                        return number;
                    else
                        number++;
                }
            }
        }
    }
}
