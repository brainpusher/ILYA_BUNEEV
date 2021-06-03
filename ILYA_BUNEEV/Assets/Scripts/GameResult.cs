using UnityEngine;

public class GameResult : MonoBehaviour
{
    [SerializeField] private Timer gameTimer;
    [SerializeField] private GameResultView gameResultView;

    private void OnEnable()
    {
        gameTimer.OnTimerStop += ShowResult;
    }

    private void OnDisable()
    {
        gameTimer.OnTimerStop -= ShowResult;
    }

    private void ShowResult()
    {
        Time.timeScale = 0f;
        gameResultView.ShowGameResult();
    }
}
