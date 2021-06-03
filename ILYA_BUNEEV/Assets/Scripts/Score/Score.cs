using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private int defaultScorePoints = 0;
    [SerializeField] private List<ScoreView> scoreViews;
    
    private int _currentScore;
    
    private void Start()
    {
        _currentScore = defaultScorePoints;
        UpdateScoreView(_currentScore);
    }


    public void AddPoint()
    {
        _currentScore++;
        UpdateScoreView(_currentScore);
    }

    public void RemovePoint()
    {
        _currentScore--;
        UpdateScoreView(_currentScore);
    }

    private void UpdateScoreView(int score)
    {
        foreach (var scoreView in scoreViews)
        {
            scoreView.UpdateScore(score);
        }
    }
} 
