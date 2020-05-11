using System.Text.RegularExpressions;
using ConsoleCalc.Extensions;
using ConsoleCalc.Models;

namespace ConsoleCalc
{
    public static class RegexService
    {
        /// <summary>
        /// Для поиска выражений типа 'X [действие] Y', где:
        /// <para>[действие] - это -, +, *, / или %</para>
        /// <para>X или Y - это число, отрицательное число, guid в кавычках (например: "0"), другое аналогичное выражение 'A [действие] B' </para>
        /// <para>guid в кавычках нужно в качестве костыля, чтобы заменять ими выражения 'X [действие] Y' - таким образом одно и то же выражение не будет снова попадать под повторный поиск выражений в той же строке  на следующей итерации;</para>
        /// 
        /// <para>Пример:</para>
        /// <para>Input: '1 * 2 * 3 + 4', match = '1 * 2' --> '"guid_0" * 3 + 4', match = '"guid_0" * 3' --> '"guid_1" + 4', match = NULL </para>
        /// <para>Reverce step1: '"guid_1" + 4', match = '"guid_1"', replacement = ( + '"guid_0" * 3' + ) --> '("guid_0" * 3) + 4'</para>
        /// <para>Reverce step2: '("guid_0" * 3) + 4', match = '"guid_0"', replacement = ( + '1 * 2' + ) --> '((1 * 2) * 3) + 4'</para>
        /// <para>Result: '1 * 2 * 3 + 4' --> '((1 * 2) * 3) + 4'</para>
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public static Regex GetRegex_FindExpessionForBracersAdding(Operation operation)
        {
            var operationChar = operation.ToChar();
            
            return new Regex($"(?<firstValue>[(]-?\\d+\\.?\\d+\\s*[-+*/%]\\s*-?\\d+\\.?\\d+[)]|-?\\d+\\.?\\d+|-?\\d+|\"[a-z\\d_]+\")\\s*(?<operation>[{operationChar}])\\s*(?<secondValue>[(]-?\\d+\\.?\\d+\\s*[-+*/%]\\s*-?\\d+\\.?\\d+[)]|-?\\d+\\.?\\d+|-?\\d+|\"[a-z\\d_]+\")");
        }

        /// <summary>
        /// Ищет знаки пробела, идущие по 2 или более подряд
        /// </summary>
        /// <returns></returns>
        public static Regex GetRegex_FindMultipleSpacebars()
        {
            return new Regex("[ ]{2,}");
        }

        /// <summary>
        /// Ищет знаки минуса, идущие по 2 или более подряд
        /// </summary>
        /// <returns></returns>
        public static Regex GetRegex_FindMultipleMinusSymbol()
        {
            return new Regex("[-]{2,}");
        }

        /// <summary>
        /// Находит Guid в кавычках
        /// </summary>
        /// <returns></returns>
        public static Regex GetRegex_FindGuid()
        {
            return new Regex("\"[a-z\\d_]+\"");
        }

        /// <summary>
        /// Для поиска частей внутри выражений типа 'X [действие] Y'
        /// <para>X или Y - число, отрицательное число, другое аналогичное выражение 'A [действие] B'</para>
        /// <para>[действие] - это +, -, *, / или %</para>
        /// <para>Разделение на группы (match.Groups): [firstValue] [operation] [secondValue]</para>
        /// </summary>
        /// <returns></returns>
        public static Regex GetRegex_FindExpressionParts()
        {
            return new Regex("(?<firstValue>(\\(-?\\d*\\.?\\d*\\s*[-+*/%]\\s*-?\\d*\\.?\\d*\\))|(-?\\d*\\.?\\d*))\\s*(?<operation>[-+*/%])\\s*(?<secondValue>(\\(-?\\d*\\.?\\d*\\s*[-+*/%]\\s*-?\\d*\\.?\\d*\\))|(-?\\d*\\.?\\d*))");
        }
    }
}
