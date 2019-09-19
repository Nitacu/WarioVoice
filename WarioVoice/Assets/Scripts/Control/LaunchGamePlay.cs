using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchGamePlay : MonoBehaviour
{    

    public void LaunchNextMiniGame(bool gamePassed)
    {
        GameManager.GetInstance().launchNextMinigame(gamePassed);
    }

    public void StartGameplay()
    {
        GameManager.GetInstance().StartGame();
    }

    public void ResetOrder()
    {
        //GameManager.GetInstance().setLevelRound();
    }
}
