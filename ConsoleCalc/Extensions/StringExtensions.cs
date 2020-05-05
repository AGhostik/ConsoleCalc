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
            var regex = new Regex("[-]{2,}");
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
            var regex = new Regex("[ ]{2,}");
            var result = regex.Replace(value, " ");
            return result;
        }

        public static string AddBracers(this string value)
        {
            var regex = new Regex("((\\(-?\\d+( [-+*/%] -?\\d+)?\\))|(-?\\d+)|(\"\\d\")) [*/%] ((\\(-?\\d+( [-+*/%] -?\\d+)?\\))|(-?\\d+)|(\"\\d\"))");
            
            var result = value;
            var replacements = new Dictionary<string, string>(); // key = {"X"} , value = {1 + 1}
            var replacementCounter = 0;

            var matches = regex.Matches(result).ToArray();
            while (matches.Any())
            {
                foreach (var match in matches)
                {
                    var replacementKey = $"\"{replacementCounter}\"";
                    replacementCounter++;
                    replacements.Add(replacementKey, match.Value);
                    result = regex.Replace(result, m => replacementKey, 1, match.Index);
                }

                matches = regex.Matches(result).ToArray();
            }

            //todo: сюда можно добавить аналогичный Regex, только по середине не [*/%], а [-+]
            //это для того чтобы каждую операцию закрыть в скобочки для парсера
            //например: '1 * 5 + 3 * 2 + 1' => '((1 * 5) + (3 * 2)) + 1' или '(1 * 5) + ((3 * 2) + 1)'
            //на самом деле тогда все выражение целиком будет закрыто в скобочки, то можно это удалить через string.Substring(1, length - 2)
            
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