using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Question : MonoBehaviour
{
    [SerializeField] UnityEvent StartEvents;
    [SerializeField] UnityEvent EndEvents;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartQuestion()
    {
        StartEvents.Invoke();
    }

    public void FinishQuestion()
    {
        EndEvents.Invoke();
    }
}
