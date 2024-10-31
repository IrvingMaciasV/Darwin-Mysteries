using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Answer : MonoBehaviour
{
    public int ID;
    [SerializeField] Questionary questionary;


    public delegate void Trigger();
    public static event Trigger CorrectEvent;
    public static event Trigger IncorrectEvent;

    public void OnEnable()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReactCollision(DragAndDrop go)
    {
        Debug.Log("React");
        if (go.ID == ID)
        {
            if (CorrectEvent != null)
            {
                CorrectEvent();
            }
        }

        else
        {
            Debug.Log("Incorrect");
            if (IncorrectEvent != null)
            {
                Debug.Log("Incorrect trigger");
                IncorrectEvent();
            }
        }
    }
}
