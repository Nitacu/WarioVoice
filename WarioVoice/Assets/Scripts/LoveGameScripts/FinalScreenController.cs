using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScreenController : MonoBehaviour
{
    public Sprite winScreen;
    public Sprite loseScreen;

    public void winScreenImage()
    {
        GetComponent<Image>().sprite = winScreen;
    }

    public void loseScreenImage()
    {
        GetComponent<Image>().sprite = loseScreen;
    }
}
