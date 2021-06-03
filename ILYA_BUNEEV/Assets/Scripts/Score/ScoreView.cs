using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private Text scoreValueText;

    public void UpdateScore(int score)
    {
        scoreValueText.text = score.ToString();
    }
}
