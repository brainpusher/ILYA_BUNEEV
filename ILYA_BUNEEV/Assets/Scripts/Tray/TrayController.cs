using UnityEngine;

public class TrayController : MonoBehaviour
{
    [SerializeField] private Score score;

    private Tray _currentTray;
    
    private void Start()
    {
        SpawnNewTray();
    }

    public void SpawnNewTray()
    {
        _currentTray = ObjectPooler.SharedInstance.GetPooledObject(0).GetComponent<Tray>();
    }
    
    public void SpawnFigureOnTray(Figure figureToSpawn)
    {
        if (_currentTray == null) return;
        
        TrayPosition trayPosition = _currentTray.GetFreePosition();
        if (trayPosition != null)
        {
            Figure figureInstance = Instantiate(figureToSpawn, trayPosition.transform);
            _currentTray.AddFigureOnTray(figureInstance);
        }
    }

    public void StartTrayMovement()
    {
        if (_currentTray != null)
        {
            if (!_currentTray.IsTrayEmpty())
            {
                _currentTray.StartMovement(this);
            }
        }
    }

    public void TrayRemoved()
    {
        score.RemovePoint();
    }

    public void TrayTaken()
    {
        score.AddPoint();
    }


}
