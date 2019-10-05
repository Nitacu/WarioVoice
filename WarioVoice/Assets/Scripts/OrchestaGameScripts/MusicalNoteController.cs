using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicalNoteController : MonoBehaviour
{
    public List<Sprite> musicNoteSprites = new List<Sprite>();
    [HideInInspector]
    public bool isOn = false;
    private int randomNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        randomNumber = Random.Range(0, musicNoteSprites.Count - 1);
        GetComponent<Image>().sprite = musicNoteSprites[randomNumber];
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            GetComponent<Image>().color = Color.red;
        }
        else
        {
            GetComponent<Image>().color = Color.black;
        }
    }
}
