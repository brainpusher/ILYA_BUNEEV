using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField] private PopupFiguresGenerator popupFiguresGenerator;
    [SerializeField] private List<GameObject> panelsToActivate;
    [SerializeField] private float timeMovingToConveyor = 5f;
    [SerializeField] private Timer humanWaitingTimer;
    
    private Vector3 _humanDefaultPosition;
    private bool _isMovingToConveyor;
    private HumanController _humanController;
    private HumanPosition _currentHumanPosition;
    private void Awake()
    {
        humanWaitingTimer.OnTimerStop += TimeOut;
    }
    
    private void Start()
    {
        _humanDefaultPosition = transform.position;
    }

    public void MoveToConveyor(HumanController humanController,HumanPosition humanPosition)
    {
        _humanController = humanController;
        _currentHumanPosition = humanPosition;
        _isMovingToConveyor = true;
        StartCoroutine(MoveToPosition(transform,humanPosition.gameObject.transform.position));
    }

    public void MoveBack()
    {
        _isMovingToConveyor = false;
        _currentHumanPosition.AssignHuman(null);
        StartCoroutine(MoveToPosition(transform,_humanDefaultPosition));
    }

    private void TimeOut()
    {
        SetPanelsActive(false);
        _currentHumanPosition.AssignHuman(null);
        _humanController.TimeOuted(this);
    }
    
    private void OnDestroy()
    {
        humanWaitingTimer.OnTimerStop -= TimeOut;
    }
    
    private void SetPanelsActive(bool status)
    {
        foreach (GameObject panelToActivate in panelsToActivate)
        {
            panelToActivate.SetActive(status);
        }
    }
    
    private IEnumerator MoveToPosition (Transform objectToMove, Vector3 end)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.position;
        
        if(!_isMovingToConveyor)
            SetPanelsActive(false);
        
        while (elapsedTime < timeMovingToConveyor)
        {
            transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / timeMovingToConveyor));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = end;

        if (_isMovingToConveyor)
        {
            SetPanelsActive(true);
            _currentHumanPosition.AssignHuman(this);
        }
        else
        {
            gameObject.SetActive(false);
            _currentHumanPosition.IsPositionFree = true;
            _currentHumanPosition.AssignHuman(null);
        }
    }
    
    public List<string> GetRequiredFigureTypes()
    {
        List<string> figureTypes = new List<string>();
        
        foreach (var figureType in popupFiguresGenerator.GetRequiredFigures())
        {
            figureTypes.Add(figureType.TypeName);
        }

        return figureTypes;
    }
}
