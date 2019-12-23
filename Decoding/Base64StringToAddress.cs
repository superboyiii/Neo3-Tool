using System;
using System.Linq;
using Neo;
using Neo.IO;
using static Neo.Helper;

namespace Decoding
{
    class Base64StringToAddress
    {
         void base64StringToAddress(string value)
        {
            /*String value = "NeHNBbeLNtiCEeaFQ6tLLpXkr5Xw6esKnV";*/
            byte[] result = Convert.FromBase64String(value).Reverse().ToArray();
            String hex = result.ToHexString();
            var number = System.Numerics.BigInteger.Parse(hex, System.Globalization.NumberStyles.AllowHexSpecifier);
            Console.WriteLine(hex);
            Console.WriteLine(number);
            Console.ReadKey();
        }
    }
}
