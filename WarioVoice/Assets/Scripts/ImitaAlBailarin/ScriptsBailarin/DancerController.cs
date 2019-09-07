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



    public void dancePlayer(CrystalController.Colors crystal)
    {
        switch (crystal)
        {
            case CrystalController.Colors.BLUE:
                GetComponent<Animator>().Play(Animator.StringToHash(BLUEDANCE));
                break;
            case CrystalController.Colors.GREEN:
                GetComponent<Animator>().Play(Animator.StringToHash(GREENDANCE));
                break;
            case CrystalController.Colors.YELLOW:              
                GetComponent<Animator>().Play(Animator.StringToHash(YELLOWDANCE));
                break;
            case CrystalController.Colors.PINK:             
                GetComponent<Animator>().Play(Animator.StringToHash(PINKDANCE));
                break;
            case CrystalController.Colors.ORANGE:
                GetComponent<Animator>().Play(Animator.StringToHash(ORANGEDANCE));
                break;
            case CrystalController.Colors.BROWN:             
                GetComponent<Animator>().Play(Animator.StringToHash(BROWNDANCE));
                break;
            case CrystalController.Colors.LIME:      
                GetComponent<Animator>().Play(Animator.StringToHash(LIMEDANCE));
                break;
            case CrystalController.Colors.SILVER:      
                GetComponent<Animator>().Play(Animator.StringToHash(SILVERDANCE));
                break;
            case CrystalController.Colors.MAGENTA:             
                GetComponent<Animator>().Play(Animator.StringToHash(MAGENTADANCE));
                break;
            case CrystalController.Colors.WHITE:    
                GetComponent<Animator>().Play(Animator.StringToHash(WHITEDANCE));
                break;
            case CrystalController.Colors.TURQUOISE:
                GetComponent<Animator>().Play(Animator.StringToHash(TURQUOISEDANCE));
                break;
            case CrystalController.Colors.BLACK:
                GetComponent<Animator>().Play(Animator.StringToHash(BLACKDANCE));
                break;
            case CrystalController.Colors.INDIGO:
                GetComponent<Animator>().Play(Animator.StringToHash(INDIGODANCE));
                break;
            case CrystalController.Colors.RED:
                GetComponent<Animator>().Play(Animator.StringToHash(REDDANCE));
                break;
            case CrystalController.Colors.FUCHSIA:
                GetComponent<Animator>().Play(Animator.StringToHash(FUCHSIADANCE));
                break;
        }
    }
}
