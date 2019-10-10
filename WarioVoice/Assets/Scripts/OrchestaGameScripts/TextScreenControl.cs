using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextScreenControl : MonoBehaviour
{

    private TextMeshProUGUI text;
    private PatternController _patternController;
    [HideInInspector]
    public bool intro = true;
   
    // Start is called before the first frame update
    void Start()
    {
        
        text = GetComponent<TextMeshProUGUI>();
        _patternController = FindObjectOfType<PatternController>();
        intro = true;
    }

    public void startGame()
    {
        text.text = " ";
        intro = false;
        _patternController.startGame();
    }
    
    public void clearText()
    {
        text.text = " ";

    }

    public void showInstrument(string instrument, float time)
    {
        if (!intro)
        {
            text.text = instrument;
            Invoke("clearText", time);
        }
    }

  

}
