using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


partial class SarnegEncoder(string key)
{
    private readonly Dictionary<char, char> encodeMap = key
        .Select((ch, i) => new { ch, i })
        .ToDictionary(x => (char)('0' + x.i), x => x.ch);

    public string Encode(string input)
    {
        return new string(input.Select(ch => encodeMap.TryGetValue(ch, out var decoded) ? decoded : ch).ToArray());
    }

}