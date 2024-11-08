using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Button continueButton;
    [SerializeField] Button newButton;

    // Start is called before the first frame update
    void Start()
    {
        if (ManagerGameData.Instance.isLOL)
        {
            LoLManager.Instance.SetButtons(continueButton, newButton);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        ManagerGameData.Instance.NewGame();
    }
}
