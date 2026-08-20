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
		_logger.LogDebug(">>> Entering Encode(text={Text})", text);
        text = text.ToUpper(CultureInfo.CurrentCulture);
        text = TrimSpaces(text);
		_logger.LogDebug("Encode(text={Text}): text → {Text1}", text, text);
        text = StartReplace(text);
        text = EndReplace(text);

        for (var i = 1; i < text.Length; i++)
        {
            text = ReplaceStep(text, i);
        }

        text = RemoveDuplicates(text);
		_logger.LogDebug("<<< Exiting Encode(text={Text})", text);
        return TrimEnd(text);
    }

    private string TrimSpaces(string text) => text.Replace(" ", string.Empty);

    private string RemoveDuplicates(string text)
    {
		_logger.LogDebug(">>> Entering RemoveDuplicates(text={Text})", text);
        var sb = new StringBuilder();
        sb.Append(text[0]);
		_logger.LogDebug("RemoveDuplicates(text={Text}): sb → {Sb}", text, sb);
        foreach (var c in text)
        {
            if (sb[^1] != c)
            {
                sb.Append(c);
            }
        }

		_logger.LogDebug("<<< Exiting RemoveDuplicates(text={Text})", text);
        return sb.ToString();
    }

    private string TrimEnd(string text)
    {
		_logger.LogDebug(">>> Entering TrimEnd(text={Text})", text);
        var checks = new (string From, string To)?[]
        {
            ("S", string.Empty),
            ("AY", "Y"),
            ("A", string.Empty),
        };
        var replacement = checks.FirstOrDefault(t => text.EndsWith(t!.Value.From));
		_logger.LogDebug("TrimEnd(text={Text}): replacement → {Replacement}", text, replacement);
        if (replacement is { })
        {
            var (from, to) = replacement!.Value;
            text = Replace(text, text.Length - from.Length, from.Length, to);
			_logger.LogDebug("TrimEnd(text={Text}): text → {Text1}", text, text);
        }

		_logger.LogDebug("<<< Exiting TrimEnd(text={Text})", text);
        return text;
    }

    private string ReplaceStep(string text, int i)
    {
		_logger.LogDebug(">>> Entering ReplaceStep(text={Text},i={I})", text, i);
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
		_logger.LogDebug("ReplaceStep(text={Text},i={I}): replaced → {Replaced}", text, i, replaced);
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

		_logger.LogDebug("<<< Exiting ReplaceStep(text={Text},i={I})", text, i);
        return text;

        string ReplaceWithPrevious() => Replace(text, i, 1, text[i - 1].ToString());
    }

    private bool TryReplace(string text, int index, (string, string)[] opts, out string result)
    {
		_logger.LogDebug(">>> Entering TryReplace(text={Text},index={Index},opts={Opts})", text, index, opts);
        for (var i = 0; i < opts.Length; i++)
        {
            var check = opts[i].Item1;
            var repl = opts[i].Item2;
            if (text.Length >= index + check.Length && text.Substring(index, check.Length) == check)
            {
                result = Replace(text, index, check.Length, repl);
				_logger.LogDebug("TryReplace(text={Text},index={Index},opts={Opts}): result → {Result}", text, index, opts, result);
                return true;
            }
        }

        result = text;
        return false;
    }

    private string StartReplace(string start)
    {
		_logger.LogDebug(">>> Entering StartReplace(start={Start})", start);
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
		_logger.LogDebug("StartReplace(start={Start}): replacement → {Replacement}", start, replacement);
        if (replacement is { })
        {
            var (from, to) = replacement!.Value;
            start = Replace(start, 0, from.Length, to);
        }

        return start;
		_logger.LogDebug("<<< Exiting StartReplace(start={Start})", start);
    }

    private string EndReplace(string end)
    {
		_logger.LogDebug(">>> Entering EndReplace(end={End})", end);
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
		_logger.LogDebug("EndReplace(end={End}): replacement → {Replacement}", end, replacement);
        if (replacement is { })
        {
            var (from, to) = replacement!.Value;
            end = Replace(end, end.Length - from.Length, from.Length, to);
			_logger.LogDebug("EndReplace(end={End}): end → {End1}", end, end);
        }

		_logger.LogDebug("<<< Exiting EndReplace(end={End})", end);
        return end;
    }

    private string Replace(string text, int index, int length, string substitute) =>
        text[..index] + substitute + text[(index + length)..];
}
