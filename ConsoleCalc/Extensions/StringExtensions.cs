using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ConsoleCalc.Models;

namespace ConsoleCalc.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        ///     Пока что работает только на знаки "-"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveExcessLeadingSign(this string value)
        {
            var regex = RegexService.GetRegex_FindMultipleMinusSymbol();
            var match = regex.Match(value);
            if (match.Success)
            {
                if (match.Length % 2 > 0)
                    return regex.Replace(value, "-");
                return regex.Replace(value, "");
            }

            return value;
        }

        public static string RemoveExcessSpacebar(this string value)
        {
            var regex = RegexService.GetRegex_FindMultipleSpacebars();
            var result = regex.Replace(value, " ");
            return result;
        }

        /// <summary>
        /// Поиск выражений и замена на "guid"; Возвращает строку-результат и список замен [guid, "выражение"]
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private static (string result, IEnumerable<KeyValuePair<string, string>> replacements) ReplaceExpressions(Regex regex, string input)
        {
            var result = input;
            var replacements = new Dictionary<string, string>();
            var matches = regex.Matches(result).ToArray();
            while (matches.Any())
            {
                foreach (var match in matches)
                {
                    var replacementKey = $"\"{GuidService.NewGuid()}\"";
                    replacements.Add(replacementKey, match.Value);
                    result = regex.Replace(result, m => replacementKey, 1, match.Index);
                }

                matches = regex.Matches(result).ToArray();
            }

            return (result, replacements);
        }

        public static string AddBracers(this string value)
        {
            //это все нужно для того чтобы каждую операцию закрыть в скобочки для парсера
            //например: '1 * 5 + 3 * 2 + 1' => '((1 * 5) + (3 * 2)) + 1'
            var result = value;

            // key = {"X"} , value = {1 + 1}
            var replacements = new Dictionary<string, string>();

            Replace(RegexService.GetRegex_FindExpessionForBracersAdding(Operation.DivRem));
            Replace(RegexService.GetRegex_FindExpessionForBracersAdding(Operation.Div));
            Replace(RegexService.GetRegex_FindExpessionForBracersAdding(Operation.Multiply));
            Replace(RegexService.GetRegex_FindExpessionForBracersAdding(Operation.Minus));
            Replace(RegexService.GetRegex_FindExpessionForBracersAdding(Operation.Plus));

            void Replace(Regex regex)
            {
                var (s, keyValuePairs) = ReplaceExpressions(regex, result);
                result = s;
                foreach (var pair in keyValuePairs)
                {
                    replacements.Add(pair.Key, pair.Value);
                }
            }

            // востанавливаем выражения
            var findGuid = RegexService.GetRegex_FindGuid();
            var replacementsMatches = findGuid.Matches(result).ToArray();
            while (replacementsMatches.Any())
            {
                foreach (var match in replacementsMatches)
                {
                    result = findGuid.Replace(result, m =>
                    {
                        var replacement = replacements[match.Value];
                        return $"({replacement})";
                    }, 1, match.Index);
                }
                replacementsMatches = findGuid.Matches(result).ToArray();
            }

            return result;
        }

        public static string AddSpacebars(this string value)
        {
            var result = value;
            var valueLength = value.Length;

            if (valueLength < 2) return value;

            for (var i = 1; i < valueLength; i++)
            {
                // todo: переделать этот ужас
                // todo: сделать regex с паттерном типо \\d[-+/%*]*\\d

                var previousOfPreviousCharType = i > 2 ? GetCharacterType(result[i - 3]) : -2;
                var previousCharType = GetCharacterType(result[i - 1]);
                var currentCharType = GetCharacterType(result[i]);

                // not a spacebar
                if (previousCharType != 0 && currentCharType != 0)
                    // not a "-X" and not a "[operation] -X"
                    if (!(previousOfPreviousCharType > 0 && previousCharType == 2 && currentCharType == -1))
                        // not a Digit characters
                        if (!(previousCharType == -1 && currentCharType == -1))
                        {
                            result = result.Insert(i, " ");
                            valueLength++;
                            i++;
                        }
            }

            return result;

            int GetCharacterType(char value)
            {
                switch (value)
                {
                    case ' ':
                        return 0;
                    case '+':
                        return 1;
                    case '-':
                        return 2;
                    case '/':
                        return 3;
                    case '*':
                        return 4;
                    case '%':
                        return 5;
                    default:
                        return -1;
                }
            }
        }
    }
}