using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestionaryTimeline : MonoBehaviour
{
    [Header("Data")]
    public int actualLevel;
    public int initProgress;
    public int[] answerID;
    [SerializeField] int life;
    public float waitTime;


    [Header("Question")]
    [SerializeField] GameObject[] Questions;
    int it;

    [Header("Conditions")]
    [SerializeField] GameObject incorrectAnswer;
    [SerializeField] GameObject victoryGO;
    [SerializeField] GameObject gameoverGO;

    [Header("Events")]
    [SerializeField] UnityEvent StartEvents;
    [SerializeField] UnityEvent EndEvents;

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
        Debug.Log("Correct Answer TL");
        initProgress++;
        LoLManager.Instance.SaveProgress(initProgress);

        it++;
        if (it == Questions.Length)
        {
            NextQuestion();
        }
    }

    public void IncorrectAnswer()
    {
        Debug.Log("Incorrect Answer TL");
        incorrectAnswer.SetActive(true);
        life--;
        if (life <= 0)
        {
            gameoverGO.SetActive(true);
        }
    }

    public void NextQuestion()
    {
        if (ManagerGameData.Instance.isLOL)
        {
            initProgress++;
            LoLManager.Instance.SaveProgress(initProgress);
        }

        if (it >= Questions.Length)
        {
            EndEvents.Invoke();
        }
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
