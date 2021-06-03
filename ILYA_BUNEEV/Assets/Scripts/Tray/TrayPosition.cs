using UnityEngine;

public class TrayPosition : MonoBehaviour
{
    private bool _isPositionFree;
    
    public bool IsPositionFree
    {
        get => _isPositionFree;
        set => _isPositionFree = value;
    }

    private void Awake()
    {
        IsPositionFree = true;
    }
}
