using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroOrquesta : MonoBehaviour
{

    public void endAnimationOrquesta()
    {
        FindObjectOfType<TextScreenControl>().startGame();
        gameObject.SetActive(false);
    }
}
