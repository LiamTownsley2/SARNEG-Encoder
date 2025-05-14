using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


partial class Sarneg()
{
    public string Encode(string input, string key)
    {
        Dictionary<char, char> encodeMap = key
        .Select((ch, i) => new { ch, i })
        .ToDictionary(x => (char)('0' + x.i), x => x.ch);
        var output = new string(input.Select(ch => encodeMap.TryGetValue(ch, out var decoded) ? decoded : ch).ToArray());
        return Regex.Replace(output, @"[^A-Z\s]", "", RegexOptions.IgnoreCase);
    }

    public string GetValidChallengeWord()
    {
        string? challengeWord;
        do
        {
            Console.Write("Enter SARNEG Challenge Word: ");
            challengeWord = Console.ReadLine();
            if (!IsValidSarneg(challengeWord))
            {
                Console.WriteLine("Invalid SARNEG Challenge Code - Please try again.");
            }
        } while (challengeWord == null || !IsValidSarneg(challengeWord));

        return challengeWord;
    }

    public bool IsValidSarneg(string? challengeWord)
    {
        return SARNEG_REGEX().IsMatch(challengeWord ?? "") && !string.IsNullOrWhiteSpace(challengeWord) && challengeWord.Distinct().Count() == challengeWord.Length;
    }

    [GeneratedRegex(@"^[A-Z]{10}$")]
    private static partial Regex SARNEG_REGEX();
}