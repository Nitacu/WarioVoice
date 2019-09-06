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
        }
    }
}
