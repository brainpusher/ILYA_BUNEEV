using UnityEngine;
using UnityEngine.UI;

public class FillableImageTimerView : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private Image timerImage;
    
    private void Awake()
    {
        timer.OnChangeTime += UpdateTimerImage;
        timer.OnTimerStart += ResetImageFill;
    }

    private void UpdateTimerImage(float timeInSeconds)
    {
        timerImage.fillAmount -= timeInSeconds;
    }

    private void ResetImageFill()
    {
        timerImage.fillAmount = 1f;
    }
    
    private void OnDestroy()
    {
        timer.OnChangeTime -= UpdateTimerImage;
        timer.OnTimerStart -= ResetImageFill;
    }
}
