namespace Luthor.Lexers;

public static class NumericClasses
{
    public static Lexer MatchInteger => segment =>
    {
        var start = segment;
        if (segment.Peek() is '-' or '+')
        {
            segment = segment.Advance();
        }

        var lexeme = CharacterClasses.IsDigit(segment);
        if (lexeme.Success)
        {
            while (lexeme.Success)
            {
                segment = segment.Advance();
                lexeme = CharacterClasses.IsDigit(segment);
            }

            return Lexeme.Hit(start, segment);
        }

        return Lexeme.Miss(start);
    };

    public static Lexer MatchFloatingPoint => segment =>
    {
        var start = segment;
        var lexeme = MatchInteger(segment);
        if (lexeme.Success)
        {
            segment = lexeme.Remainder;
            if (segment.Peek() == '.')
            {
                segment = segment.Advance();
                lexeme = CharacterClasses.IsDigit(segment);
                if (lexeme.Success)
                {
                    while (lexeme.Success)
                    {
                        segment = segment.Advance();
                        lexeme = CharacterClasses.IsDigit(segment);
                    }

                    return Lexeme.Hit(start, segment);
                }
            }
        }

        return Lexeme.Miss(start);
    };
}
