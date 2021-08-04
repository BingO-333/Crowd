using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class BestScoreVIew : MonoBehaviour
{
    private Text _bestScore;
    
    private void Awake()
    {
        _bestScore = GetComponent<Text>();

        _bestScore.text = $"Best x{PlayerPrefs.GetInt("BestScore", 0)}";
    }
}
