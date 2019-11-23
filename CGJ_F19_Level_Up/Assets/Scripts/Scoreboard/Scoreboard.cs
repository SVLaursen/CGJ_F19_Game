using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private int maxEntries = 5;
    [SerializeField] private Transform highscoreHolder;
    [SerializeField] private GameObject entryObject;

    private string SavePath => "Assets/Resources/Highscores.json";
    
    public void AddEntry(ScoreboardDataEntry dataEntry)
    {
        var savedScores = GetSavedScores();
        var scoreAdded = false;
        
        for (var i = 0; i < savedScores.highscores.Count; i++)
        {
            if (dataEntry.entryScore <= savedScores.highscores[i].entryScore) continue;
            savedScores.highscores.Insert(i, dataEntry);
            scoreAdded = true;
            break;
        }

        if (!scoreAdded && savedScores.highscores.Count < maxEntries)
            savedScores.highscores.Add(dataEntry);
        
        if(savedScores.highscores.Count > maxEntries)
            savedScores.highscores.RemoveRange(maxEntries, savedScores.highscores.Count - maxEntries);

        UpdateUI(savedScores);
        SaveScores(savedScores);
    }

    private void UpdateUI(ScoreboardSaveData saveData)
    {
        foreach (Transform child in highscoreHolder)
            Destroy(child.gameObject);

        foreach (var data in saveData.highscores)
            Instantiate(entryObject, highscoreHolder).GetComponent<ScoreboardEntryUI>().Initialize(data);
    }

    private ScoreboardSaveData GetSavedScores()
    {
        Debug.Log("Getting saved scores");
        if (!File.Exists(SavePath))
        {
            File.Create(SavePath).Dispose();
            Debug.Log("Creating new save file...");
            return new ScoreboardSaveData();
        }

        using (var stream = new StreamReader(SavePath))
        {
            var jsonData = stream.ReadToEnd();
            return JsonUtility.FromJson<ScoreboardSaveData>(jsonData);
        }
    }

    private void SaveScores(ScoreboardSaveData saveData)
    {
        using (var stream = new StreamWriter(SavePath))
        {
            var jsonData = JsonUtility.ToJson(saveData, true);
            stream.Write(jsonData);
        }
    }
}
