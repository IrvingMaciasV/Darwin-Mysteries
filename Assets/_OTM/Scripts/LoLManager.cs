using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;
using SimpleJSON;

public class LoLManager : MonoBehaviour
{
    public static LoLManager Instance { get; private set; }
    public string language;
    string s="hola";
    JSONNode _langNode;
    string _langCode;
    public GameData gameDataLOL;
    public int progressMax;
    Coroutine _feedbackMethod;
    [SerializeField] Button continueButton, newGameButton;
    bool _init;

    [SerializeField] PlayEvent events;

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

    void Start()
    {

        // Create the WebGL (or mock) object
        // This will all change in SDK V6 to be simplified and streamlined.
#if UNITY_EDITOR
        ILOLSDK sdk = new LoLSDK.MockWebGL();
#elif UNITY_WEBGL
            ILOLSDK sdk = new LoLSDK.WebGL();
#elif UNITY_IOS || UNITY_ANDROID
            ILOLSDK sdk = null; // TODO COMING SOON IN V6
#endif

        LOLSDK.Init(sdk, "com.legends-of-learning.unity.sdk.v5.3.Init MM");

        // Register event handlers
        LOLSDK.Instance.StartGameReceived += new StartGameReceivedHandler(StartGame);
        LOLSDK.Instance.GameStateChanged += new GameStateChangedHandler(gameState => Debug.Log(gameState));
        LOLSDK.Instance.QuestionsReceived += new QuestionListReceivedHandler(questionList => Debug.Log(questionList));
        LOLSDK.Instance.LanguageDefsReceived += new LanguageDefsReceivedHandler(LanguageUpdate);

        // Used for player feedback. Not required by SDK.
        LOLSDK.Instance.SaveResultReceived += OnSaveResult;

        // Call GameIsReady before calling LoadState or using the helper method.
        LOLSDK.Instance.GameIsReady();

#if UNITY_EDITOR
        UnityEditor.EditorGUIUtility.PingObject(this);
        LoadMockData();
#endif

        // Helper method to hide and show the state buttons as needed.
        // Will call LoadState<T> for you.
        if (ManagerGameData.Instance.isLOL)
        {
            Helper.StateButtonInitialize<GameData>(newGameButton, continueButton, OnLoad);
        }
    }

    public void SetButtons(Button continuebtn, Button newbtn)
    {
        if (continueButton ==null || newGameButton == null)
        {
            Debug.Log("SET");
            continueButton = continuebtn;
            newGameButton = newbtn;
            Helper.StateButtonInitialize<GameData>(newGameButton, continueButton, OnLoad);
            //continueButton.onClick.AddListener(ButtonsEvents);
            //newGameButton.onClick.AddListener(ButtonsEvents);
        }
    }

    private void ButtonsEvents()
    {
        events.WaitEvent();
    }

    private void OnDestroy()
    {
#if UNITY_EDITOR
        if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
            return;
#endif
        LOLSDK.Instance.SaveResultReceived -= OnSaveResult;
    }

    void Save()
    {
        if (ManagerGameData.Instance.isLOL)
        {
            LOLSDK.Instance.SaveState(gameDataLOL);
            //LOLSDK.Instance.LanguageDefsReceived();
        }
    }

    public void SaveProgress(int p)
    {
        if (p > gameDataLOL.progress)
            gameDataLOL.progress = p;

        if (gameDataLOL.progress <= progressMax)
        {
            LOLSDK.Instance.SubmitProgress(0, gameDataLOL.progress, progressMax);
            Save();
        }
        
    }
    public void CompletedGame()
    {
        LOLSDK.Instance.CompleteGame();
    }


    void OnSaveResult(bool success)
    {
        if (!success)
        {
            Debug.LogWarning("Saving not successful");
            return;
        }

        //if (_feedbackMethod != null)
        //    StopCoroutine(_feedbackMethod);
        // ...Auto Saving Complete
        //_feedbackMethod = StartCoroutine(_Feedback(GetText("autoSave")));
    }

    void StartGame(string startGameJSON)
    {
        if (string.IsNullOrEmpty(startGameJSON))
            return;

        JSONNode startGamePayload = JSON.Parse(startGameJSON);
        // Capture the language code from the start payload. Use this to switch fonts
        _langCode = startGamePayload["languageCode"];
    }

    void LanguageUpdate(string langJSON)
    {
        if (string.IsNullOrEmpty(langJSON))
            return;

        _langNode = JSON.Parse(langJSON);

        TextDisplayUpdate();
    }

    public void SpeakText(string K)
    {
        LOLSDK.Instance.SpeakText(K);
    }

    public string GetText(string key)
    {
        string value = _langNode?[key];
        return value ?? "--missing--";
    }

    // This could be done in a component with a listener attached to an lang change
    // instead of coupling all the text to a controller class.
    void TextDisplayUpdate()
    {
        ManagerGameData.Instance.TranslateGame();
    }

    /// <summary>
    /// This is the setting of your initial state when the scene loads.
    /// The state can be set from your default editor settings or from the
    /// users saved data after a valid save is called.
    /// </summary>
    /// <param name="loadedCookingData"></param>
    void OnLoad(GameData loadedCookingData)
    {
        // Overrides serialized state data or continues with editor serialized values.
        if (loadedCookingData != null)
        {
            Debug.Log("LOAD");
            gameDataLOL = loadedCookingData;
        }

        if (ManagerGameData.Instance.isLOL)
        {
            ManagerGameData.Instance.SetGameData(gameDataLOL);
        }

        // Initially set the text displays since the lang node should be populated.
        TextDisplayUpdate();

        // I use an init flag so I can call the same Set methods during initial load and during gameplay.
        // You don't have to follow this pattern, you can have init methods and gameplay methods separated.
        _init = true;
    }

    public void SetGameData(GameData gd)
    {
        gameDataLOL = gd;
        Save();
    }

    public GameData GetGameData()
    {
        return gameDataLOL;
    }

#if UNITY_EDITOR
    // Loading Mock Gameframe data
    // This will all be changed and streamlined in SDK V6
    private void LoadMockData()
    {
        // Load Dev Language File from StreamingAssets

        string startDataFilePath = Path.Combine(Application.streamingAssetsPath, "startGame.json");

        if (File.Exists(startDataFilePath))
        {
            string startDataAsJSON = File.ReadAllText(startDataFilePath);
            StartGame(startDataAsJSON);
        }

        // Load Dev Language File from StreamingAssets
        string langFilePath = Path.Combine(Application.streamingAssetsPath, "language.json");
        if (File.Exists(langFilePath))
        {
            string langDataAsJson = File.ReadAllText(langFilePath);
            var lang = JSON.Parse(langDataAsJson)[_langCode];
            LanguageUpdate(lang.ToString());
        }
    }
#endif
}
