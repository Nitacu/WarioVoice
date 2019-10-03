using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WTFBarController : MonoBehaviour
{
    private Image wtfBar;
    [HideInInspector]
    public float numberOfTries = 3;
    public float contSigns = 0;
    private WordController wordControl;
    public GameObject friend;

    private void Start()
    {
        wordControl = FindObjectOfType<WordController>();
        wtfBar = GetComponent<Image>();
        wtfBar.fillAmount = 0;
    }

    public void updateWTFbar()
    {
        contSigns++;
        if (contSigns < numberOfTries)
        {
            wtfBar.fillAmount = contSigns / numberOfTries;
        }
        else
        {
            wtfBar.fillAmount = 1;
            friend.SetActive(false);
            Invoke("callEndScene", 1);
        }
    }

    private void callEndScene()
    {
        wordControl.loseScene();
        wordControl.disableSpeechButton();
    }
}
