using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIButtonsHandler : MonoBehaviour
{
    [SerializeField] private List<UiButton> uiButtons;
    [SerializeField] private TrayController trayController;
    private void Start()
    {
        Time.timeScale = 1f;
        Subscribe();
    }

    private void Subscribe()
    {
        foreach (var uiButton in uiButtons)
        {
            uiButton.OnFigureButtonClicked += SendSpawnRequest;
            uiButton.OnOkButtonClicked += SendStartTrayRequest;
        }
    }

    private void Unsubscribe()
    {
        foreach (var uiButton in uiButtons)
        {
            uiButton.OnFigureButtonClicked -= SendSpawnRequest;
            uiButton.OnOkButtonClicked -= SendStartTrayRequest;
        }
    }

    private void SendSpawnRequest(Figure figureToSpawn)
    {
        trayController.SpawnFigureOnTray(figureToSpawn);
    }

    private void SendStartTrayRequest()
    {
        trayController.StartTrayMovement();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}
