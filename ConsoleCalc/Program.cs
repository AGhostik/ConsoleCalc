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

            var expressionEvaluator = new ExpressionEvaluator();
            
            do
            {
                var input = Console.ReadLine();

                if (input?.ToLowerInvariant() == "exit")
                    break;

                try
                {
                    var result = expressionEvaluator.Evaluate(input);
                    Console.WriteLine(result);
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
