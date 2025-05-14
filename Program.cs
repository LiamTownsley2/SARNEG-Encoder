using System;
using System.Text.RegularExpressions;

partial class Program
{
    static void Main()
    {
        string challengeWord = GetValidChallengeWord();
        Console.WriteLine($"SARNEG Codeword Selected: {challengeWord})");
        SarnegEncoder sarneg_encoder = new(challengeWord);
        string mode = GetMode("Would you like to provide your location using: [A] Grid Coordinates [B] Geographic Coordinates (Longitude & Latitude): ");
        string? output = null;
        if(mode.Equals("grid"))
        {
            Dictionary<string, string> grid = GetGridCoordinate("Enter Grid Coordinates (76232 63380): ");
            var additional = grid["additional"]?.ToString();
            var coordinate = sarneg_encoder.Encode(grid["coordinate"]);
            output = string.IsNullOrEmpty(additional) ? coordinate : $"{additional}, SARNEG: {coordinate}";
        }
        else if (mode.Equals("geographic"))
        {
            double latitude = GetGeographicCoordinate("Enter Latitude (38.8977): ");
            double longitude = GetGeographicCoordinate("Enter Longitude (77.0365): ");
            output = $"{sarneg_encoder.Encode(latitude.ToString())} {sarneg_encoder.Encode(longitude.ToString())}";
        }
        
        if(output is null)
        {
            throw new Exception("There was an error whilst generating this SARNEG code. Please try again.");
        }

        Console.Write(output);
    }

    private static double GetGeographicCoordinate(string prompt)
    {
        string? input;

        while (true)
        {
            Console.Write(prompt);
            input = Console.ReadLine();

            if (double.TryParse(input, out double coordinate))
            {
                return coordinate;
            }
            else
            {
                Console.WriteLine($"Invalid input. Please enter a valid coordinate (e.g., 38.8977).");
            }
        }
    }
    private static Dictionary<string, string> GetGridCoordinate(string prompt)
    {
        string? input;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine();

            if(input is not null)
            {
                Dictionary<string, string>? parsed_input = ParseMGRS(input);
                if(parsed_input is not null && parsed_input.Count > 0)
                {
                    return parsed_input;
                }
            } else
            {
                Console.WriteLine($"Invalid input. Please enter a valid coordinate (e.g., 76232 63380).");
            }
        } while (true);
    }

    private static string GetMode(string prompt)
    {
        string? input;
        while (true)
        {
            Console.Write(prompt);
            input = Console.ReadLine();

            if(string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid Input. Please select: [A] Grid Coordinates [B] Geographic Coordinates (Longitude & Latitude)");
            } else
            {
                switch (input.ToLower())
                {
                    case "a":
                        return "grid";
                    case "b":
                        return "geographic";
                }
            }
        }
    }

    private static string GetValidChallengeWord()
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

    private static Dictionary<string, string> ParseMGRS(string input)
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

    private static bool IsValidSarneg(string? challengeWord)
    { 
        return SARNEG_REGEX().IsMatch(challengeWord ?? "") && !string.IsNullOrWhiteSpace(challengeWord) && challengeWord.Distinct().Count() == challengeWord.Length;
    }

    [GeneratedRegex(@"^[A-Z]{10}$")]
    private static partial Regex SARNEG_REGEX();

    [GeneratedRegex(@"^(\d{2}[A-Z])?\s?([A-Z]{2})?\s?(\d{5})\s(\d{5})$")]
    private static partial Regex MGRS_REGEX(); // Military Grid Reference System
}
