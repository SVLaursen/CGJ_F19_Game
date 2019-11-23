using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreboardEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI entryNameText;
    [SerializeField] private TextMeshProUGUI entryScoreText;

    public void Initialize(ScoreboardDataEntry dataEntry)
    {
        entryNameText.text = dataEntry.entryName;
        entryScoreText.text = dataEntry.entryScore.ToString();
    }
}
