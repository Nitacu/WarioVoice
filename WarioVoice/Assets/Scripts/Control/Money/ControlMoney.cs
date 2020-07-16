using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMoney : MonoBehaviour
{
    public static bool _earnedMoney = false ;
    public static int _lastMoney = 0;
    public static Action UpdateMoney;

    public static void EarnMoney(int earnedMoney = 0)
    {
        GameManager.GetInstance().Money += earnedMoney;
        _lastMoney = earnedMoney;
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_MONEY, GameManager.GetInstance().Money);
        _earnedMoney = true;
    }

    public static void LoseMoney(int lostMoney)
    {
        GameManager.GetInstance().Money -= lostMoney;
        if(GameManager.GetInstance().Money < 0)
        {
            GameManager.GetInstance().Money = 0;
        }
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_MONEY, GameManager.GetInstance().Money);

        UpdateMoney.Invoke();
    }

}
