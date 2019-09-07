using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancerController : MonoBehaviour
{
    private const string BLUEDANCE = "Running";
    private const string GREENDANCE = "SecondDance";
    private const string YELLOWDANCE = "ThirdDance";
    private const string PINKDANCE = "FourDance";
    private const string ORANGEDANCE = "FifthDance";
    private const string BROWNDANCE = "SixthDance";
    private const string LIMEDANCE = "seventhDance";
    private const string SILVERDANCE = "eigthDance";
    private const string MAGENTADANCE = "nineDance";
    private const string WHITEDANCE = "TenthDance";
    private const string TURQUOISEDANCE = "TURQUOISE";
    private const string BLACKDANCE = "BLACK";
    private const string INDIGODANCE = "INDIGO";
    private const string REDDANCE = "RED";
    private const string FUCHSIADANCE = "FUCHSIA";



    public void dancePlayer(Crystal crystal)
    {
        GetComponent<Animator>().Play(crystal.danceClip.name);
    }
}
