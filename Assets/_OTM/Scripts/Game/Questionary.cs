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

    private bool isActive = false;

    [Header ("Question")]
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
        isActive = true;
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
        incorrectAnswer.SetActive(true);
        life--;
        if (life <= 0)
        {
            gameoverGO.SetActive(true);
        }
    }

    public void NextQuestion()
    {
        if (isActive)
        {
            if (ManagerGameData.Instance.isLOL)
            {
                initProgress++;
                LoLManager.Instance.SaveProgress(initProgress);
            }

            Questions[it].SetActive(false);
            it++;
            if (it < Questions.Length)
            {
                Questions[it].SetActive(true);
            }

            else
            {
                EndEvents.Invoke();
                isActive = false;
            }
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
