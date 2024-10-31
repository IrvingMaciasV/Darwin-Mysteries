using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueSimple : MonoBehaviour
{
    [SerializeField] string id;
    public UnityEvent eventsInit;
    public UnityEvent eventsFinish;
    public float waitTime = 0;

    public string GetString()
    {
        string t= name;
        name = "[PASSED] " + t;
        string s= ManagerGameData.Instance.GetText(id);
        return s;
    }

    public string GetID()
    {
        return id;
    }
}
