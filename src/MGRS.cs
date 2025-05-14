using System.Text.RegularExpressions;

namespace ConsoleApp2
{
    partial class MGRS()
    {
        public Dictionary<string, string?> ParseMGRS(string input)
        {
            var match = MGRS_REGEX().Match(input);
            if (match.Success)
            {
                string? additional = null;
                string? GZD = match.Groups[1].Value; // Grid Zone Designator (45V)
                if (!string.IsNullOrWhiteSpace(GZD)) additional = GZD;
                string? UC = match.Groups[2].Value; // 100,000m square identifier (UC) 
                if (!string.IsNullOrWhiteSpace(UC)) additional = additional + $" {UC}";
                string? Easting = match.Groups[3].Value; // Easting (76232)
                string? Northing = match.Groups[4].Value; // Northing (63380)
                
                if(string.IsNullOrWhiteSpace(Easting) || string.IsNullOrWhiteSpace(Northing))
                {
                    throw new Exception("Invalid MGRS Provided");
                }

                Dictionary<string, string> result = new();

                result.Add("coordinate", $"{Easting} {Northing}");
                if (!string.IsNullOrWhiteSpace(additional)) {
                    result.Add("additional", additional);
                }
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
