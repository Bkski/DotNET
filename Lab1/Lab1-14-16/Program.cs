using Lab1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{
    static void Main()
    {
        const string FileName = "highscores.json";
        List<HighScore> highScores;

        if (File.Exists(FileName))
            highScores = JsonSerializer.Deserialize<List<HighScore>>(File.ReadAllText(FileName));
        else
            highScores = new List<HighScore>();

        var rand = new Random();
        var value = rand.Next(1, 101);
        int guess = 0;
        int attempts = 0;

        while (guess != value)
        {
            Console.Write("\nWprowadz wartosc: ");
            guess = Convert.ToInt32(Console.ReadLine());
            attempts++;

            if (guess < value)
                Console.WriteLine("Za malo!");
            else if (guess > value)
                Console.WriteLine("Za duzo!");
            else
            {
                Console.WriteLine($"\nZgadles liczbe {value} w {attempts} probach");
                Console.Write("\nPodaj swoje imię: ");
                string name = Console.ReadLine();
                var hs = new HighScore { Name = name, Trials = attempts };
                highScores.Add(hs);
                File.WriteAllText(FileName, JsonSerializer.Serialize(highScores));
            }
        }

        Console.WriteLine("\nNajlepsze wyniki:");

        highScores.Sort((a, b) => a.Trials.CompareTo(b.Trials));

        foreach (var item in highScores)
        {
            Console.WriteLine($"{item.Name} —- {item.Trials} prób");
        }
    }
}
