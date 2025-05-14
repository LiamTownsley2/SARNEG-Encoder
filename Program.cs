using ConsoleApp2;
using System;
using System.Text.RegularExpressions;

partial class Program
{
    static void Main()
    {
        Sarneg sarneg_encoder = new();

        string challengeWord = sarneg_encoder.GetValidChallengeWord();
        Console.WriteLine($"SARNEG Codeword Selected: {challengeWord}");
        string mode = GetMode("Would you like to provide your location using: [A] Grid Coordinates [B] Geographic Coordinates (Longitude & Latitude): ");
        string? output = null;
        if(mode.Equals("grid"))
        {
            Dictionary<string, string> grid = GetGridCoordinate("Enter Grid Coordinates (76232 63380): ");
            var additional = grid["additional"]?.ToString();
            var coordinate = sarneg_encoder.Encode(grid["coordinate"], challengeWord);
            output = string.IsNullOrEmpty(additional) ? coordinate : $"{additional}, SARNEG: {coordinate}";
        }
        else if (mode.Equals("geographic"))
        {
            double latitude = GetGeographicCoordinate("Enter Latitude (38.8977): ");
            double longitude = GetGeographicCoordinate("Enter Longitude (77.0365): ");
            output = $"{sarneg_encoder.Encode(latitude.ToString(), challengeWord)} {sarneg_encoder.Encode(longitude.ToString(), challengeWord)}";
        }
        
        if(output is null)
        {
            throw new Exception("There was an error whilst generating this SARNEG code. Please try again.");
        }

        Console.Write(output);
    }

    private static string GetMode(string prompt)
    {
        string? input;
        while (true)
        {
            Console.Write(prompt);
            input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid Input. Please select: [A] Grid Coordinates [B] Geographic Coordinates (Longitude & Latitude)");
            }
            else
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

    private static Dictionary<string, string> GetGridCoordinate(string prompt)
    {
        MGRS military_grid_ref_system = new();
        string? input;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine();

            if (input is not null)
            {
                Dictionary<string, string>? parsed_input = military_grid_ref_system.ParseMGRS(input);
                if (parsed_input is not null && parsed_input.Count > 0)
                {
                    return parsed_input;
                }
            }
            else
            {
                Console.WriteLine($"Invalid input. Please enter a valid coordinate (e.g., 76232 63380).");
            }
        } while (true);
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
}
