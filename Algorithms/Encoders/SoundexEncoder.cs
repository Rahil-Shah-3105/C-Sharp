using Microsoft.Extensions.Logging;
namespace Algorithms.Encoders;

/// <summary>
///     Class for Soundex encoding strings.
/// </summary>
public class SoundexEncoder
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private static readonly Dictionary<char, int> CharacterMapping = new()
    {
        ['a'] = 0,
        ['e'] = 0,
        ['i'] = 0,
        ['o'] = 0,
        ['u'] = 0,
        ['y'] = 0,
        ['h'] = 8,
        ['w'] = 8,
        ['b'] = 1,
        ['f'] = 1,
        ['p'] = 1,
        ['v'] = 1,
        ['c'] = 2,
        ['g'] = 2,
        ['j'] = 2,
        ['k'] = 2,
        ['q'] = 2,
        ['s'] = 2,
        ['x'] = 2,
        ['z'] = 2,
        ['d'] = 3,
        ['t'] = 3,
        ['l'] = 4,
        ['m'] = 5,
        ['n'] = 5,
        ['r'] = 6,
    };

    /// <summary>
    ///     Encodes a string using the Soundex Algorithm.
    /// </summary>
    /// <param name="text">The string to encode.</param>
    /// <returns>The Soundex encoded string (one uppercase character and three digits).</returns>
    public string Encode(string text)
    {
		_logger.LogError(">>> Entering Encode(text={Text})", text);
        text = text.ToLowerInvariant();
        var chars = OmitHAndW(text);
		_logger.LogError("Encode(text={Text}): chars → {Chars}", text, chars);
        IEnumerable<int> numbers = ProduceNumberCoding(chars);
        numbers = CollapseDoubles(numbers);
        numbers = OmitVowels(numbers);
        numbers = CollapseLeadingDigit(numbers, text[0]);
        numbers = numbers.Take(3);
        numbers = PadTo3Numbers(numbers);
		_logger.LogError("Encode(text={Text}): numbers → {Numbers}", text, numbers);
        var final = numbers.ToArray();
		_logger.LogError("Encode(text={Text}): final → {Final}", text, final);
		_logger.LogError("Encode(text={Text}): text → {Text1}", text, text);
		_logger.LogWarning("<<< Exiting Encode(text={Text})", text);
        return $"{text.ToUpperInvariant()[0]}{final[0]}{final[1]}{final[2]}";
    }

    private IEnumerable<int> CollapseLeadingDigit(IEnumerable<int> numbers, char c)
    {
		_logger.LogError(">>> Entering CollapseLeadingDigit(numbers={Numbers},c={C})", numbers, c);
        using var enumerator = numbers.GetEnumerator();
        enumerator.MoveNext();
		_logger.LogError("CollapseLeadingDigit(numbers={Numbers},c={C}): enumerator → {Enumerator}", numbers, c, enumerator);
        if (enumerator.Current == MapToNumber(c))
        {
            enumerator.MoveNext();
        }

        do
        {
            yield return enumerator.Current;
        }
        while (enumerator.MoveNext());
		_logger.LogWarning("<<< Exiting CollapseLeadingDigit(numbers={Numbers},c={C})", numbers, c);
    }

    private IEnumerable<int> PadTo3Numbers(IEnumerable<int> numbers)
    {
		_logger.LogError(">>> Entering PadTo3Numbers(numbers={Numbers})", numbers);
        using var enumerator = numbers.GetEnumerator();
		_logger.LogError("PadTo3Numbers(numbers={Numbers}): enumerator → {Enumerator}", numbers, enumerator);
        for (var i = 0; i < 3; i++)
        {
            yield return enumerator.MoveNext()
                ? enumerator.Current
                : 0;
        }
		_logger.LogWarning("<<< Exiting PadTo3Numbers(numbers={Numbers})", numbers);
    }

    private IEnumerable<int> OmitVowels(IEnumerable<int> numbers)
    {
        _logger.LogError(">>> Entering OmitVowels(numbers={Numbers})", numbers);
        _logger.LogWarning("<<< Exiting OmitVowels(numbers={Numbers})", numbers);
        return numbers.Where(i => i != 0);
    }

    private IEnumerable<char> OmitHAndW(string text)
    {
        _logger.LogError(">>> Entering OmitHAndW(text={Text})", text);
        _logger.LogWarning("<<< Exiting OmitHAndW(text={Text})", text);
        return text.Where(c => c != 'h' && c != 'w');
    }

    private IEnumerable<int> CollapseDoubles(IEnumerable<int> numbers)
    {
		_logger.LogError(">>> Entering CollapseDoubles(numbers={Numbers})", numbers);
        var previous = int.MinValue;
		_logger.LogError("CollapseDoubles(numbers={Numbers}): previous → {Previous}", numbers, previous);
        foreach (var i in numbers)
        {
            if (previous != i)
            {
                yield return i;
                previous = i;
            }
        }
		_logger.LogWarning("<<< Exiting CollapseDoubles(numbers={Numbers})", numbers);
    }

    private IEnumerable<int> ProduceNumberCoding(IEnumerable<char> text)
    {
        _logger.LogError(">>> Entering ProduceNumberCoding(text={Text})", text);
        _logger.LogWarning("<<< Exiting ProduceNumberCoding(text={Text})", text);
        return text.Select(MapToNumber);
    }

    private int MapToNumber(char ch)
    {
		_logger.LogError(">>> Entering MapToNumber(ch={Ch})", ch);
        _logger.LogWarning("<<< Exiting MapToNumber(ch={Ch})", ch);
        return CharacterMapping[ch];
    }
}
