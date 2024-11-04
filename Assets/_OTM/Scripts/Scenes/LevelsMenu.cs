using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsMenu : MonoBehaviour
{
    [SerializeField] GameObject[] levels;

    private void Start()
    {
        ActiveLevels();
    }

    public void ActiveLevels()
    {
        int lvl = 0;
        if (ManagerGameData.Instance.isLOL)
        {
            lvl = ManagerGameData.Instance._gameData.levels;
        }

        else
        {
            lvl = LoLManager.Instance.gameDataLOL.levels;
        }
        Debug.Log(lvl);

        for (int i=0; i<=lvl; i++)
        {
            levels[i].SetActive(true);
        }

    }

    public void Save()
    {
        ManagerGameData.Instance.SaveGameData();
    }
}
