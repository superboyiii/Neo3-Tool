using System;
using System.Linq;
using Neo;
using Neo.IO;
using static Neo.Helper;

namespace Decoding
{
    class Base64ByteArrayToAddress
    {
         private bool base64ByteArrayToAddress(string value)
        {
            /*String value = "0wzwBoLXDacAgxEkGaxxo1Ezxh4=";*/
            byte[] result = Convert.FromBase64String(value).Reverse().ToArray();
            String hex = result.ToHexString();
            var scripthash = UInt160.Parse(hex);
            String address = Neo.Wallets.Helper.ToAddress(scripthash);
            Console.WriteLine(hex);
            Console.WriteLine(address);
            return true;
        }
    }
}
