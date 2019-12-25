using System;
using System.Linq;
using Neo;
using Neo.IO;
using static Neo.Helper;
using Neo.Ledger;
using Neo.SmartContract;
using Neo.SmartContract.Native;
using Neo.VM;
using Neo.Wallets;
using System.Numerics;

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
                case "hexstringtostring":
                    return HexStringToString(args);
                case "stringtohexstring":
                    return StringToHexString(args);
                case "hexnumbertobiginteger":
                    return HexNumberToBigInteger(args);
                case "bigintegertohexnumber":
                    return BigIntegerToHexNumber(args);
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
            Console.Write("\tBigIntegerToHexNumber [BigInteger] ");
            Console.SetCursorPosition(70, Console.CursorTop);
            Console.WriteLine("输入BigInteger转换成HexNumber");
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
            Console.WriteLine(System.Text.Encoding.ASCII.GetString(args[1].HexToBytes()));
            Console.WriteLine();
            return OnCommand();
        }

        private static bool StringToHexString(string[] args)
        {
            Console.WriteLine((System.Text.Encoding.ASCII.GetBytes(args[1])).ToHexString());
            Console.WriteLine();
            return OnCommand();
        }

        private static bool HexNumberToBigInteger(string[] args)
        {
            String bigEnd = args[1].HexToBytes().Reverse().ToArray().ToHexString();
            Console.WriteLine(System.Numerics.BigInteger.Parse(bigEnd, System.Globalization.NumberStyles.HexNumber));
            Console.WriteLine();
            return OnCommand();
        }

        private static bool BigIntegerToHexNumber(string[] args)
        {
            String bigEndHex = BigInteger.Parse(args[1]).ToByteArray().ToHexString();
            String littleEndHex = bigEndHex.HexToBytes().Reverse().ToArray().ToHexString();
            Console.WriteLine("BigEnd: " + bigEndHex);
            Console.WriteLine("LittleEnd: " + littleEndHex);
            Console.WriteLine();
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
            Console.WriteLine("Standard Address: " + address);
            return OnCommand();
        }

        private static bool Base64ByteArrayToBigInteger(string[] args)
        {
            /*String value = "AADBb/KGIw==";*/
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
            Console.WriteLine();
            return OnCommand();
        }

        private static bool LittleEndScriptHashToAddress(string[] args)
        {
            UInt160 littleEnd = UInt160.Parse(args[1]);
            String bigEnd = littleEnd.ToArray().ToHexString();
            UInt160 scriptHash = UInt160.Parse(bigEnd); 
            String address = Neo.Wallets.Helper.ToAddress(scriptHash);
            Console.WriteLine("Standard Address: " + address);
            Console.WriteLine();
            return OnCommand();
        }

        private static bool BigEndScriptHashToAddress(string[] args)
        {
            UInt160 scriptHash = UInt160.Parse(args[1]);
            String address = Neo.Wallets.Helper.ToAddress(scriptHash);
            Console.WriteLine("Standard Address: " + address);
            Console.WriteLine();
            return OnCommand();
        }

        private static bool BigAndLittleEndExchange(string[] args)
        {
            String reverse = args[1].HexToBytes().Reverse().ToArray().ToHexString();
            Console.WriteLine("LitteleEnd <=> BigEnd: " + reverse);
            Console.WriteLine();
            return OnCommand();
        }

    }
}
