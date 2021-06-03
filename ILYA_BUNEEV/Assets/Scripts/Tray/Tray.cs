using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour
{
    [SerializeField] private Rigidbody trayRigidbody;
    [SerializeField] private Vector3 trayStartPosition;
    [SerializeField] private List<TrayPosition> trayPositions;
    [SerializeField] [TagSelector] private string tagToDestroyTray;
    [SerializeField] private float timeBeforeRemoveTray = 0.2f;
    [SerializeField] private float timeMovingToConveyor = 0.2f;

    private int _freePositionsCount;
    private List<Figure> _figuresOnTray = new List<Figure>();
    private TrayController _trayController;
    private Vector3 _traySavedPosition;
    
    private void Start()
    {
        _freePositionsCount = trayPositions.Count;
        _traySavedPosition = transform.position;
    }

    public void AddFigureOnTray(Figure figure)
    {
        _figuresOnTray.Add(figure);
    }

    public bool IsTrayEmpty()
    {
        return _figuresOnTray.Count <= 0;
    } 
    
    public TrayPosition GetFreePosition()
    {
        if (_freePositionsCount != 0)
        {
            foreach (var trayPosition in trayPositions)
            {
                if (!trayPosition.IsPositionFree) continue;
                trayPosition.IsPositionFree = false;
                return trayPosition;
            }
        }
        return null;
    }

    public void StartMovement(TrayController trayController)
    {
        _trayController = trayController;
        StartCoroutine(MoveToPosition(transform,trayStartPosition));
    }
    
    
    private IEnumerator MoveToPosition (Transform objectToMove, Vector3 end)
    {
        float elapsedTime = 0;
        trayRigidbody.isKinematic = true;
        Vector3 startingPos = objectToMove.position;
        while (elapsedTime < timeMovingToConveyor)
        {
            transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / timeMovingToConveyor));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = end;
        trayRigidbody.isKinematic = false;
        _trayController.SpawnNewTray();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(tagToDestroyTray))
            StartCoroutine(RemoveObjectFromGame());
    }

    public void TakeTray()
    {
        ClearTrayPositions();
        RemoveFiguresFromTray();
        _trayController.TrayTaken();
        gameObject.SetActive(false);
        transform.position = _traySavedPosition;
    }
    
    private IEnumerator RemoveObjectFromGame()
    {
        yield return new WaitForSeconds(timeBeforeRemoveTray);
        ClearTrayPositions();
        RemoveFiguresFromTray();
        _trayController.TrayRemoved();
        gameObject.SetActive(false);
        transform.rotation = Quaternion.identity;
        transform.position = _traySavedPosition;
    }

    private void ClearTrayPositions()
    {
        foreach (var trayPosition in trayPositions)
        {
            trayPosition.IsPositionFree = true;
        }
        _freePositionsCount = trayPositions.Count;
    }

    private void RemoveFiguresFromTray()
    {
        foreach (var figureOnTray in _figuresOnTray)
        {
            Destroy(figureOnTray.gameObject);
        }
        _figuresOnTray = new List<Figure>();
    }

    public List<string> GetTrayFigureTypes()
    {
        List<string> figureTypes = new List<string>();

        foreach (var figureType in _figuresOnTray)
        {
            figureTypes.Add(figureType.FigureTypeName);
        }

        return figureTypes;
    }
}
