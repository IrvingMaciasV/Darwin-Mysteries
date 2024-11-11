using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Answer : MonoBehaviour
{
    public int ID;
    [SerializeField] bool stayAnswer =false;
    [SerializeField] Questionary questionary;

    private RectTransform rt;

    public delegate void Trigger();
    public static event Trigger CorrectEvent;
    public static event Trigger IncorrectEvent;

    public void OnEnable()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        rt = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReactCollision(DragAndDrop go)
    {
        Debug.Log(name + " React " + go.name);
        if (go.ID == ID)
        {
            if (CorrectEvent != null)
            {
                CorrectEvent();
                if (stayAnswer)
                {
                    StayAnswer(go);
                }
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

    public void StayAnswer(DragAndDrop obj)
    {
        obj.StayInAnswerPosition(rt);
        gameObject.SetActive(false);
    }
}
