using Microsoft.Extensions.Logging;
namespace Algorithms.Encoders;

/// <summary>
///     Class for NYSIIS encoding strings.
/// </summary>
public class NysiisEncoder
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    private static readonly char[] Vowels = ['A', 'E', 'I', 'O', 'U'];

    /// <summary>
    ///     Encodes a string using the NYSIIS Algorithm.
    /// </summary>
    /// <param name="text">The string to encode.</param>
    /// <returns>The NYSIIS encoded string (all uppercase).</returns>
    public string Encode(string text)
    {
		_logger.LogError(">>> Entering Encode(text={Text})", text);
        text = text.ToUpper(CultureInfo.CurrentCulture);
        text = TrimSpaces(text);
        text = StartReplace(text);
        text = EndReplace(text);
		_logger.LogError("Encode(text={Text}): text → {Text1}", text, text);

        for (var i = 1; i < text.Length; i++)
        {
            text = ReplaceStep(text, i);
        }

        text = RemoveDuplicates(text);
		_logger.LogWarning("<<< Exiting Encode(text={Text})", text);
        return TrimEnd(text);
    }

    private string TrimSpaces(string text)
    {
        _logger.LogError(">>> Entering TrimSpaces(text={Text})", text);
        _logger.LogWarning("<<< Exiting TrimSpaces(text={Text})", text);
        return text.Replace(" ", string.Empty);
    }

    private string RemoveDuplicates(string text)
    {
		_logger.LogError(">>> Entering RemoveDuplicates(text={Text})", text);
        var sb = new StringBuilder();
        sb.Append(text[0]);
		_logger.LogError("RemoveDuplicates(text={Text}): sb → {Sb}", text, sb);
        foreach (var c in text)
        {
            if (sb[^1] != c)
            {
                sb.Append(c);
            }
        }

		_logger.LogWarning("<<< Exiting RemoveDuplicates(text={Text})", text);
        return sb.ToString();
    }

    private string TrimEnd(string text)
    {
		_logger.LogError(">>> Entering TrimEnd(text={Text})", text);
        var checks = new (string From, string To)?[]
        {
            ("S", string.Empty),
            ("AY", "Y"),
            ("A", string.Empty),
        };
        var replacement = checks.FirstOrDefault(t => text.EndsWith(t!.Value.From));
		_logger.LogError("TrimEnd(text={Text}): replacement → {Replacement}", text, replacement);
        if (replacement is { })
        {
            var (from, to) = replacement!.Value;
            text = Replace(text, text.Length - from.Length, from.Length, to);
			_logger.LogError("TrimEnd(text={Text}): text → {Text1}", text, text);
        }

		_logger.LogWarning("<<< Exiting TrimEnd(text={Text})", text);
        return text;
    }

    private string ReplaceStep(string text, int i)
    {
		_logger.LogError(">>> Entering ReplaceStep(text={Text},i={I})", text, i);
        (string From, string To)[] replacements =
        [
            ("EV", "AF"),
            ("E", "A"),
            ("I", "A"),
            ("O", "A"),
            ("U", "A"),
            ("Q", "G"),
            ("Z", "S"),
            ("M", "N"),
            ("KN", "NN"),
            ("K", "C"),
            ("SCH", "SSS"),
            ("PH", "FF"),
        ];
        var replaced = TryReplace(text, i, replacements, out text);
		_logger.LogError("ReplaceStep(text={Text},i={I}): replaced → {Replaced}", text, i, replaced);
        if (replaced)
        {
            return text;
        }

        // H[vowel] or [vowel]H -> text[i-1]
        if (text[i] == 'H')
        {
            if (!Vowels.Contains(text[i - 1]))
            {
                return ReplaceWithPrevious();
            }

            if (i < text.Length - 1 && !Vowels.Contains(text[i + 1]))
            {
                return ReplaceWithPrevious();
            }
        }

        // [vowel]W -> [vowel]
        if (text[i] == 'W' && Vowels.Contains(text[i - 1]))
        {
            return ReplaceWithPrevious();
        }

		_logger.LogWarning("<<< Exiting ReplaceStep(text={Text},i={I})", text, i);
        return text;

        string ReplaceWithPrevious() => Replace(text, i, 1, text[i - 1].ToString());
    }

    private bool TryReplace(string text, int index, (string, string)[] opts, out string result)
    {
		_logger.LogError(">>> Entering TryReplace(text={Text},index={Index},opts={Opts})", text, index, opts);
        for (var i = 0; i < opts.Length; i++)
        {
            var check = opts[i].Item1;
            var repl = opts[i].Item2;
            if (text.Length >= index + check.Length && text.Substring(index, check.Length) == check)
            {
                result = Replace(text, index, check.Length, repl);
				_logger.LogError("TryReplace(text={Text},index={Index},opts={Opts}): result → {Result}", text, index, opts, result);
                return true;
            }
        }

        result = text;
        return false;
    }

    private string StartReplace(string start)
    {
		_logger.LogError(">>> Entering StartReplace(start={Start})", start);
        _logger.LogError(">>> Entering StartReplace(start={Start})", start);
        var checks = new (string From, string To)?[]
        {
            ("MAC", "MCC"),
            ("KN", "NN"),
            ("K", "C"),
            ("PH", "FF"),
            ("PF", "FF"),
            ("SCH", "SSS"),
        };
        var replacement = checks.FirstOrDefault(t => start.StartsWith(t!.Value.From));
		_logger.LogError("StartReplace(start={Start}): replacement → {Replacement}", start, replacement);
        if (replacement is { })
        {
            var (from, to) = replacement!.Value;
            start = Replace(start, 0, from.Length, to);
        }

		_logger.LogWarning("<<< Exiting StartReplace(start={Start})", start);
        return start;
    }

    private string EndReplace(string end)
    {
		_logger.LogError(">>> Entering EndReplace(end={End})", end);
        var checks = new (string From, string To)?[]
        {
            ("EE", "Y"),
            ("IE", "Y"),
            ("DT", "D"),
            ("RT", "D"),
            ("NT", "D"),
            ("ND", "D"),
        };
        var replacement = checks.FirstOrDefault(t => end.EndsWith(t!.Value.From));
		_logger.LogError("EndReplace(end={End}): replacement → {Replacement}", end, replacement);
        if (replacement is { })
        {
            var (from, to) = replacement!.Value;
            end = Replace(end, end.Length - from.Length, from.Length, to);
			_logger.LogError("EndReplace(end={End}): end → {End1}", end, end);
        }

		_logger.LogWarning("<<< Exiting EndReplace(end={End})", end);
        return end;
    }

    private string Replace(string text, int index, int length, string substitute) =>
        text[..index] + substitute + text[(index + length)..];
}
