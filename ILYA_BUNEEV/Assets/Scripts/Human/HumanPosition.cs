using System;
using UnityEngine;

public class HumanPosition : MonoBehaviour
{
    public event Action<HumanPosition> OnPositionBecameFree = delegate {  };
    
    private bool _isPositionFree;
    private Human _currentHumanOnPosition = null;
    
    public bool IsPositionFree
    {
        get => _isPositionFree;
        set => _isPositionFree = value;
    }

    private void Awake()
    {
        IsPositionFree = true;
    }

    private void Update()
    {
        if (IsPositionFree)
        {
            OnPositionBecameFree?.Invoke(this);
            IsPositionFree = false;
        }
    }

    public void AssignHuman(Human human)
    {
        _currentHumanOnPosition = human;
    }

    public Human GetHumanOnPosition()
    {
        return _currentHumanOnPosition;
    }
    
}
