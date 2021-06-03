using System;
using UnityEngine;
using UnityEngine.UI;

public class UiButton : MonoBehaviour
{
    public event Action<Figure> OnFigureButtonClicked = delegate {  };
    public event Action OnOkButtonClicked = delegate {  };

    [SerializeField] private Figure figure;
    [SerializeField] private Button button;

    [SerializeField] private bool isOkButton;

    private void Awake()
    {
        if(!isOkButton)
            button.onClick.AddListener(CurrentFigureClicked);
        else
            button.onClick.AddListener(OkButtonClicked);
    }

    private void CurrentFigureClicked()
    {
        OnFigureButtonClicked?.Invoke(figure);
    }

    private void OkButtonClicked()
    {
        OnOkButtonClicked?.Invoke();
    }
}
