using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.Networking;


public class ManagerGameData : MonoBehaviour
{
    public bool isLOL = true;
    public static ManagerGameData Instance { get; private set; }
    public string language = "en";
    public int level = 0;
    public float musicVolume = 1;
    public AudioSource audio;
    //public List<string> dictionaryGame = new List<string>() { "hola", "como estas" };
    [SerializeField] Dictionary<string, string> dictionaryGame;

    public delegate void Translate();
    public event Translate TranslateLang;

    public  GameData _gameData;

    private bool isReady;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }

        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        _gameData = new GameData();
        InitData();
    }

    private void Update()
    {
        if (!isReady && _gameData != null)
        {
            isReady = true;
        }
    }

    public void InitData()
    {
        if (!isLOL)
        {
#if UNITY_EDITOR
            LoadGameData();
            SetData(_gameData);
#elif UNITY_WEBGL
           
#endif
            GetJson();
        }

        else
        {
            //SetGameData(LoLManager.Instance.GetGameData());
            //SetData(_gameData);
        }
        
    }


    public void SaveGameData()
    {
#if UNITY_EDITOR
        string filePath = Path.Combine(Application.streamingAssetsPath, "GameData.json");
        string json = JsonUtility.ToJson(_gameData);

        File.WriteAllText(filePath, json);
#endif
        if (isLOL)
        {
            LoLManager.Instance.SetGameData(_gameData);
        }
    }

    public GameData LoadGameData()
    {
        Debug.Log("Load");
        string filePath = Path.Combine(Application.streamingAssetsPath, "GameData.json");
        string json = File.ReadAllText(filePath);

        _gameData = JsonUtility.FromJson<GameData>(json);
        
        return _gameData;
    }

    private void SetData(GameData gd)
    {
        language = gd.language;
        level = gd.levels;
        musicVolume = gd.musicVolume;
        SetDictionary();
        TranslateGame();
    }

    private void InsertData()
    {
        _gameData.language = language;
        _gameData.levels = level;
        _gameData.musicVolume = musicVolume;

        LoLManager.Instance.SetGameData(_gameData);
        
    }

    private void SetDictionary()
    {
        Debug.Log("Set Dictionary");
        dictionaryGame = new Dictionary<string, string>();
        for (int i = 0; i < _gameData.dictionaryGame.Count; i += 4)
        {
            dictionaryGame.Add(_gameData.dictionaryGame[i], _gameData.dictionaryGame[i + 1]);
            dictionaryGame.Add(_gameData.dictionaryGame[i + 2], _gameData.dictionaryGame[i + 3]);
        }
        Debug.Log("Dictionary: " + dictionaryGame);
    }

    public void TranslateGame()
    {
        if (TranslateLang != null)
            TranslateLang();
    }


    public string GetText(string id)
    {
        if (!isLOL)
        {

            string tempL = "";
            if (language == "en") {
                tempL = "Eng";
            }
            else if (language == "es"){
                tempL = "Esp";
            }
            id += "-" + tempL;

            foreach (KeyValuePair<string, string> kvp in dictionaryGame)
            {
                if (kvp.Key == id)
                {
                    return kvp.Value;
                }
            }

            return "";
        }
        else
        {
            return LoLManager.Instance.GetText(id);
        }
    }

    public void SetLanguage(string s)
    {
        language = s;
        InsertData();
        SaveGameData();
        TranslateGame();
    }

    public void SetLevel(int i)
    {
        if (i > _gameData.levels)
        {
            Debug.Log("Level Accepted");
            level = i;
            InsertData();
            SaveGameData();
        }
    }

    public bool ManagerReady()
    {
        return isReady;
    }

    public void SetAudioVolume(float f)
    {
        audio.volume = f;
        musicVolume = f;
        InsertData();
        SaveGameData();
    }

    public void SetGameData(GameData gd)
    {
        _gameData = gd;

        //if (isLOL)
        //{
        //    LoLManager.Instance.SetGameData(_gameData);
        //}
    }

    public void SetGameDataLoL(GameData gd)
    {
        _gameData = gd;
    }

    public void NewGame()
    {
        _gameData.NewGameData();

        if (isLOL)
        {
            LoLManager.Instance.gameDataLOL = _gameData;
        }

        SaveGameData();
    }

    JSONNode itemsData;
    void GetJson()
    {
        DictionaryStrings ds = new DictionaryStrings();
        level = 2;
        language = "en";
        musicVolume = 100;
        _gameData.dictionaryGame = ds.dictionary;
        InsertData();
        SetDictionary();
        TranslateGame();
    }



    private void DeserializePages()
    {
        
    }
}
