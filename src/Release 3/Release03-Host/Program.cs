using System;

namespace Bnaya.Samples
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var info = string.Join("\r\n", args);
            Console.WriteLine(info);
        }
    }
}
