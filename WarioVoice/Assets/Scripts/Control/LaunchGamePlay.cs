using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchGamePlay : MonoBehaviour
{    

    public void LaunchNextMiniGame(bool gamePassed)
    {
        GameManager.GetInstance().launchMinigame(gamePassed);
    }

    public void StartGameplay()
    {
        GameManager.GetInstance().startGame();
    }

    public void ResetOrder()
    {
        GameManager.GetInstance().setLevelRound();
    }
}
