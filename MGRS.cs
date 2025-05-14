using System.Text.RegularExpressions;

namespace ConsoleApp2
{
    partial class MGRS()
    {
        public Dictionary<string, string> ParseMGRS(string input)
        {
            var match = MGRS_REGEX().Match(input);
            if (match.Success)
            {
                var GZD = match.Groups[1].Value; // Grid Zone Designator (45V)
                var UC = match.Groups[2].Value; // 100,000m square identifier (UC) 
                var Easting = match.Groups[3].Value; // Easting (76232)
                var Northing = match.Groups[4].Value; // Northing (63380)

                var result = new Dictionary<string, string>
            {
                { "coordinate", $"{Easting} {Northing}" },
                { "additional", $"{GZD} {UC}" }
            };

                return result;
            }
            else
            {
                throw new Exception("Invalid MGRS Input");
            }
        }


        [GeneratedRegex(@"^(\d{2}[A-Z])?\s?([A-Z]{2})?\s?(\d{5})\s(\d{5})$")]
        private static partial Regex MGRS_REGEX(); // Military Grid Reference System
    }
}
