using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    [SerializeField] private Score score;
    [SerializeField] private List<HumanPosition> humanPositions;
    
    private Human _currentHuman;

    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        foreach (var humanPosition in humanPositions)
        {
            humanPosition.OnPositionBecameFree += SpawnNextHuman;
        }
    }
    
    private void Unsubscribe()
    {
        foreach (var humanPosition in humanPositions)
        {
            humanPosition.OnPositionBecameFree -= SpawnNextHuman;
        }
    }

    private void SpawnNextHuman(HumanPosition humanPosition)
    {
        _currentHuman =  ObjectPooler.SharedInstance.GetPooledObject(1).GetComponent<Human>();
        StartHumanMovement(humanPosition);
    }
    
    private void StartHumanMovement(HumanPosition humanPosition)
    {
        _currentHuman.MoveToConveyor(this,humanPosition);
    }
    
    public void TimeOuted(Human human)
    {
        score.RemovePoint();
        human.MoveBack();
    }
    
    private void OnDestroy()
    {
        Unsubscribe();
    }
}
