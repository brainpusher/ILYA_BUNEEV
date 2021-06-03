using UnityEngine;

[CreateAssetMenu(fileName = "New Figure Type", menuName = "FigureType")]
public class FigureType : ScriptableObject
{
    [SerializeField] private string typeName;
    [SerializeField] private Sprite figureSprite;
    
    /// <summary>
    /// Возвращает название типа
    /// </summary>
    public string TypeName => typeName;

    public Sprite FigureSprite => figureSprite;
}
