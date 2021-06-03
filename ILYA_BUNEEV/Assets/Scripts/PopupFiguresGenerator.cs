using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PopupFiguresGenerator : MonoBehaviour
{
    [SerializeField] private List<FigureType> possibleTypes;
    [SerializeField] private List<Image> availableImages;
    
    private int _possibleTypesMinCount;
    private int _possibleTypesMaxCount;

    private List<FigureType> _generatedFigures;

    private void OnEnable()
    {
        _generatedFigures = new List<FigureType>();
        _possibleTypesMaxCount = possibleTypes.Count;
        _possibleTypesMinCount = 1;

        GenerateRandomFigures();
        AssignAvailableImages();
    }

    private void GenerateRandomFigures()
    {
        //случайно задаем число фигур
        int randomFiguresCount = Random.Range(_possibleTypesMinCount, _possibleTypesMaxCount+1);
        
        //далее случайно выбираем каждый тип фигуры
        for (int i = 0; i < randomFiguresCount; i++)
        {
            int randomFigureTypeIndex = Random.Range(0, _possibleTypesMaxCount);
            _generatedFigures.Add(possibleTypes[randomFigureTypeIndex]);
        }
    }

    private void AssignAvailableImages()
    {
        int i = 0;
        foreach (var generatedFigure in _generatedFigures)
        {
            availableImages[i].sprite = generatedFigure.FigureSprite;
            availableImages[i].gameObject.SetActive(true);
            i++;
        }
    }

    private void OnDisable()
    {
        DisableImages();
    }

    private void DisableImages()
    {
        foreach (var availableImage in availableImages)
        {
            availableImage.gameObject.SetActive(false);
        }
    }

    public List<FigureType> GetRequiredFigures()
    {
        return _generatedFigures;
    }
}
