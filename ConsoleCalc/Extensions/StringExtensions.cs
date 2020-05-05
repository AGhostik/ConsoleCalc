using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

        public static string AddBracers(this string value)
        {
            var regexMultiply = RegexService.GetRegex_FindExpessionForBracersAdding();
            
            var result = value;

            // key = {"X"} , value = {1 + 1}
            var replacements = new Dictionary<string, string>();
            var replacementCounter = 0;

            //это все нужно для того чтобы каждую операцию закрыть в скобочки для парсера
            //например: '1 * 5 + 3 * 2 + 1' => '((1 * 5) + (3 * 2)) + 1'

            // поиск выражений с умножением, делением или остатком от деления, и замена на [числа в кавычках]
            var matches = regexMultiply.Matches(result).ToArray();
            while (matches.Any())
            {
                foreach (var match in matches)
                {
                    var replacementKey = $"\"{replacementCounter}\"";
                    replacementCounter++;
                    replacements.Add(replacementKey, match.Value);
                    result = regexMultiply.Replace(result, m => replacementKey, 1, match.Index);
                }

                matches = regexMultiply.Matches(result).ToArray();
            }

            //todo: эти два куска кода while{...} можно заменить на один приватный метод

            // поиск выражений со сложением или вычитанием, и замена на [числа в кавычках]
            var regexPlusMinus = RegexService.GetRegex_FindExpessionForBracersAdding(false);
            matches = regexPlusMinus.Matches(result).ToArray();
            while (matches.Any())
            {
                foreach (var match in matches)
                {
                    var replacementKey = $"\"{replacementCounter}\"";
                    replacementCounter++;
                    replacements.Add(replacementKey, match.Value);
                    result = regexPlusMinus.Replace(result, m => replacementKey, 1, match.Index);
                }

                matches = regexPlusMinus.Matches(result).ToArray();
            }
            
            // востанавливаем выражения
            // постепенно возвращаем на место [чисел в кавычках] выражения, дополнительно обрамляя их в скобочки
            var regexReplacements = new Regex("\"\\d\"");
            var replacementsMatches = regexReplacements.Matches(result).ToArray();
            while (replacementsMatches.Any())
            {
                foreach (var match in replacementsMatches)
                {
                    result = regexReplacements.Replace(result, m =>
                    {
                        var replacement = replacements[match.Value];
                        return $"({replacement})";
                    }, 1, match.Index);
                }
                replacementsMatches = regexReplacements.Matches(result).ToArray();
            }

            //на самом деле тогда все выражение целиком будет закрыто в скобочки, поэтому нужно их удалить с помощью string.Substring
            return result.Substring(1, result.Length - 2);
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