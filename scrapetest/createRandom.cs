using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scrapetest
{
    public static class createRandom
    {
        public static string random_string(int length)
        {
            Random ran = new Random();

            const string b = "abcdefghijklmnopqrstuvwxyz0123456789";
            string random = "";

            for (int i = 0; i < length; i++)
            {
                int a = ran.Next(b.Length); //string.Lenght gets the size of string
                random += b[a];
            }

            return random;
        }
    }
}
