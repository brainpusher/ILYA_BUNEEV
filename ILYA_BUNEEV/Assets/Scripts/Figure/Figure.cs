using UnityEngine;

public class Figure : MonoBehaviour
{
    [SerializeField] private FigureType figureType;

    public string FigureTypeName => figureType.TypeName;
    
}
