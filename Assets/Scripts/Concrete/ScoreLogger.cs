using System;
using System.Collections.Generic;
using System.IO;
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

    public List<string> GetAllScores()
    {
        List<string> scores = new List<string>();

        if (File.Exists(filePath))
        {
            scores.AddRange(File.ReadAllLines(filePath));
        }
        else
        {
            Debug.Log("Aucun fichier de scores trouvé.");
        }

        return scores;
    }
}
