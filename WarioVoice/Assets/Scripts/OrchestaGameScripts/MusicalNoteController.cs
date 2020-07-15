using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicalNoteController : MonoBehaviour
{
    public Sprite questionMark;
    [HideInInspector]
    public bool isOn = false;
    [HideInInspector]
    public Sprite instrumentSprite;
#pragma warning disable CS0414 // El campo 'MusicalNoteController.randomNumber' está asignado pero su valor nunca se usa
    private int randomNumber = 0;
#pragma warning restore CS0414 // El campo 'MusicalNoteController.randomNumber' está asignado pero su valor nunca se usa

    // Start is called before the first frame update
    void Start()
    {      
        GetComponent<Image>().sprite = questionMark;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            GetComponent<Image>().color = Color.white;
            GetComponent<Image>().sprite = instrumentSprite;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
            GetComponent<Image>().sprite = questionMark;
        }
    }
}
