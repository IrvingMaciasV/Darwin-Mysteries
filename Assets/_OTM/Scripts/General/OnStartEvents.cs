using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnStartEvents : MonoBehaviour
{
    [SerializeField] UnityEvent startEvents;
    [SerializeField] UnityEvent enableEvents;
    // Start is called before the first frame update
    void Start()
    {
        startEvents.Invoke();
    }

    private void OnEnable()
    {
        enableEvents.Invoke();
    }
}
