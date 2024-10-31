using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using LoLSDK;

public class Dialogue : MonoBehaviour
{
    public bool initOnStart;

    [Header ( "Components")]
    [SerializeField] Text text;
    [SerializeField] TextMeshProUGUI textPro;
    [SerializeField] Button button;

    [Space(20)]
    [Header("Values")]
    [SerializeField] float waitTime;
    [SerializeField] float textSpeed;
    [SerializeField] DialogueSimple[] lines;
    [SerializeField] string actualString;
    private bool canSkip = false;
    private bool isPlaying=false;

    [Space (20)]
    [Header ("Events")]
    [SerializeField] UnityEvent initEvents;
    [SerializeField] UnityEvent endingEvents;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        if(text == null) text = GetComponent<Text>();
        if (textPro ==null) textPro = GetComponent<TextMeshProUGUI>();
        
        if (initOnStart)
        {
            initEvents.Invoke();
            StartCoroutine(WaitStart());
        }
    }

    private void Update()
    {

    }

    public void TriggerStart()
    {
        initEvents.Invoke();
        StartCoroutine(WaitStart());
    }

    public IEnumerator WaitStart()
    {
        yield return new WaitForSeconds(waitTime);

        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnClickNextLine);
            Debug.Log("Button Added");
        }

        isPlaying = true;
        StartDialogue();
    }

    public void OnClickNextLine()
    {
        Debug.Log("CLICK");
        if (canSkip)
        {

            if (text != null && text.text == actualString)
            {
                NextLine();
            }

            else if (textPro != null && textPro.text == actualString)
            {
                NextLine();
            }

            else
            {
                StopAllCoroutines();
                if (text != null)
                    text.text = actualString;
                else
                    textPro.text = actualString;
            }
        }
    }

    public void StartDialogue()
    {
        canSkip = false;
        index = 0;
        if (text != null) text.text = "";
        if (textPro != null) textPro.text = "";
        actualString = lines[0].GetString();
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        lines[index].eventsInit.Invoke();

        yield return new WaitForSeconds(lines[index].waitTime);
        LoLManager.Instance.SpeakText(lines[index].GetID());
        canSkip = true;

        foreach (char c in actualString.ToCharArray())
        {
            yield return new WaitForSeconds(textSpeed);

            if (text != null)
            {
                text.text += c;
            }

            else if (textPro != null)
            {
                textPro.text += c;
            }
        }

    }

    private void NextLine()
    {
        if(index < lines.Length - 1)
        {
            //LOLSDK.Instance.StopSound();
            canSkip = false;
            lines[index].eventsFinish.Invoke();
            index++;
            if (text != null) text.text = "";
            if (textPro != null) textPro.text = "";
            actualString = lines[index].GetString();
            StartCoroutine(TypeLine());
        }

        else
        {
            canSkip = false;
            endingEvents.Invoke();
        }
    }

    public void EndDialogue()
    {
        if (canSkip)
        {
            canSkip = false;
            endingEvents.Invoke();
        }
    }
}
