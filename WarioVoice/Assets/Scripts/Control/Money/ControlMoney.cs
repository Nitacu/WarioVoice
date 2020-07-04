using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMoney : MonoBehaviour
{
    public static void EarnMoney(int earnedMoney = 10)
    {
        GameManager.GetInstance().Money += earnedMoney;
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_MONEY, GameManager.GetInstance().Money);
    }

    public static void LoseMoney(int lostMoney)
    {
        GameManager.GetInstance().Money -= lostMoney;
        PlayerPrefs.SetInt(PlayerPrefsKeys.KEY_MONEY, GameManager.GetInstance().Money);
    }

}
