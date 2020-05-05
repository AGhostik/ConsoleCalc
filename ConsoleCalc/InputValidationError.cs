namespace ConsoleCalc
{
    public class InputValidationError
    {
        public InputValidationError(int index, char character)
        {
            Index = index;
            Character = character;
        }

        public int Index { get; }
        public char Character { get; }

        public override string ToString()
        {
            return $"index: \'{Index}\', character: \'{Character}\'";
        }
    }
}