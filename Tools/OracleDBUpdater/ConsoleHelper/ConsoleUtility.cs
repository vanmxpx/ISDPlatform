using System;

namespace ConsoleHelper
{
    public static class ConsoleUtility
    {
        public static void WriteLine(string line, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(line);
            Console.ResetColor();
        }

        public static void Write(string line, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(line);
            Console.ResetColor();
        }

        public static string ReadLine(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            string line = Console.ReadLine();
            Console.ResetColor();
            return line;
        }
    }
}
