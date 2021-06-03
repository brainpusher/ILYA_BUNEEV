using UnityEngine;

public class GameResultView : MonoBehaviour
{
    [SerializeField] private GameObject gameResultObject;

    public void ShowGameResult()
    {
        gameResultObject.SetActive(true);
    }
}
