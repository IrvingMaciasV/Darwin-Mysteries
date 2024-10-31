using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LangID : MonoBehaviour
{
    public string id;
    //[SerializeField] private ManagerGameData gameData;
    private Text text;
    private TextMeshProUGUI textPro;
    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<Text>();
        textPro = GetComponent<TextMeshProUGUI>();
        ManagerGameData.Instance.TranslateLang+=Translate;
    }

    private void OnEnable()
    {
        Translate();
    }

    private void OnDisable()
    {
        Debug.Log("DISABLED");
        this.enabled = true;
    }

    //public void SendError(string Message, [CallerMemberName] string callerName = "")
    //{
    //    UnityEngine.Debug.Log(callerName + "called me.");
    //}

    private void Translate()
    {
        //if (gameData == null)
        //{
        //    gameData = GameObject.Find("GameSystem").GetComponent<ManagerGameData>();
        //}

        if (text != null)
        {
            text.text = ManagerGameData.Instance.GetText(id);
        }

        if (textPro !=null)
        {
            textPro.text = ManagerGameData.Instance.GetText(id);
        }
    }

    public void SetNewID(string newid)
    {
        id = newid;
        Translate();
    }
}
