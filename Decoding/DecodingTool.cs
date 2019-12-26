using System;
using System.Linq;
using Neo;
using Neo.IO;
using System.Numerics;

/*This is a tool for NEO3 data convert*/

namespace Decoding
{
    class DecodingTool
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Neo3 Decoding Tool v1.0.0");
            Console.WriteLine();
            Console.WriteLine("Input [help] to see details.");
            Console.WriteLine();
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
                case "hexstringtostring":
                    return HexStringToString(args);
                case "stringtohexstring":
                    return StringToHexString(args);
                case "hexnumbertobiginteger":
                    return HexNumberToBigInteger(args);
                case "bigintegertohexnumber":
                    return BigIntegerToHexNumber(args);
                case "base64hexstringtostring":
                    return Base64HexStringToString(args);
                case "base64bytearraytoaddress":
                    return Base64ByteArrayToAddress(args);
                case "base64bytearraytobiginteger":
                    return Base64ByteArrayToBigInteger(args);
                case "addresstoscripthash":
                    return AddressToScriptHash(args);
                case "littleendscripthashtoaddress":
                    return LittleEndScriptHashToAddress(args);
                case "bigendscripthashtoaddress":
                    return BigEndScriptHashToAddress(args);
                case "bigandlittleendexchange":
                    return BigAndLittleEndExchange(args);
                default:
                    Console.WriteLine("error: command not found ");
                    Console.WriteLine();
                    return OnCommand();
            }
        }

        private static bool OnHelpCommand()
        {
            Console.WriteLine("Command List:");
            Console.Write("\tHexStringToString [hex] ");
            Console.SetCursorPosition(70, Console.CursorTop);
            Console.WriteLine("输入HexString转换成String");
            Console.Write("\tStringToHexString [string] ");
            Console.SetCursorPosition(70, Console.CursorTop);
            Console.WriteLine("输入String转换成HexString");
            Console.Write("\tHexNumberToBigInteger [hex] ");
            Console.SetCursorPosition(70, Console.CursorTop);
            Console.WriteLine("输入HexNumber转换成BigInteger");
            Console.Write("\tBigIntegerToHexNumber [bigInteger] ");
            Console.SetCursorPosition(70, Console.CursorTop);
            Console.WriteLine("输入BigInteger转换成HexNumber");
            Console.Write("\tBase64HexStringToString [base64HexString] ");
            Console.SetCursorPosition(70, Console.CursorTop);
            Console.WriteLine("输入Base64HexString转换成String");
            Console.Write("\tBase64ByteArrayToAddress [base64ByteArray] ");
            Console.SetCursorPosition(70, Console.CursorTop);
            Console.WriteLine("输入base64的ByteArray转换成Neo3标准地址");
            Console.Write("\tBase64ByteArrayToBigInteger [base64ByteArray] ");
            Console.SetCursorPosition(70, Console.CursorTop);
            Console.WriteLine("输入base64的ByteArray转换成BigInteger");
            Console.Write("\tAddressToScriptHash [standardAddress] ");
            Console.SetCursorPosition(70, Console.CursorTop);
            Console.WriteLine("输入标准地址转成大小端序的script hash");
            Console.Write("\tLittleEndScriptHashToAddress [littleEndScriptHash] ");
            Console.SetCursorPosition(70, Console.CursorTop);
            Console.WriteLine("输入小端序的script hash转换成地址");
            Console.Write("\tBigEndScriptHashToAddress [bigEndScriptHash] ");
            Console.SetCursorPosition(70, Console.CursorTop);
            Console.WriteLine("输入大端序的script hash转换成地址");
            Console.Write("\tBigAndLittleEndExchange [scriptHash(bigEnd or littleEnd)] ");
            Console.SetCursorPosition(70, Console.CursorTop);
            Console.WriteLine("script hash大小端序互换");
            Console.WriteLine();
            return OnCommand();
        }

        private static bool HexStringToString(string[] args)
        {
            //String args[1] = "7472616e73666572";
            try
            {
                Console.WriteLine(System.Text.Encoding.ASCII.GetString(args[1].HexToBytes()));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {Environment.NewLine}{e.Message}");
            }
            Console.WriteLine();
            return OnCommand();
        }

        private static bool StringToHexString(string[] args)
        {
            //String args[1] = "Transfer";
            try
            {
                Console.WriteLine((System.Text.Encoding.ASCII.GetBytes(args[1])).ToHexString());
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {Environment.NewLine}{e.Message}");
            }
            Console.WriteLine();
            return OnCommand();
        }

        private static bool HexNumberToBigInteger(string[] args)
        {
            //String args[1] = "00c2eb0b";
            try
            {
                String bigEnd = args[1].HexToBytes().Reverse().ToArray().ToHexString();
                Console.WriteLine(System.Numerics.BigInteger.Parse(bigEnd, System.Globalization.NumberStyles.HexNumber));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {Environment.NewLine}{e.Message}");
            }
            Console.WriteLine();
            return OnCommand();
        }

        private static bool BigIntegerToHexNumber(string[] args)
        {
            //String args[1] = "100000000";
            try
            {
                String bigEndHex = BigInteger.Parse(args[1]).ToByteArray().ToHexString();
                String littleEndHex = bigEndHex.HexToBytes().Reverse().ToArray().ToHexString();
                Console.WriteLine("BigEnd: " + bigEndHex);
                Console.WriteLine("LittleEnd: " + littleEndHex);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {Environment.NewLine}{e.Message}");
            }
            Console.WriteLine();
            return OnCommand();
        }

        private static bool Base64HexStringToString(string[] args)
        {
            //String args[1] = "VHJhbnNmZXI=";
            try
            {
                Console.WriteLine(System.Text.Encoding.ASCII.GetString(Convert.FromBase64String(args[1])));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {Environment.NewLine}{e.Message}");
            }
            Console.WriteLine();
            return OnCommand();
        }

        private static bool Base64ByteArrayToAddress(string[] args)
        {
            //String args[1] = "0wzwBoLXDacAgxEkGaxxo1Ezxh4=";
            String value = args[1];
            try
            {
                byte[] result = Convert.FromBase64String(value).Reverse().ToArray();
                String hex = result.ToHexString();
                var scripthash = UInt160.Parse(hex);
                String address = Neo.Wallets.Helper.ToAddress(scripthash);
                Console.WriteLine("Hex: " + hex);
                Console.WriteLine("Standard Address: " + address);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {Environment.NewLine}{e.Message}");
            }
            Console.WriteLine();
            return OnCommand();
        }

        private static bool Base64ByteArrayToBigInteger(string[] args)
        {
            //String args[1] = "AADBb/KGIw==";
            String value = args[1];
            try
            {
                String littleEnd = Convert.FromBase64String(value).ToArray().ToHexString();
                String bigEnd = Convert.FromBase64String(value).Reverse().ToArray().ToHexString();
                var number = System.Numerics.BigInteger.Parse(bigEnd, System.Globalization.NumberStyles.AllowHexSpecifier);
                Console.WriteLine("LittleEnd: " + littleEnd);
                Console.WriteLine("BigEnd: " + bigEnd);
                Console.WriteLine("BigInteger number:" + number);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {Environment.NewLine}{e.Message}");
            }
            Console.WriteLine();
            return OnCommand();
        }

        private static bool AddressToScriptHash(string[] args)
        {
            //String args[1] = "NeHNBbeLNtiCEeaFQ6tLLpXkr5Xw6esKnV";
            String address = args[1];
            try
            {
                UInt160 bigEnd = Neo.Wallets.Helper.ToScriptHash(address);
                String littleEnd = bigEnd.ToArray().ToHexString();
                Console.WriteLine("BigEnd: " + bigEnd);
                Console.WriteLine("LittleEnd: " + littleEnd);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {Environment.NewLine}{e.Message}");
            }
            Console.WriteLine();
            return OnCommand();
        }

        private static bool LittleEndScriptHashToAddress(string[] args)
        {
            //String args[1] = "c97e324bac15a4ea589f423e4b29a7210b8fad09";
            try
            {
                UInt160 littleEnd = UInt160.Parse(args[1]);
                String bigEnd = littleEnd.ToArray().ToHexString();
                UInt160 scriptHash = UInt160.Parse(bigEnd);
                String address = Neo.Wallets.Helper.ToAddress(scriptHash);
                Console.WriteLine("Standard Address: " + address);
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error: {Environment.NewLine}{e.Message}");
            }
            Console.WriteLine();
            return OnCommand();
        }

        private static bool BigEndScriptHashToAddress(string[] args)
        {
            //String args[1] = "0x09ad8f0b21a7294b3e429f58eaa415ac4b327ec9";
            UInt160 scriptHash = UInt160.Parse(args[1]);
            try
            {
                String address = Neo.Wallets.Helper.ToAddress(scriptHash);
                Console.WriteLine("Standard Address: " + address);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {Environment.NewLine}{e.Message}");
            }
            Console.WriteLine();
            return OnCommand();
        }

        private static bool BigAndLittleEndExchange(string[] args)
        {
            //String args[1] = "c97e324bac15a4ea589f423e4b29a7210b8fad09";
            try
            {
                String reverse = args[1].HexToBytes().Reverse().ToArray().ToHexString();
                Console.WriteLine("LitteleEnd <=> BigEnd: " + reverse);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {Environment.NewLine}{e.Message}");
            }
            Console.WriteLine();
            return OnCommand();
        }

    }
}
