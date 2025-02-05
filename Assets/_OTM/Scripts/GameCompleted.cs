using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompleted : MonoBehaviour
{
    public void EndGame()
    {
        if (ManagerGameData.Instance.isLOL)
        {
            LoLManager.Instance.CompletedGame();
        }
    } 
}
