using System;

namespace ConsoleCalc
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Hello Calc!");
            Console.WriteLine("Type \"exit\" to stop program");
            Console.WriteLine();
            
            do
            {
                var input = Console.ReadLine();

                if (input?.ToLowerInvariant() == "exit")
                    break;

                try
                {

                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
                finally
                {
                    Console.WriteLine();
                }
            } while (true);
        }
    }
}
