using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Timer : MonoBehaviour
{
    public event Action<float> OnChangeTime = delegate {  };
    
    public event Action OnTimerStart = delegate { };
    public event Action OnTimerStop = delegate {  };
    
    [SerializeField] private float minSecondsToCount = 5f;
    [SerializeField] private float maxSecondToCount = 10f;
    [SerializeField] private float tickTime = 1f;
    [SerializeField] private bool isTextTimer;
    
    private Coroutine timerRoutine;
    private float _secondsToCount = 0f;

    private void OnEnable()
    {
        _secondsToCount = Random.Range(minSecondsToCount, maxSecondToCount);
        _secondsToCount = Mathf.Round(_secondsToCount);
        StartTimer();
       // RestartTimer();
    }

    private void OnDisable()
    {
        StopTimer();
    }

    private void StartTimer()
    {
        if (timerRoutine == null)
        {
            timerRoutine = StartCoroutine(isTextTimer ? CountDownText() : CountDownImage());
            OnTimerStart?.Invoke();
        }
    }

    private void StopTimer()
    {
        if (timerRoutine != null)
        {
            StopCoroutine(timerRoutine);
            //OnTimerStop?.Invoke();
            timerRoutine = null;
        }
    }

    /* public void RestartTimer()
    {
        StopTimer();
        StartTimer();
    }*/
    
    private IEnumerator CountDownText()
    {
        float count = _secondsToCount;
        while (count > 0f)
        {
            yield return new WaitForSeconds(tickTime);
            count--;
            OnChangeTime?.Invoke(count);
        }
        OnTimerStop?.Invoke();
    }
    
    private IEnumerator CountDownImage()
    {
        float count = _secondsToCount;
        float step = 1.0f / _secondsToCount;
        while (count > 0f)
        {
            yield return new WaitForSeconds(tickTime);
            count--;
            OnChangeTime?.Invoke(step);
        }
        OnTimerStop?.Invoke();
    }
}
