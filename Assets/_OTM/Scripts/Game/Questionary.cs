using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Questionary : MonoBehaviour
{
    [Header("Data")]
    public int actualLevel;
    public int initProgress;
    public int[] answerID;
    [SerializeField] int life;
    public float waitTime;

    [Header ("Question")]
    [SerializeField] GameObject[] Questions;
    int it;

    [Header ("Conditions")]
    [SerializeField] GameObject victoryGO;
    [SerializeField] GameObject gameoverGO;

    [Header("Events")]
    [SerializeField] UnityEvent StartEvents;

    public void OnEnable()
    {
        Answer.CorrectEvent += CorrectAnswer;
        Answer.IncorrectEvent += IncorrectAnswer;
    }

    // Start is called before the first frame update
    void Start()
    {
        it = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartQuestionary()
    {
        StartEvents.Invoke();
        StartCoroutine(WaitStarting());
    }

    private IEnumerator WaitStarting()
    {
        yield return new WaitForSeconds(waitTime);
        Questions[0].SetActive(true);
    }

    public void CorrectAnswer()
    {
        Debug.Log("Correct Answer");
        initProgress++;
        LoLManager.Instance.SaveProgress(initProgress);

        NextQuestion();
    }

    public void IncorrectAnswer()
    {
        Debug.Log("Incorrect Answer");
    }

    public void NextQuestion()
    {


        Questions[it].SetActive(false);
        it++;
        if (it < Questions.Length)
        {
            Questions[it].SetActive(true);
        }


        if (ManagerGameData.Instance.isLOL)
        {
            initProgress++;
            LoLManager.Instance.SaveProgress(initProgress);
        }
        //else
        //{
        //    victoryGO.SetActive(true);
        //}
    }

    public void GameOver()
    {
        gameoverGO.SetActive(true);
    }


    public void LevelCompleted()
    {
        ManagerGameData.Instance.SetLevel(actualLevel);
    }
}
