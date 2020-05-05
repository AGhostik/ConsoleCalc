using System.Collections.Generic;

namespace ConsoleCalc
{
    public class InputValidationService
    {
        private readonly HashSet<char> _validCharacters = new HashSet<char>
        {
            ' ',
            '.',
            '(',
            ')',
            '-',
            '+',
            '/',
            '%',
            '*'
        };

        public IEnumerable<InputValidationError> FindUnexpectedCharacters(string input)
        {
            var result = new List<InputValidationError>();
            for (var i = 0; i < input.Length; i++)
            {
                var character = input[i];
                if (!char.IsDigit(character) && !_validCharacters.Contains(character))
                    result.Add(new InputValidationError(i + 1, character));
            }

            return result;
        }

        public IEnumerable<InputValidationError> FindInvalidBracket(string input)
        {
            var result = new List<InputValidationError>();
            var openedBrackets = 0;
            for (var i = 0; i < input.Length; i++)
            {
                var character = input[i];
                if (character == '(')
                    openedBrackets++;
                if (character == ')')
                {
                    openedBrackets--;
                    if (openedBrackets < 0)
                    {
                        result.Add(new InputValidationError(i + 1, character));
                        openedBrackets = 0;
                    }
                }
            }
            if (openedBrackets > 0)
                result.Add(new InputValidationError(input.Length + 1, '?'));

            return result;
        }
    }
}