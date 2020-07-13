using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMoney : MonoBehaviour
{
    public static void EarnMoney(int earnedMoney = 0)
    {
        GameManager.GetInstance().Money += earnedMoney;
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_MONEY, GameManager.GetInstance().Money);
    }

    public static void LoseMoney(int lostMoney)
    {
        GameManager.GetInstance().Money -= lostMoney;
        if(GameManager.GetInstance().Money < 0)
        {
            GameManager.GetInstance().Money = 0;
        }
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_MONEY, GameManager.GetInstance().Money);
    }

}
