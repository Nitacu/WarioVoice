using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShowingPatternControl : MonoBehaviour
{
    [HideInInspector]
    public int cont = 0;
    public TextMeshProUGUI text;
    public List<Sprite> numbers = new List<Sprite>();
    private Image spRenderer;
    public Color numberColor;

    // Start is called before the first frame update
    void Start()
    {
        cont = 0;
        GetComponent<Shadow>().enabled = false;
        spRenderer = GetComponent<Image>();
        spRenderer.color = Color.white;

    }


    public void showPattern(string instrument)
    {
        spRenderer.enabled = true;
        GetComponent<Shadow>().enabled = true;
        spRenderer.color = numberColor;
        text.text = "\n" +  "\n" + instrument;
        if(cont < 0 || cont > 7)
        {
            cont = 0;
        }
        spRenderer.sprite = numbers[cont];
        cont++;
    }

    public void clearScreen()
    {
        text.text = " ";
        spRenderer.enabled = false;
        
        cont = 0;
    }

}
