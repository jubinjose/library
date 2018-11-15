using System;
using System.Linq;

namespace Jubin.Utils.Encryption
{
    public class PasswordUtil
    {
        public static string GeneratePassword(int desiredLength = 8, bool usenumbers = true, bool uselowalphabets = true,
            bool usehighalphabets = true, bool usesymbols = true)
        {

            var upperCase = new char[]
                {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
                'V', 'W', 'X', 'Y', 'Z'
                };

            var lowerCase = new char[]
                {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
                'v', 'w', 'x', 'y', 'z'
                };

            var numerals = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            var symbols = new char[]
                {
                '~', '`', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '{', '[', '}', ']', '-', '_', '=', '+', ':',
                ';', '|', '/', '?', ',', '<', '.', '>'
                };

            char[] total = (new char[0])
                            .Concat(usehighalphabets ? upperCase : new char[0])
                            .Concat(uselowalphabets ? lowerCase : new char[0])
                            .Concat(usenumbers ? numerals : new char[0])
                            .Concat(usesymbols ? symbols : new char[0])
                            .ToArray();

            var rnd = new Random();

            var chars = Enumerable
                .Repeat<int>(0, desiredLength)
                .Select(i => total[rnd.Next(total.Length)])
                .ToArray();

            return new string(chars);
        }
    }
}
