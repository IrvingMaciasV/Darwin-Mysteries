using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayEvent : MonoBehaviour
{
    [SerializeField] bool StartOnEnable = false;
    [SerializeField] float timeWait;
    [SerializeField] UnityEvent StartEvents;
    [SerializeField] UnityEvent FinishEvents;

    private void Start()
    {
        if (StartOnEnable)
        {
            WaitEvent();
        }
    }

    public void WaitEvent()
    {
        StartEvents.Invoke();
        StartCoroutine(WaitAndPlay());
    }

    IEnumerator WaitAndPlay()
    {
        yield return new WaitForSeconds(timeWait);
        InvokeEvents();
    }

    public void InvokeEvents()
    {
        FinishEvents.Invoke();
    }
}
