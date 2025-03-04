using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ScoreLogger : I_Logger
{
    private string filePath;

    public ScoreLogger()
    {
        filePath = Path.Combine(Directory.GetCurrentDirectory(), "scores.txt");
    }

    public void log(string pseudoAndScore)
    {
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine(pseudoAndScore);
        }

        Debug.Log($"Données enregistrees : {pseudoAndScore}");
    }

    public List<(string, string)> getAllScores()
    {
        List<(string, string)> scores = new List<(string, string)>();

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(':', 2);
                if (parts.Length == 2)
                {
                    scores.Add((parts[0], parts[1]));
                }
                else
                {
                    Debug.Log($"Ligne invalide ignorée : {line}");
                }
            }
        }
        else
        {
            Debug.Log("Aucun fichier de scores trouvé.");
        }

        return scores
            .OrderByDescending(entry => int.TryParse(entry.Item2, out int level) ? level : int.MinValue)
            .ToList();
    }
}
