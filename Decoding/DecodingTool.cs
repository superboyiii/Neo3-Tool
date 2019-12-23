using System;
using System.Linq;
using Neo;
using Neo.IO;
using static Neo.Helper;


namespace Decoding
{
    class DecodingTool
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Neo3 Decoding Tool v1.0.0");
            Console.WriteLine();
            Console.WriteLine("Input [help] to see details.");
            OnCommand();
        }
        protected static bool OnCommand()
        {
            string str = Console.ReadLine();
            if (str.Length < 4)
            {
                Console.WriteLine("Invalid params");
                str = null;
                return OnCommand();
            }
            char[] ch = { ' ' };
            string[] args = str.Split(ch, StringSplitOptions.RemoveEmptyEntries);

            switch (args[0].ToLower())
            {
                case "help":
                    return OnHelpCommand();
                case "base64bytearraytoaddress":
                    return Base64ByteArrayToAddress(args);
                case "base64bytearraytobiginteger":
                    return Base64ByteArrayToBiginteger(args);
                case "addresstoscripthash":
                    return AddressToScriptHash(args);
                default:
                    Console.WriteLine("error: command not found ");
                    return OnCommand();
            }
        }

        private static bool OnHelpCommand()
        {
            Console.WriteLine("Command List:");
            Console.WriteLine("\tBase64ByteArrayToAddress [base64ByteArray] 输入base64的ByteArray转换成Neo3标准地址");
            Console.WriteLine("\tBase64ByteArrayToBigInteger [base64ByteArray] 输入base64的ByteArray转换成BigInteger");
            Console.WriteLine("\tAddressToScriptHash [standardAddress] 输入标准地址转成大小端序的script hash");
            return OnCommand();
        }

        private static bool Base64ByteArrayToAddress(string[] args)
        {
            /*String value = "0wzwBoLXDacAgxEkGaxxo1Ezxh4=";*/
            String value = args[1];
            byte[] result = Convert.FromBase64String(value).Reverse().ToArray();
            String hex = result.ToHexString();
            var scripthash = UInt160.Parse(hex);
            String address = Neo.Wallets.Helper.ToAddress(scripthash);
            Console.WriteLine("Hex: " + hex);
            Console.WriteLine("Standard Address:" + address);
            return OnCommand();
        }

        private static bool Base64ByteArrayToBiginteger(string[] args)
        {
            /*String value = "UQ ==";*/
            String value = args[1];
            byte[] result = Convert.FromBase64String(value).Reverse().ToArray();
            String hex = result.ToHexString();
            var number = System.Numerics.BigInteger.Parse(hex, System.Globalization.NumberStyles.AllowHexSpecifier);
            Console.WriteLine("Hex: " + hex);
            Console.WriteLine("BigInteger number:" + number);
            return OnCommand();
        }

        private static bool AddressToScriptHash(string[] args)
        {
            /*String value = "NeHNBbeLNtiCEeaFQ6tLLpXkr5Xw6esKnV";*/
            String address = args[1];
            UInt160 bigEnd = Neo.Wallets.Helper.ToScriptHash(address);
            String littleEnd = bigEnd.ToArray().ToHexString();
            Console.WriteLine("BigEnd: " + bigEnd);
            Console.WriteLine("LittleEnd: " + littleEnd);
            //Console.WriteLine(Convert.ToBase64String("02ea3566fecd303f7775292f242be14c778d59c9ea32439e26a8f141b621cc6406".HexToBytes()));
            Console.ReadKey();
            return OnCommand();
        }
    }
}
